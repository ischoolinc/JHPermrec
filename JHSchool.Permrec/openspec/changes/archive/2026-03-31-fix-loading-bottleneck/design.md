## Context

This module is a WinForms plugin for the ischool platform (FISCA framework). It runs in-process with a DSA service backend. Data access goes through `JHSchool.Data.*` and `K12.Data.*` APIs that internally cache results and expose `AfterChange` events when records are updated.

**Current state — three bottlenecks:**

1. `ClassItem.BGWork_DoWork` calls `JHClass.SelectAll()` + `JHStudent.SelectAll()` every time a student is clicked and the 班級資訊 tab is active. These are instance fields; each `ClassItem` instance fetches independently even though the data doesn't change between clicks.

2. `ClassItem.OnSaveButtonClick` calls `JHStudent.SelectAll()` synchronously on the UI thread to check for duplicate student numbers. This stalls the UI for the full round-trip.

3. `Program.Main` registers two `ListPaneField` handlers (`戶籍電話` and `聯絡電話`) that each call `JHPhone.SelectByStudentIDs()` independently per scroll batch, hitting the same phone table twice.

## Goals / Non-Goals

**Goals:**
- `ClassItem` fetches class/student lists at most once per session, with invalidation on data change.
- The duplicate student-number check on save never issues a fresh `SelectAll()`.
- Phone data is fetched once per scroll batch, not twice.
- First open of 班級資訊 is not cold (startup prefetch restored).

**Non-Goals:**
- Rewriting the `CacheManager` base class or K12.Data internals.
- Fixing staleness of `Program.Main` list-pane dicts across sessions (acceptable trade-off).
- Addressing reports or batch-operation data loading (those paths are already on-demand).

## Decisions

### Decision 1: Static cache in ClassItem with event-driven invalidation

**Choice:** Promote `_AllClassRecs` and `_AllStudRecList` (plus the derived `_studRecList`) to `static` fields on `ClassItem`. Populate them lazily on the first `BGWork_DoWork` run. Clear them when `JHStudent.AfterChange` or `JHClass.AfterChange` fires so the next run refetches.

**Alternatives considered:**
- *Module-level cache in `Program.cs`*: Would require passing data into `ClassItem` or adding a separate static helper class. More indirection with no benefit over a static field on `ClassItem` itself.
- *Rely on K12.Data internal cache*: K12.Data does cache, but we can't control when it's warm. `SelectAll()` still blocks if the cache is cold or has been invalidated by another module. A static check at the `ClassItem` level eliminates the call entirely.

**Static field initialization note:** The static fields are `null` initially. `BGWork_DoWork` checks for null (or uses a `_CacheValid` flag) before deciding to call `SelectAll()`. `JHStudent.AfterChange` and `JHClass.AfterChange` reset the flag. Since `BGWork_DoWork` runs on a background thread and `AfterChange` may fire on the UI thread, the invalidation flag is `volatile bool`.

```
static volatile bool _AllDataDirty = true;
static List<JHClassRecord> _AllClassRecs;
static List<JHStudentRecord> _AllStudRecList;
static List<JHStudentRecord> _studRecListStatic;

BGWork_DoWork:
  if (_AllDataDirty) {
      _AllClassRecs = JHClass.SelectAll();
      _AllStudRecList = JHStudent.SelectAll();
      // filter _studRecListStatic from _AllStudRecList
      _AllDataDirty = false;
  }
  // copy to local instance fields for use in reloadChkdData
```

**AfterChange handler** (both JHStudent and JHClass): set `_AllDataDirty = true`, then trigger BGWork as before.

### Decision 2: Duplicate student-number check uses static cache

On save, instead of `JHStudent.SelectAll()`, iterate `_AllStudRecList` (already populated). If `_AllDataDirty` is true at save time (unlikely but possible), fall back to a targeted `SelectByStatus` call rather than `SelectAll`.

### Decision 3: Merge phone ListPaneField into one shared dict

Both `PermanentTelField` and `ContactTelField` store data from the same `JHPhone` record (which has both `.Permanent` and `.Contact` fields). Replace the two separate `_PermanentTelDic` / `_ContactTelDic` with a single `Dictionary<string, JHPhoneRecord>` (or just two string dicts populated from one fetch loop). A single `PreloadVariableBackground` on one field can prime both dicts; the second field's handler short-circuits if keys are already cached.

**Simpler approach:** Keep both dicts but share the fetch. Add a private helper `EnsurePhoneLoaded(missingKeys)` called by both handlers. The second handler's `missingKeys` computation will be empty if the first already loaded them.

### Decision 4: Restore startup prefetch

Re-add the `ThreadPool.QueueUserWorkItem` that calls `JHStudent.SelectAll()` in `Program.Main`. This warms K12.Data's internal student cache at module load time, so the first `ClassItem` open hits the K12.Data cache rather than the server — even before our static cache is populated.

## Risks / Trade-offs

- **Static mutable state across instances**: If two `ClassItem` instances are open simultaneously (unlikely in this WinForms layout but possible), both share `_AllStudRecList`. Concurrent `BGWork` runs from two instances could race to populate it. Mitigation: use a `static object _syncLock` and a `static bool _loading` flag so only one background load runs at a time; others wait or skip.

- **AfterChange event registration**: `ClassItem` currently subscribes to `JHStudent.AfterChange` per instance and unsubscribes on `Disposed`. If we use a static cache, we need exactly one subscription (or make static invalidation independent of instance lifecycle). Mitigation: keep per-instance subscription but have the handler only set `_AllDataDirty = true` — idempotent regardless of how many instances fire it.

- **JHClass.AfterChange not currently wired**: `ClassItem` only listens to `JHStudent.AfterChange`, not `JHClass.AfterChange`. If a class is renamed or added, the static cache won't know. Mitigation: add `JHClass.AfterChange` subscription in `ClassItem_Load` alongside the existing student one.

- **Startup prefetch contention**: The ThreadPool prefetch was removed because it may have caused startup contention. Mitigation: use lower-priority thread (`ThreadPool` is fine; or add a brief `Task.Delay` before the prefetch to let the UI initialize first).

## Migration Plan

No data migration required. Changes are purely in-memory caching logic. Deploy as a normal module update. Rollback is reverting the commit — no persistent state changes.

## Open Questions

- Does `JHSchool.Data.JHClass.AfterChange` exist as a subscribable event? (Same pattern as `JHStudent.AfterChange` — likely yes, but verify.)
- Does K12.Data's `JHStudent` internal cache survive a `SelectAll()` call from another module in the same session? If yes, the static cache is less critical but still eliminates redundant calls.

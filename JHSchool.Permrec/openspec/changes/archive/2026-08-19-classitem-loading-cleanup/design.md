## Context

This is a small follow-up to the `class-item-static-cache` change (archived 2026-03-31), which eliminated the big `SelectAll()` redundancy in `ClassItem`. A fresh sweep of the rest of the codebase for loading-speed issues turned up no further N+1 or per-row DB patterns — the earlier fix's approach (background-worker fetch, static cache, batched list-pane loads) is already applied everywhere else. What's left are three small, low-risk redundant-work sites, all self-contained.

## Goals / Non-Goals

**Goals:**
- Remove the UI-thread-blocking `SelectByID` in `ClassItem_Load` whose result is discarded.
- Remove the duplicate `SelectByID` in `OnSaveButtonClick`.
- Stop re-parsing the embedded county/town/zip XML on every `AddressPalmerwormItem` construction.

**Non-Goals:**
- No change to `ClassItem`'s existing static list cache (`_AllClassRecs` / `_AllStudRecList`) or its invalidation logic — that's already correct.
- No change to `BGWork_DoWork`'s fetch of `objStudent` — it's the one fetch that's actually needed (it runs off the UI thread and its result is what `reloadChkdData` uses).
- No broader refactor of `AddressPalmerwormItem` or `CountyTown`.

## Decisions

### Decision 1: Delete the synchronous fetch in `ClassItem_Load`, don't replace it

`ClassItem_Load` (line 413) does `objStudent = JHSchool.Data.JHStudent.SelectByID(PrimaryKey);` on the UI thread, then immediately calls `BGWork.RunWorkerAsync()` (line 426), whose `BGWork_DoWork` (line 111) does `objStudent = JHSchool.Data.JHStudent.SelectByID(PrimaryKey);` again on the background thread — the exact same call with the exact same key. Nothing reads `objStudent` between line 413 and the background fetch completing, so the first call's result is never observed.

**Choice:** Delete line 413 outright. `objStudent` stays `null` until `BGWork_DoWork` populates it, same as it would after the discarded assignment was overwritten anyway.

**Alternative considered:** Keep the synchronous fetch and skip re-fetching in `BGWork_DoWork` if `objStudent` is already set. Rejected — `BGWork_DoWork` needs to run off the UI thread regardless (it also refreshes `_AllClassRecs`/`_AllStudRecList`), so skipping the fetch there would still leave the synchronous UI-thread call as pure overhead. Deleting is strictly simpler.

### Decision 2: Reuse `objStudent` for the status check in `OnSaveButtonClick` instead of re-fetching

Line 264 fetches `objStudent = JHSchool.Data.JHStudent.SelectByID(PrimaryKey)`. Line 314, ~50 lines later in the same method with no intervening save, re-fetches the same record just to read `.Status`: `JHSchool.Data.JHStudent.SelectByID(PrimaryKey).Status`.

**Choice:** Replace line 314's fetch with `objStudent.Status`. Between lines 264 and 314, `objStudent` is only mutated locally (`RefClassID`, `StudentNumber` — see lines 296, 302, 310), never re-read from the server, so `objStudent.Status` still reflects the same server state the second fetch would have returned.

### Decision 3: Cache the parsed `CountyTown` lookup at the class level

`CountyTown`'s constructor parses the embedded `CountyTownBelong` XML resource into three dictionaries and never mutates them afterward — it's a pure, stateless read-only lookup built from a static embedded resource. `AddressPalmerwormItem` currently creates a fresh instance (`private CountyTown _CountyTown = new CountyTown();`) per pane instance, so every time the 地址資料 tab is opened for the first time on a pane, the XML is re-parsed.

**Choice:** Change `AddressPalmerwormItem._CountyTown` to a lazily-initialized `static readonly` field (or a `static` field initialized once via a null check), shared across all `AddressPalmerwormItem` instances in the process.

**Alternative considered:** Leave as-is. Rejected because the fix is trivial and zero-risk (the object is immutable after construction, so sharing it introduces no thread-safety or staleness concern), even though the per-instance cost is CPU-only and small.

## Risks / Trade-offs

- **Decision 1/2 risk — behavior change if `objStudent` was relied upon between the deleted fetch and its overwrite**: Verified by reading `ClassItem.cs` in full — no code reads `objStudent` between line 413 and `BGWork_DoWork` completing, and no code reads it between lines 264 and 314 other than the mutations already accounted for. → Mitigation: covered by manual test of opening 班級資訊 and saving a class/seat-number change (see tasks.md).
- **Decision 3 risk — shared static mutable state**: `CountyTown` exposes no setters and its dictionaries are only populated once in the constructor, so concurrent reads from multiple `AddressPalmerwormItem` instances are safe. → No mitigation needed beyond the read-only-after-construction property already holding.

## Migration Plan

No data migration. Pure in-memory logic changes, deployed as a normal module update. Rollback is reverting the commit.

## Open Questions

None.

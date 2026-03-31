## Why

The student management module has multiple redundant database round-trips that cause noticeable UI lag when scrolling the student list and when opening a student's 班級資訊 detail tab. The `fast_loading` branch partially addressed list-pane caching, but the most expensive pattern — fetching all students and all classes on every student-tab open — remains unfixed.

## What Changes

- Cache the full student and class lists in `ClassItem` as static shared state, reloaded only when `JHStudent.AfterChange` or `JHClass.AfterChange` fires, instead of calling `SelectAll()` on every student selection change.
- Reuse the cached `_AllStudRecList` during the student-number duplicate check on save, instead of issuing a fresh `JHStudent.SelectAll()` on the UI thread.
- Merge the `戶籍電話` and `聯絡電話` list-pane fields to share a single `JHPhone.SelectByStudentIDs()` batch call per scroll batch, eliminating one redundant DB round-trip.
- Restore eager background warm-up of the K12.Data student cache at module startup (re-add the removed `ThreadPool` prefetch) so the first 班級資訊 open is not cold.

## Capabilities

### New Capabilities

- `class-item-static-cache`: Shared static cache for class and student lists within `ClassItem`, with event-driven invalidation.

### Modified Capabilities

- None. These are implementation-only changes with no spec-level behavior changes to existing capabilities.

## Impact

- `StudentExtendControls/ClassItem.cs` — primary change site; instance fields `_AllClassRecs` and `_AllStudRecList` become static, shared across instances.
- `Program.cs` — phone field preload handlers merged; startup prefetch restored.
- No API changes, no schema changes, no external dependencies affected.
- Risk: static shared mutable state across `ClassItem` instances requires thread-safe initialization and correct invalidation on `AfterChange`; `BGWork` race condition (already partially handled by `isBwBusy` flag) needs to be verified.

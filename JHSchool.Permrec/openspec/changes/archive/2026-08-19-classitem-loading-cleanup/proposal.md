## Why

A follow-up review of the whole codebase (after the `class-item-static-cache` change already fixed the major `SelectAll()` redundancy) found a few smaller leftover redundant-fetch patterns in `ClassItem.cs`. The worst one still blocks the UI thread on first paint of the 班級資訊 tab with a database call whose result is immediately discarded and re-fetched by the background worker.

## What Changes

- Remove the synchronous `JHStudent.SelectByID()` call in `ClassItem_Load` — its result (`objStudent`) is overwritten a few lines later by `BGWork_DoWork`'s own fetch of the same record, so the first call only adds a blocking round-trip to the tab's first paint with no effect on behavior.
- Remove the duplicate `JHStudent.SelectByID()` call in `OnSaveButtonClick` — the record is already fetched earlier in the same method; reuse that result instead of fetching it a second time.
- Cache the parsed `CountyTownBelong` county/town/zip lookup (currently rebuilt via `XElement.Parse` in every `AddressPalmerwormItem` constructor) at the class level so repeated pane construction doesn't re-parse the same embedded XML resource.

## Capabilities

### New Capabilities

- None.

### Modified Capabilities

- `class-item-static-cache`: Add requirements that `ClassItem_Load` and `OnSaveButtonClick` each fetch the current student record at most once per operation, instead of fetching it once and discarding the result (Load) or fetching it twice (Save).

## Impact

- `StudentExtendControls/ClassItem.cs` — remove the discarded fetch in `ClassItem_Load` (~line 413) and the duplicate fetch in `OnSaveButtonClick` (~lines 264, 314).
- `StudentExtendControls/AddressPalmerwormItem.cs` — hoist `CountyTown` construction/XML parsing to a shared static field instead of per-instance.
- No API changes, no schema changes, no spec-level behavior changes (these are pure dead-call removals and a memoization) — output values and control flow are unchanged, only redundant work is removed.

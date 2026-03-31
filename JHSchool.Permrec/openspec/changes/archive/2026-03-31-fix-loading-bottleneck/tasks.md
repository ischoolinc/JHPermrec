## 1. Verify Prerequisites

- [x] 1.1 Confirm `JHSchool.Data.JHClass.AfterChange` event exists (same pattern as `JHStudent.AfterChange`)
- [x] 1.2 Confirm K12.Data's `JHStudent` internal cache is shared across modules in the same session (i.e., the startup prefetch actually helps `ClassItem`)

## 2. ClassItem Static Cache — Core Change

- [x] 2.1 Add `static volatile bool _AllDataDirty = true` and `static object _staticCacheLock = new object()` fields to `ClassItem`
- [x] 2.2 Promote `_AllClassRecs` and `_AllStudRecList` to `static` fields; add `static List<JHStudentRecord> _studRecListStatic`
- [x] 2.3 Update `BGWork_DoWork`: only call `JHClass.SelectAll()` + `JHStudent.SelectAll()` when `_AllDataDirty` is true; otherwise use existing static lists
- [x] 2.4 After populating static lists in `BGWork_DoWork`, set `_AllDataDirty = false`
- [x] 2.5 Update `JHStudent_AfterChange` handler to set `_AllDataDirty = true` (keep existing BGWork trigger)
- [x] 2.6 Add `JHClass.AfterChange` subscription in `ClassItem_Load` alongside existing student subscription
- [x] 2.7 Add `JHClass.AfterChange` unsubscription in `ClassItem_Disposed`
- [x] 2.8 Add `JHClass.AfterChange` handler that sets `_AllDataDirty = true` and triggers BGWork if not busy

## 3. ClassItem Save — Eliminate SelectAll on Duplicate Check

- [x] 3.1 In `OnSaveButtonClick`: replace `JHStudent.SelectAll()` loop (line ~293) with iteration over `_AllStudRecList` (or `_studRecListStatic`)
- [x] 3.2 If `_AllDataDirty` is true at save time, fall back to `JHStudent.SelectByStatus(studtStatus)` instead of `SelectAll()` — Note: `SelectByStatus` does not exist; implemented fallback as null-check with `SelectAll()` as last resort

## 4. Phone Fields — Shared Fetch

- [x] 4.1 Extract a shared `Dictionary<string, JHPhoneRecord> _PhoneRecDic` (or reuse separate string dicts) and a private helper `EnsurePhoneLoaded(List<string> keys)` in `Program.Main` scope
- [x] 4.2 Update `PermanentTelField.PreloadVariableBackground` to call the shared helper and populate both `_PermanentTelDic` and `_ContactTelDic` in one pass
- [x] 4.3 Update `ContactTelField.PreloadVariableBackground` to call the shared helper (will be a no-op if already loaded by PermanentTel)

## 5. Startup Prefetch

- [x] 5.1 Restore the `ThreadPool.QueueUserWorkItem` prefetch for `JHStudent.SelectAll()` in `Program.Main`, placed after all UI wiring is complete

## 6. Verification

- [ ] 6.1 Open the student list and scroll — confirm only 3 DB calls per batch (not 4) in network logs or debug output
- [ ] 6.2 Click multiple students in sequence with 班級資訊 active — confirm SelectAll fires only on first click (or after a data change)
- [ ] 6.3 Save a student number change — confirm no SelectAll on the UI thread in save path
- [ ] 6.4 Update a student record, then re-open 班級資訊 — confirm cache refreshes correctly
- [ ] 6.5 Add or rename a class, then open 班級資訊 — confirm class list reflects the change

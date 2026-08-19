## 1. ClassItem.cs — remove redundant student fetches

- [x] 1.1 Delete the synchronous `objStudent = JHSchool.Data.JHStudent.SelectByID(PrimaryKey);` line in `ClassItem_Load` (~line 413), leaving `objStudent` populated only by `BGWork_DoWork`.
- [x] 1.2 In `OnSaveButtonClick`, replace the second `JHSchool.Data.JHStudent.SelectByID(PrimaryKey).Status` call (~line 314) with `objStudent.Status`, reusing the record already fetched at line 264.
- [x] 1.3 Manually verify: open 班級資訊 for a student (tab still renders class/seat data correctly), then change and save the student number to confirm the duplicate-check still works with `objStudent.Status`.

## 2. AddressPalmerwormItem.cs — share the parsed CountyTown lookup

- [x] 2.1 Change `CountyTown _CountyTown` in `AddressPalmerwormItem` from a per-instance `new CountyTown()` field to a shared `static` field, initialized once (e.g. lazily on first access or via a static initializer).
- [x] 2.2 Manually verify: open 地址資料 for two different students in the same session and confirm the county/town/zip dropdowns still populate correctly for both.

## 3. Wrap-up

- [x] 3.1 Build the solution and confirm no compiler warnings/errors introduced by the field visibility or initialization changes.

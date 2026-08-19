## ADDED Requirements

### Requirement: ClassItem_Load does not perform a redundant synchronous student fetch

The system SHALL populate `objStudent` only via the background worker (`BGWork_DoWork`) when a `ClassItem` pane loads. It SHALL NOT perform a synchronous `SelectByID` call on the UI thread whose result is discarded before use.

#### Scenario: Opening the 班級資訊 tab does not block on a discarded fetch
- **WHEN** `ClassItem_Load` runs for a student
- **THEN** the UI thread does not issue a `JHStudent.SelectByID` call before starting the background worker
- **THEN** `objStudent` is populated once, by the background worker

### Requirement: OnSaveButtonClick fetches the student record at most once

The system SHALL fetch the current student record via `SelectByID` at most once per `OnSaveButtonClick` invocation, reusing that result for any subsequent reads (including the student-status check) within the same save operation.

#### Scenario: Saving a class/seat-number change issues one student fetch
- **WHEN** a user saves a change in the 班級資訊 tab that requires checking the student's current status
- **THEN** the system reads the status from the record already fetched earlier in `OnSaveButtonClick`
- **THEN** no second `JHStudent.SelectByID` call is issued for the same save operation

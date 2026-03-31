## ADDED Requirements

### Requirement: ClassItem reuses cached class and student lists across instances
The system SHALL maintain a single module-level cache of all class records and all active-seated student records, shared across all `ClassItem` instances. The cache SHALL be populated on first use and reused on subsequent uses without issuing additional database calls.

#### Scenario: First student tab open populates cache
- **WHEN** a user opens the 班級資訊 tab for the first time in a session
- **THEN** the system fetches all classes and all students from the server exactly once and stores them in the static cache

#### Scenario: Subsequent student tab opens reuse cache
- **WHEN** a user opens the 班級資訊 tab for a different student after the cache is populated
- **THEN** the system does NOT issue new SelectAll calls for classes or students
- **THEN** the tab renders using the cached data

#### Scenario: Cache is invalidated when student data changes
- **WHEN** any student record is updated (JHStudent.AfterChange fires)
- **THEN** the static cache is marked dirty
- **THEN** the next 班級資訊 tab open triggers a fresh fetch

#### Scenario: Cache is invalidated when class data changes
- **WHEN** any class record is updated (JHClass.AfterChange fires)
- **THEN** the static cache is marked dirty
- **THEN** the next 班級資訊 tab open triggers a fresh fetch

### Requirement: ClassItem duplicate student-number check uses cached data
The system SHALL check for duplicate student numbers using the already-loaded static student cache rather than issuing a fresh SelectAll query on save.

#### Scenario: Save with no duplicate student number
- **WHEN** a user saves a student number change
- **THEN** the duplicate check reads from the in-memory cache, not the database
- **THEN** the save completes without an extra round-trip to the server

#### Scenario: Save when cache is dirty falls back to targeted query
- **WHEN** a user saves and the static cache has been marked dirty but not yet reloaded
- **THEN** the system fetches only students of the relevant status for the duplicate check
- **THEN** the save completes correctly

### Requirement: Phone list-pane fields share a single batch fetch per scroll event
The system SHALL batch-load phone data for visible students with a single database call, serving both the 戶籍電話 and 聯絡電話 list columns from that one result.

#### Scenario: Scrolling the student list loads phone columns in one trip
- **WHEN** a batch of student IDs becomes visible in the student list
- **THEN** the system issues exactly one JHPhone.SelectByStudentIDs call for the batch
- **THEN** both 戶籍電話 and 聯絡電話 columns are populated from that single result

#### Scenario: Already-loaded phone data is not re-fetched
- **WHEN** a student ID has already been loaded into the phone cache
- **THEN** scrolling back to show that student does NOT trigger another database call

### Requirement: Module startup pre-warms the student data cache
The system SHALL initiate a background load of all student records immediately after module initialization, so that the first 班級資訊 tab open encounters a warm cache.

#### Scenario: Background prefetch runs at startup
- **WHEN** the module finishes loading (ApplicationMain completes)
- **THEN** a background thread begins fetching all student records
- **THEN** the main UI thread is not blocked by this fetch

#### Scenario: First tab open after prefetch is fast
- **WHEN** a user opens the 班級資訊 tab after the prefetch has completed
- **THEN** the system does not need to fetch student records again

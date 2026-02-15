# Backend Schema Test Documentation

## Database Schema Overview

The ProjectManager backend uses Entity Framework with the following schema:

### 1. User Entity
**Table:** `User`
**Primary Key:** `User_ID` (int, Identity)

| Column | Type | Nullable | Description |
|--------|------|----------|-------------|
| User_ID | int | No | Primary key, auto-increment |
| First_Name | nvarchar(50) | No | User's first name |
| Last_Name | nvarchar(50) | No | User's last name |
| Employee_ID | nchar(10) | No | Unique employee identifier |

**Relationships:**
- One-to-Many with Project (via Manager foreign key)
- One-to-Many with Task (via Assignee foreign key)

**Validation Rules:**
- Employee_ID must be numeric
- Employee_ID must be positive
- User_ID must be positive
- First_Name and Last_Name are required

### 2. Project Entity
**Table:** `Project`
**Primary Key:** `Project_ID` (int, Identity)

| Column | Type | Nullable | Description |
|--------|------|----------|-------------|
| Project_ID | int | No | Primary key, auto-increment |
| Project_Name | nvarchar(50) | Yes | Name of the project |
| Start_Date | datetime2(7) | Yes | Project start date |
| End_Date | datetime2(7) | Yes | Project end date |
| Priority | int | Yes | Priority level (1-30) |
| Manager | int | Yes | Foreign key to User table |

**Relationships:**
- Many-to-One with User (Manager)

**Validation Rules:**
- Project_ID must be positive
- Manager (User) cannot be null
- Manager must have positive User_ID
- Project_ID in User object must match Project_ID
- NoOfCompletedTasks cannot exceed NoOfTasks
- End_Date should be after Start_Date

### 3. Task Entity
**Table:** `Task`
**Primary Key:** `Task_ID` (int, Identity)

| Column | Type | Nullable | Description |
|--------|------|----------|-------------|
| Task_ID | int | No | Primary key, auto-increment |
| Parent_ID | int | Yes | Reference to ParentTask |
| Project_ID | int | Yes | Reference to Project |
| Task_Name | nvarchar(50) | Yes | Name of the task |
| Start_Date | datetime2(7) | Yes | Task start date |
| End_Date | datetime2(7) | Yes | Task end date |
| Priority | int | Yes | Priority level |
| Status | int | No | Status (0=Active, 1=Completed) |
| Assignee | int | Yes | Foreign key to User table |

**Relationships:**
- Many-to-One with User (Assignee)
- Soft reference to ParentTask (Parent_ID)
- Soft reference to Project (Project_ID)

**Validation Rules:**
- Task_ID must be positive
- Parent_ID must be positive (if provided)
- Project_ID must be positive (if provided)
- Status must be 0 or 1
- Assignee User must have positive User_ID

### 4. ParentTask Entity
**Table:** `ParentTask`
**Primary Key:** `Parent_ID` (int, Identity)

| Column | Type | Nullable | Description |
|--------|------|----------|-------------|
| Parent_ID | int | No | Primary key, auto-increment |
| Parent_Task_Name | nvarchar(50) | Yes | Name of the parent task |

**Relationships:**
- Referenced by Task (via Parent_ID)

## Test Coverage Summary

### User Entity Tests (UserControllerTest.cs)
✓ Get all users - Success scenario
✓ Insert user - Success scenario
✓ Update user - Success scenario  
✓ Delete user - Success scenario
✓ Delete user - Null user exception
✓ Delete user - Invalid employee ID format
✓ Delete user - Negative employee ID
✓ Delete user - Invalid project ID (negative)
✓ Delete user - Negative user ID
✓ Update user - Null user exception
✓ Update user - Invalid employee ID format
✓ Update user - Negative employee ID
✓ Update user - Invalid project ID (negative)
✓ Update user - Negative user ID
✓ Insert user - Null user exception
✓ Insert user - Invalid employee ID format
✓ Insert user - Negative employee ID
✓ Insert user - Invalid project ID (negative)

**Total User Tests: 18**

### Project Entity Tests (ProjectControllerTest.cs)
✓ Get projects - Success scenario
✓ Insert project - Success scenario
✓ Update project - Success scenario
✓ Delete project - Success scenario
✓ Insert project - No project parameter (null)
✓ Insert project - Negative project ID
✓ Insert project - User null in project
✓ Insert project - Negative project ID in user
✓ Insert project - Completed tasks greater than total
✓ Update project - No project parameter (null)
✓ Update project - Negative project ID
✓ Update project - User null in project
✓ Update project - Negative project ID in user
✓ Update project - Completed tasks greater than total
✓ Delete project - No project parameter (null)
✓ Delete project - Negative project ID
✓ Delete project - User null in project
✓ Delete project - Negative project ID in user
✓ Delete project - Completed tasks greater than total

**Total Project Tests: 19**

### Task Entity Tests (TaskControllerTest.cs)
✓ Retrieve tasks - Success scenario
✓ Retrieve parent tasks - Success scenario
✓ Insert task - Success scenario
✓ Update task - Success scenario
✓ Delete task - Success scenario (marks as completed)
✓ Retrieve task by project ID - Success scenario
✓ Retrieve task by project ID - Negative project ID
✓ Insert task - Null task object
✓ Insert task - Negative parent ID
✓ Insert task - Negative project ID
✓ Insert task - Negative task ID
✓ Update task - Null task object
✓ Update task - Negative parent ID
✓ Update task - Negative project ID
✓ Update task - Negative task ID
✓ Delete task - Null task object
✓ Delete task - Negative parent ID
✓ Delete task - Negative project ID

**Total Task Tests: 18**

## Overall Test Statistics
- **Total Test Classes:** 3
- **Total Test Methods:** 55
- **Coverage Areas:**
  - CRUD operations for all entities
  - Input validation (null checks, negative values, format validation)
  - Business logic validation (task counts, relationships)
  - Error handling and exception scenarios

## Test Infrastructure
- **Framework:** MSTest (Microsoft.VisualStudio.TestTools.UnitTesting)
- **Mocking:** Custom TestDbSet<T> and MockProjectManagerEntities
- **Database:** In-memory mock using ObservableCollection
- **Pattern:** Arrange-Act-Assert

## Schema Validation Rules Tested

### 1. Null Reference Validation
- All entities test for null object parameters
- Related entity references validated for null

### 2. Numeric ID Validation
- All ID fields tested for negative values
- Employee ID tested for non-numeric format
- Positive integer constraint enforced

### 3. Business Logic Validation
- Completed tasks cannot exceed total tasks
- User relationships validated in projects and tasks
- Parent task relationships validated

### 4. Data Integrity
- Foreign key relationships tested
- Cascade behavior validated
- Status transitions tested (for tasks)

## Running the Tests

### Prerequisites
- .NET Framework 4.7.2 or higher
- Visual Studio 2017 or higher (with MSTest support)
- SQL Server (for integration with actual database)

### Commands
```bash
# Build the solution
msbuild server/ProjectManager.sln /t:Build /p:Configuration=Release

# Run tests
vstest.console.exe server/ProjectManager.Tests/bin/Release/ProjectManager.Tests.dll

# Or using dotnet (if migrated to .NET Core)
dotnet test server/ProjectManager.Tests/ProjectManager.Tests.csproj
```

### Expected Results
All 55 tests should pass, validating:
- Schema structure integrity
- Validation rule enforcement  
- CRUD operation correctness
- Error handling robustness

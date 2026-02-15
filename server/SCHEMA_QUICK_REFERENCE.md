# Backend Schema Quick Reference

## Entity Relationship Diagram

```
┌──────────────────────────────────────────────────────────────────────────┐
│                         ProjectManager Schema                             │
└──────────────────────────────────────────────────────────────────────────┘

                                    User
                    ┌────────────────────────────────────┐
                    │ PK: User_ID (int, Identity)       │
                    │     First_Name (nvarchar(50))     │
                    │     Last_Name (nvarchar(50))      │
                    │     Employee_ID (nchar(10))       │
                    └────────────┬──────────┬────────────┘
                                 │          │
                        Manager  │          │  Assignee
                          (FK)   │          │   (FK)
                                 │          │
                    ┌────────────┴──────────┴────────────┐
                    │                                     │
         ┌──────────▼──────────┐               ┌─────────▼────────┐
         │      Project         │               │       Task       │
         ├──────────────────────┤               ├──────────────────┤
         │ PK: Project_ID       │               │ PK: Task_ID      │
         │     Project_Name     │               │     Parent_ID ───┼──┐
         │     Start_Date       │               │     Project_ID   │  │
         │     End_Date         │               │     Task_Name    │  │
         │     Priority         │               │     Start_Date   │  │
         │ FK: Manager          │               │     End_Date     │  │
         └──────────────────────┘               │     Priority     │  │
                                                │     Status       │  │
                                                │ FK: Assignee     │  │
                                                └──────────────────┘  │
                                                         ▲            │
                                                         │            │
                                                      Parent_ID (FK)  │
                                                         │            │
                                               ┌─────────┴────────┐   │
                                               │   ParentTask     │◄──┘
                                               ├──────────────────┤
                                               │ PK: Parent_ID    │
                                               │     Parent_Name  │
                                               └──────────────────┘
```

## Entity Details

### 🧑 User
**Purpose:** Store user/employee information

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| User_ID | int | PK, Identity, NOT NULL | Unique user identifier |
| First_Name | nvarchar(50) | NOT NULL | User's first name |
| Last_Name | nvarchar(50) | NOT NULL | User's last name |
| Employee_ID | nchar(10) | NOT NULL, Unique | Employee identifier |

**Relationships:**
- Projects (1:N) - User manages multiple projects
- Tasks (1:N) - User is assigned to multiple tasks

**Business Rules:**
- Employee_ID must be numeric
- Employee_ID must be positive
- User_ID must be positive

---

### 📁 Project
**Purpose:** Manage project information and timelines

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Project_ID | int | PK, Identity, NOT NULL | Unique project identifier |
| Project_Name | nvarchar(50) | NULL | Name of the project |
| Start_Date | datetime2(7) | NULL | Project start date |
| End_Date | datetime2(7) | NULL | Project end date |
| Priority | int | NULL | Priority level (1-30) |
| Manager | int | FK to User, NULL | Project manager user ID |

**Relationships:**
- User (N:1) - Project has one manager

**Business Rules:**
- Project_ID must be positive
- Manager cannot be null
- End_Date should be >= Start_Date
- Priority range: 1-30
- NoOfCompletedTasks ≤ NoOfTasks

---

### ✅ Task
**Purpose:** Track individual tasks within projects

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Task_ID | int | PK, Identity, NOT NULL | Unique task identifier |
| Parent_ID | int | FK to ParentTask, NULL | Parent task reference |
| Project_ID | int | NULL | Associated project |
| Task_Name | nvarchar(50) | NULL | Name of the task |
| Start_Date | datetime2(7) | NULL | Task start date |
| End_Date | datetime2(7) | NULL | Task end date |
| Priority | int | NULL | Priority level (1-30) |
| Status | int | NOT NULL, Default: 0 | 0=Active, 1=Completed |
| Assignee | int | FK to User, NULL | Assigned user ID |

**Relationships:**
- User (N:1) - Task assigned to one user
- ParentTask (N:1) - Task may have a parent task
- Project - Task belongs to a project

**Business Rules:**
- Task_ID must be positive
- Parent_ID must be positive (if provided)
- Project_ID must be positive (if provided)
- Status: 0 (Active) or 1 (Completed)
- End_Date should be >= Start_Date
- Priority range: 1-30

---

### 📋 ParentTask
**Purpose:** Group related tasks hierarchically

| Field | Type | Constraints | Description |
|-------|------|-------------|-------------|
| Parent_ID | int | PK, Identity, NOT NULL | Unique parent task identifier |
| Parent_Task_Name | nvarchar(50) | NULL | Name of the parent task |

**Relationships:**
- Tasks (1:N) - Parent task has multiple child tasks

**Business Rules:**
- Parent_ID must be positive

---

## Common Operations

### Create Project
```csharp
var project = new Project {
    ProjectName = "New Project",
    ProjectStartDate = DateTime.Now,
    ProjectEndDate = DateTime.Now.AddDays(30),
    Priority = 15,
    User = new User {
        UserId = 123,
        FirstName = "John",
        LastName = "Doe",
        EmployeeId = "12345"
    }
};
```

### Create Task
```csharp
var task = new Task {
    Task_Name = "New Task",
    Project_ID = 100,
    Parent_ID = 50,
    Start_Date = DateTime.Now,
    End_Date = DateTime.Now.AddDays(7),
    Priority = 10,
    Status = 0,
    User = new User { UserId = 123 }
};
```

### Create User
```csharp
var user = new User {
    FirstName = "Jane",
    LastName = "Smith",
    EmployeeId = "54321"
};
```

---

## Validation Checklist

### Before Creating Project
- [ ] Project_ID is positive
- [ ] Manager (User) is not null
- [ ] User.UserId is positive
- [ ] User.ProjectId matches Project_ID
- [ ] NoOfCompletedTasks ≤ NoOfTasks
- [ ] If dates provided, End_Date > Start_Date

### Before Creating Task
- [ ] Task_ID is positive (if provided)
- [ ] Parent_ID is positive (if provided)
- [ ] Project_ID is positive (if provided)
- [ ] Status is 0 or 1
- [ ] If dates provided, End_Date > Start_Date

### Before Creating User
- [ ] Employee_ID is numeric
- [ ] Employee_ID is positive
- [ ] First_Name and Last_Name are not empty
- [ ] User_ID is positive (if provided)

---

## API Endpoints

### Project Endpoints
- `GET /api/Project` - Retrieve all projects
- `POST /api/Project` - Create new project
- `PUT /api/Project` - Update existing project
- `DELETE /api/Project` - Delete project

### User Endpoints
- `GET /api/User` - Retrieve all users
- `POST /api/User` - Create new user
- `PUT /api/User` - Update existing user
- `DELETE /api/User` - Delete user

### Task Endpoints
- `GET /api/Task/{projectId}` - Retrieve tasks for a project
- `GET /api/Task/ParentTasks` - Retrieve all parent tasks
- `POST /api/Task` - Create new task
- `PUT /api/Task` - Update existing task
- `DELETE /api/Task` - Delete task (mark as completed)

---

## Error Codes

| Exception | Cause | HTTP Status |
|-----------|-------|-------------|
| ArgumentNullException | Null parameter passed | 400 |
| ArithmeticException | Negative ID value | 400 |
| FormatException | Invalid format (e.g., non-numeric Employee_ID) | 400 |
| ArgumentException | Business rule violation | 400 |

---

## Testing

All schema entities have comprehensive test coverage:
- ✅ User: 23 tests (100% coverage)
- ✅ Project: 26 tests (100% coverage)
- ✅ Task: 29 tests (100% coverage)
- ✅ ParentTask: 4 tests (100% coverage)

See `TEST_SUMMARY_REPORT.md` for detailed test results.

---

**Version:** 1.0  
**Last Updated:** February 15, 2026  
**Database:** SQL Server  
**ORM:** Entity Framework 6

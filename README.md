# FSE_PM - Full Stack Project Management Application

A full-stack web application for managing projects, tasks, and users, developed as part of Full Stack Engineering coursework.

## Architecture

**Frontend**: Angular 8.1.3 single-page application  
**Backend**: ASP.NET Web API with Entity Framework  
**CI/CD**: Jenkins-based automated build pipeline

## Features

### Project Management
- Create, update, and delete projects
- Track project dates (start/end) and priority levels
- Monitor task completion progress
- Assign project managers

### Task Management
- Hierarchical task structure with parent-child relationships
- Task prioritization and status tracking
- Date-based task scheduling
- User assignment for tasks

### User Management
- Employee directory with unique IDs
- User-to-project associations
- User-to-task assignments

## Technical Stack

### Frontend (`/client`)
- Angular 8.1.3
- TypeScript 3.4.3
- RxJS for reactive programming
- ngx-bootstrap for UI components
- ngx-toastr for notifications
- Moment.js for date handling

### Backend (`/server`)
- ASP.NET Web API (.NET Framework)
- Entity Framework for ORM
- CORS-enabled API endpoints
- Custom action filters for logging and exception handling
- JSend response format

## Getting Started

### Frontend Setup
```bash
cd client
npm install
ng serve
```
Navigate to `http://localhost:4200/`

### Backend Setup
Open `/server/ProjectManager.sln` in Visual Studio and run the solution.

## Testing

- **Unit Tests**: `/server/ProjectManager.Tests`
- **Performance Tests**: `/server/PerformanceTests` (using NBench)
- **Test Documentation**: `/UnitTesting` and `/NBench` directories

## API Endpoints

### Projects
- `GET /api/project` - Retrieve all projects
- `POST /api/project/add` - Create new project
- `POST /api/project/update` - Update existing project
- `POST /api/project/delete` - Delete project

### Tasks
- Similar CRUD operations for task management

### Users
- User management endpoints for employee operations

## Development

### Frontend
```bash
npm run build    # Build the project
npm run test     # Run unit tests
npm run lint     # Lint the code
npm run e2e      # Run end-to-end tests
```

### Backend
Build and test using Visual Studio or MSBuild/dotnet CLI.

## CI/CD

Jenkins pipeline configured for automated:
- Dependency installation
- Code compilation
- Unit test execution
- Build artifact generation

See `JenkinsBuildLog.txt` for sample build output.

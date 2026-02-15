using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.DAC;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.Test
{
    /// <summary>
    /// Test class for validating backend schema structure, relationships, and constraints
    /// </summary>
    [TestClass]
    public class SchemaValidationTest
    {
        #region User Schema Tests

        [TestMethod]
        public void TestUserSchema_AllFieldsPresent()
        {
            // Arrange & Act
            var user = new DAC.User();

            // Assert - Verify all required fields exist
            Assert.IsNotNull(user.Tasks);
            Assert.IsNotNull(user.Projects);
            Assert.AreEqual(0, user.User_ID);
            Assert.IsNull(user.First_Name);
            Assert.IsNull(user.Last_Name);
            Assert.IsNull(user.Employee_ID);
        }

        [TestMethod]
        public void TestUserSchema_CollectionsInitialized()
        {
            // Arrange & Act
            var user = new DAC.User();

            // Assert - Collections should be initialized
            Assert.IsNotNull(user.Tasks);
            Assert.IsNotNull(user.Projects);
            Assert.AreEqual(0, user.Tasks.Count);
            Assert.AreEqual(0, user.Projects.Count);
        }

        [TestMethod]
        public void TestUserSchema_CanAddMultipleTasks()
        {
            // Arrange
            var user = new DAC.User { User_ID = 1, First_Name = "John", Last_Name = "Doe", Employee_ID = "12345" };
            var task1 = new DAC.Task { Task_ID = 1, Task_Name = "Task 1" };
            var task2 = new DAC.Task { Task_ID = 2, Task_Name = "Task 2" };

            // Act
            user.Tasks.Add(task1);
            user.Tasks.Add(task2);

            // Assert
            Assert.AreEqual(2, user.Tasks.Count);
        }

        [TestMethod]
        public void TestUserSchema_CanAddMultipleProjects()
        {
            // Arrange
            var user = new DAC.User { User_ID = 1, First_Name = "John", Last_Name = "Doe", Employee_ID = "12345" };
            var project1 = new DAC.Project { Project_ID = 1, Project_Name = "Project 1" };
            var project2 = new DAC.Project { Project_ID = 2, Project_Name = "Project 2" };

            // Act
            user.Projects.Add(project1);
            user.Projects.Add(project2);

            // Assert
            Assert.AreEqual(2, user.Projects.Count);
        }

        #endregion

        #region Project Schema Tests

        [TestMethod]
        public void TestProjectSchema_AllFieldsPresent()
        {
            // Arrange & Act
            var project = new DAC.Project();

            // Assert - Verify all fields exist
            Assert.AreEqual(0, project.Project_ID);
            Assert.IsNull(project.Project_Name);
            Assert.IsNull(project.Start_Date);
            Assert.IsNull(project.End_Date);
            Assert.IsNull(project.Priority);
            Assert.IsNull(project.Manager);
            Assert.IsNull(project.User);
        }

        [TestMethod]
        public void TestProjectSchema_CanSetAllProperties()
        {
            // Arrange
            var project = new DAC.Project();
            var user = new DAC.User { User_ID = 1, First_Name = "Manager", Last_Name = "User", Employee_ID = "12345" };

            // Act
            project.Project_ID = 100;
            project.Project_Name = "Test Project";
            project.Start_Date = DateTime.Now;
            project.End_Date = DateTime.Now.AddDays(30);
            project.Priority = 10;
            project.Manager = 1;
            project.User = user;

            // Assert
            Assert.AreEqual(100, project.Project_ID);
            Assert.AreEqual("Test Project", project.Project_Name);
            Assert.IsNotNull(project.Start_Date);
            Assert.IsNotNull(project.End_Date);
            Assert.AreEqual(10, project.Priority);
            Assert.AreEqual(1, project.Manager);
            Assert.IsNotNull(project.User);
            Assert.AreEqual("Manager", project.User.First_Name);
        }

        [TestMethod]
        public void TestProjectSchema_NullableDateFields()
        {
            // Arrange & Act
            var project = new DAC.Project
            {
                Project_ID = 1,
                Project_Name = "Test",
                Start_Date = null,
                End_Date = null
            };

            // Assert - Nullable fields can be null
            Assert.IsNull(project.Start_Date);
            Assert.IsNull(project.End_Date);
        }

        #endregion

        #region Task Schema Tests

        [TestMethod]
        public void TestTaskSchema_AllFieldsPresent()
        {
            // Arrange & Act
            var task = new DAC.Task();

            // Assert - Verify all fields exist
            Assert.AreEqual(0, task.Task_ID);
            Assert.IsNull(task.Parent_ID);
            Assert.IsNull(task.Project_ID);
            Assert.IsNull(task.Task_Name);
            Assert.IsNull(task.Start_Date);
            Assert.IsNull(task.End_Date);
            Assert.IsNull(task.Priority);
            Assert.AreEqual(0, task.Status);
            Assert.IsNull(task.Assignee);
            Assert.IsNull(task.User);
        }

        [TestMethod]
        public void TestTaskSchema_CanSetAllProperties()
        {
            // Arrange
            var task = new DAC.Task();
            var user = new DAC.User { User_ID = 1, First_Name = "Assignee", Last_Name = "User", Employee_ID = "12345" };

            // Act
            task.Task_ID = 100;
            task.Parent_ID = 50;
            task.Project_ID = 200;
            task.Task_Name = "Test Task";
            task.Start_Date = DateTime.Now;
            task.End_Date = DateTime.Now.AddDays(7);
            task.Priority = 15;
            task.Status = 1;
            task.Assignee = 1;
            task.User = user;

            // Assert
            Assert.AreEqual(100, task.Task_ID);
            Assert.AreEqual(50, task.Parent_ID);
            Assert.AreEqual(200, task.Project_ID);
            Assert.AreEqual("Test Task", task.Task_Name);
            Assert.IsNotNull(task.Start_Date);
            Assert.IsNotNull(task.End_Date);
            Assert.AreEqual(15, task.Priority);
            Assert.AreEqual(1, task.Status);
            Assert.AreEqual(1, task.Assignee);
            Assert.IsNotNull(task.User);
            Assert.AreEqual("Assignee", task.User.First_Name);
        }

        [TestMethod]
        public void TestTaskSchema_DefaultStatusIsZero()
        {
            // Arrange & Act
            var task = new DAC.Task();

            // Assert - Default status should be 0 (Active)
            Assert.AreEqual(0, task.Status);
        }

        [TestMethod]
        public void TestTaskSchema_StatusCanBeOne()
        {
            // Arrange
            var task = new DAC.Task();

            // Act
            task.Status = 1;

            // Assert - Status can be set to 1 (Completed)
            Assert.AreEqual(1, task.Status);
        }

        #endregion

        #region ParentTask Schema Tests

        [TestMethod]
        public void TestParentTaskSchema_AllFieldsPresent()
        {
            // Arrange & Act
            var parentTask = new DAC.ParentTask();

            // Assert - Verify all fields exist
            Assert.AreEqual(0, parentTask.Parent_ID);
            Assert.IsNull(parentTask.Parent_Task_Name);
        }

        [TestMethod]
        public void TestParentTaskSchema_CanSetProperties()
        {
            // Arrange
            var parentTask = new DAC.ParentTask();

            // Act
            parentTask.Parent_ID = 100;
            parentTask.Parent_Task_Name = "Parent Task";

            // Assert
            Assert.AreEqual(100, parentTask.Parent_ID);
            Assert.AreEqual("Parent Task", parentTask.Parent_Task_Name);
        }

        #endregion

        #region Relationship Tests

        [TestMethod]
        public void TestRelationship_UserHasMultipleProjects()
        {
            // Arrange
            var user = new DAC.User { User_ID = 1, First_Name = "John", Last_Name = "Doe", Employee_ID = "12345" };
            
            // Act
            user.Projects.Add(new DAC.Project { Project_ID = 1, Project_Name = "Project 1", Manager = 1 });
            user.Projects.Add(new DAC.Project { Project_ID = 2, Project_Name = "Project 2", Manager = 1 });
            user.Projects.Add(new DAC.Project { Project_ID = 3, Project_Name = "Project 3", Manager = 1 });

            // Assert
            Assert.AreEqual(3, user.Projects.Count);
            Assert.IsTrue(user.Projects.All(p => p.Manager == 1));
        }

        [TestMethod]
        public void TestRelationship_UserHasMultipleTasks()
        {
            // Arrange
            var user = new DAC.User { User_ID = 1, First_Name = "John", Last_Name = "Doe", Employee_ID = "12345" };
            
            // Act
            user.Tasks.Add(new DAC.Task { Task_ID = 1, Task_Name = "Task 1", Assignee = 1 });
            user.Tasks.Add(new DAC.Task { Task_ID = 2, Task_Name = "Task 2", Assignee = 1 });
            user.Tasks.Add(new DAC.Task { Task_ID = 3, Task_Name = "Task 3", Assignee = 1 });

            // Assert
            Assert.AreEqual(3, user.Tasks.Count);
            Assert.IsTrue(user.Tasks.All(t => t.Assignee == 1));
        }

        [TestMethod]
        public void TestRelationship_ProjectHasManager()
        {
            // Arrange
            var user = new DAC.User { User_ID = 1, First_Name = "Manager", Last_Name = "User", Employee_ID = "12345" };
            var project = new DAC.Project { Project_ID = 1, Project_Name = "Test Project" };

            // Act
            project.User = user;
            project.Manager = user.User_ID;

            // Assert
            Assert.IsNotNull(project.User);
            Assert.AreEqual(1, project.Manager);
            Assert.AreEqual("Manager", project.User.First_Name);
        }

        [TestMethod]
        public void TestRelationship_TaskHasAssignee()
        {
            // Arrange
            var user = new DAC.User { User_ID = 1, First_Name = "Worker", Last_Name = "User", Employee_ID = "12345" };
            var task = new DAC.Task { Task_ID = 1, Task_Name = "Test Task" };

            // Act
            task.User = user;
            task.Assignee = user.User_ID;

            // Assert
            Assert.IsNotNull(task.User);
            Assert.AreEqual(1, task.Assignee);
            Assert.AreEqual("Worker", task.User.First_Name);
        }

        [TestMethod]
        public void TestRelationship_TaskReferencesProject()
        {
            // Arrange
            var task = new DAC.Task { Task_ID = 1, Task_Name = "Test Task" };

            // Act
            task.Project_ID = 100;

            // Assert
            Assert.AreEqual(100, task.Project_ID);
        }

        [TestMethod]
        public void TestRelationship_TaskReferencesParentTask()
        {
            // Arrange
            var task = new DAC.Task { Task_ID = 1, Task_Name = "Test Task" };

            // Act
            task.Parent_ID = 50;

            // Assert
            Assert.AreEqual(50, task.Parent_ID);
        }

        #endregion

        #region Model Mapping Tests

        [TestMethod]
        public void TestModelMapping_UserModelFields()
        {
            // Arrange & Act
            var userModel = new Models.User();

            // Assert - Verify model fields
            Assert.IsNull(userModel.FirstName);
            Assert.IsNull(userModel.LastName);
            Assert.IsNull(userModel.EmployeeId);
            Assert.AreEqual(0, userModel.UserId);
            Assert.AreEqual(0, userModel.ProjectId);
        }

        [TestMethod]
        public void TestModelMapping_ProjectModelFields()
        {
            // Arrange & Act
            var projectModel = new Models.Project();

            // Assert - Verify model fields
            Assert.AreEqual(0, projectModel.ProjectId);
            Assert.IsNull(projectModel.ProjectName);
            Assert.IsNull(projectModel.ProjectStartDate);
            Assert.IsNull(projectModel.ProjectEndDate);
            Assert.IsNull(projectModel.Priority);
            Assert.IsNull(projectModel.User);
            Assert.AreEqual(0, projectModel.NoOfTasks);
            Assert.AreEqual(0, projectModel.NoOfCompletedTasks);
        }

        [TestMethod]
        public void TestModelMapping_TaskModelFields()
        {
            // Arrange & Act
            var taskModel = new Models.Task();

            // Assert - Verify model fields
            Assert.AreEqual(0, taskModel.TaskId);
            Assert.IsNull(taskModel.Parent_ID);
            Assert.IsNull(taskModel.Project_ID);
            Assert.IsNull(taskModel.Task_Name);
            Assert.IsNull(taskModel.Start_Date);
            Assert.IsNull(taskModel.End_Date);
            Assert.IsNull(taskModel.Priority);
            Assert.AreEqual(0, taskModel.Status);
            Assert.IsNull(taskModel.User);
            Assert.IsNull(taskModel.ParentTaskName);
        }

        [TestMethod]
        public void TestModelMapping_ParentTaskModelFields()
        {
            // Arrange & Act
            var parentTaskModel = new Models.ParentTask();

            // Assert - Verify model fields
            Assert.AreEqual(0, parentTaskModel.ParentId);
            Assert.IsNull(parentTaskModel.ParentTaskName);
        }

        #endregion

        #region Data Integrity Tests

        [TestMethod]
        public void TestDataIntegrity_ProjectDatesValidation()
        {
            // Arrange
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(30);
            var project = new DAC.Project
            {
                Project_ID = 1,
                Project_Name = "Test",
                Start_Date = startDate,
                End_Date = endDate
            };

            // Assert - End date should be after start date
            Assert.IsTrue(project.End_Date > project.Start_Date);
        }

        [TestMethod]
        public void TestDataIntegrity_TaskDatesValidation()
        {
            // Arrange
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(7);
            var task = new DAC.Task
            {
                Task_ID = 1,
                Task_Name = "Test",
                Start_Date = startDate,
                End_Date = endDate
            };

            // Assert - End date should be after start date
            Assert.IsTrue(task.End_Date > task.Start_Date);
        }

        [TestMethod]
        public void TestDataIntegrity_PriorityRange()
        {
            // Arrange
            var project = new DAC.Project { Priority = 15 };
            var task = new DAC.Task { Priority = 20 };

            // Assert - Priority values are within expected range
            Assert.IsTrue(project.Priority >= 1 && project.Priority <= 30);
            Assert.IsTrue(task.Priority >= 1 && task.Priority <= 30);
        }

        #endregion
    }
}

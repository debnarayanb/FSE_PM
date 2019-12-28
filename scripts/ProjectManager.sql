/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4259)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [master]
GO
/****** Object:  Database [ProjectManager]    Script Date: 12/13/2019 2:43:11 PM ******/
CREATE DATABASE [ProjectManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectManager', FILENAME = N'C:\Users\Admin\Workspace\Database\ProjectManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjectManager_log', FILENAME = N'C:\Users\Admin\Workspace\Database\ProjectManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ProjectManager] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjectManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectManager] SET RECOVERY FULL 
GO
ALTER DATABASE [ProjectManager] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

EXEC sys.sp_db_vardecimal_storage_format N'ProjectManager', N'ON'
GO

USE [ProjectManager]
GO
/****** Object:  Table [dbo].[ParentTask]    Script Date: 12/13/2019 2:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentTask](
	[Parent_ID] [int] IDENTITY(1,1) NOT NULL,
	[Parent_Task_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Parent_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 12/13/2019 2:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Project_ID] [int] IDENTITY(1,1) NOT NULL,
	[Project_Name] [nvarchar](50) NULL,
	[Start Date] [datetime2](7) NULL,
	[End Date] [datetime2](7) NULL,
	[Priority] [int] NULL,
	[Manager] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Project_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 12/13/2019 2:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Task_ID] [int] IDENTITY(1,1) NOT NULL,
	[Parent_ID] [int] NULL,
	[Project_ID] [int] NULL,
	[Task_Name] [nvarchar](50) NULL,
	[Start_Date] [datetime2](7) NULL,
	[End_Date] [datetime2](7) NULL,
	[Priority] [int] NULL,
	[Status] [int] NOT NULL,
	[Assignee] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Task_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/13/2019 2:43:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[First Name] [nvarchar](50) NOT NULL,
	[Last Name] [nvarchar](50) NOT NULL,
	[Employee_ID] [nchar](10) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_User] FOREIGN KEY([Manager])
REFERENCES [dbo].[User] ([User_ID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_User]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User] FOREIGN KEY([Assignee])
REFERENCES [dbo].[User] ([User_ID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User]
GO
USE [master]
GO
ALTER DATABASE [ProjectManager] SET  READ_WRITE 
GO

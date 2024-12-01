USE [master]
GO
/****** Object:  Database [TaskManagement]    Script Date: 03/07/2024 07:26:11 ******/
CREATE DATABASE [TaskManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ManagementTask', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ManagementTask.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ManagementTask_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ManagementTask_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TaskManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaskManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaskManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaskManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaskManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaskManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaskManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaskManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaskManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaskManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaskManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaskManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaskManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaskManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaskManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaskManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TaskManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaskManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaskManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaskManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaskManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaskManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TaskManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaskManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TaskManagement] SET  MULTI_USER 
GO
ALTER DATABASE [TaskManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaskManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaskManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaskManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TaskManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TaskManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TaskManagement] SET QUERY_STORE = OFF
GO
USE [TaskManagement]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 03/07/2024 07:26:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToDoTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[FinishDate] [datetime] NULL,
	[IsDone] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [TaskManagement] SET  READ_WRITE 
GO

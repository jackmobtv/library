PRINT '' PRINT '*** Dropping Database library if found'
GO

IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases WHERE [name] = 'library')
BEGIN
	DROP DATABASE [library]
END
GO

PRINT '' PRINT '*** creating db library'
GO

CREATE DATABASE [library]
GO

PRINT '' PRINT '*** using database library'
GO

USE [library]
GO

PRINT '' PRINT '*** creating User table'
GO
CREATE TABLE [dbo].[User] (
      [UserID] [INT] IDENTITY (1000000, 1) NOT NULL
    , [FirstName] [NVARCHAR](255) NOT NULL
    , [LastName] [NVARCHAR](255) NOT NULL
    , [Email] [NVARCHAR](255) UNIQUE NOT NULL
    , [PasswordHash] [CHAR](64) NOT NULL DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e'
    , [Active] [BIT] NOT NULL DEFAULT 1

    , CONSTRAINT [pk_userID] PRIMARY KEY ([UserID] ASC)
)
-- passwordhash = 'newuser'
GO

PRINT '' PRINT '*** creating Role table'
GO
CREATE TABLE [dbo].[Role] (
      [RoleID] [INT] IDENTITY (1000000, 1) NOT NULL
    , [Name] [NVARCHAR](100) NOT NULL UNIQUE
    , [Description] [NVARCHAR](255) NOT NULL UNIQUE

    , CONSTRAINT [pk_roleID] PRIMARY KEY ([RoleID] ASC)
)
GO

PRINT '' PRINT '*** creating UserRole table'
GO
CREATE TABLE [dbo].[UserRole] (
      [UserID] [INT] NOT NULL
    , [RoleID] [INT] NOT NULL

    , CONSTRAINT [fk_UserRole_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID])
    , CONSTRAINT [fk_UserRole_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Role] ([RoleID])
    , CONSTRAINT [pk_UserID_RoleID] PRIMARY KEY ([UserID], [RoleID])
)
GO
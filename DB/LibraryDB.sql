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

PRINT '' PRINT '*** creating Publisher table'
GO
CREATE TABLE [dbo].[Publisher] (
      [PublisherID] [INT] IDENTITY (1000000, 1) NOT NULL PRIMARY KEY
    , [Name] [NVARCHAR](255) UNIQUE
)
GO

PRINT '' PRINT '*** creating Genre table'
GO
CREATE TABLE [dbo].[Genre] (
      [GenreID] [INT] IDENTITY (1000000, 1) NOT NULL PRIMARY KEY
    , [Name] [NVARCHAR](100) NOT NULL UNIQUE
    , [Description] [NVARCHAR](255) NOT NULL UNIQUE
)
GO

PRINT '' PRINT '*** creating Book table'
GO
CREATE TABLE [dbo].[Book] (
      [BookID] [INT] IDENTITY (1000000, 1) NOT NULL PRIMARY KEY
    , [GenreID] [INT] NOT NULL
    , [PublisherID] [INT] NOT NULL
    , [Name] [NVARCHAR](100)

    , CONSTRAINT [fk_Book_GenreID] FOREIGN KEY ([GenreID]) REFERENCES [Genre] ([GenreID])
    , CONSTRAINT [fk_Book_PublisherID] FOREIGN KEY ([PublisherID]) REFERENCES [Publisher] ([PublisherID])
)
GO

PRINT '' PRINT '*** creating Author table'
GO
CREATE TABLE [dbo].[Author] (
      [AuthorID] [INT] IDENTITY (1000000, 1) NOT NULL PRIMARY KEY
    , [Name] [NVARCHAR](255) NOT NULL
)
GO

PRINT '' PRINT '*** creating BookAuthor table'
GO
CREATE TABLE [dbo].[BookAuthor](
      [BookID]   [INT] NOT NULL
    , [AuthorID] [INT] NOT NULL

    , CONSTRAINT [fk_BookAuthor_BookID] FOREIGN KEY ([BookID]) REFERENCES [Book] ([BookID])
    , CONSTRAINT [fk_BookAuthor_AuthorID] FOREIGN KEY ([AuthorID]) REFERENCES [Author] ([AuthorID])
    , CONSTRAINT [pk_BookID_AuthorID] PRIMARY KEY ([BookID], [AuthorID])
)
GO

PRINT '' PRINT '*** creating User table'
GO
CREATE TABLE [dbo].[User] (
      [UserID] [INT] IDENTITY (1000000, 1) NOT NULL PRIMARY KEY
    , [FirstName] [NVARCHAR](255) NOT NULL
    , [LastName] [NVARCHAR](255) NOT NULL
    , [Email] [NVARCHAR](255) UNIQUE NOT NULL
    , [PasswordHash] [CHAR](64) NOT NULL DEFAULT '9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e'
    , [Active] [BIT] NOT NULL DEFAULT 1
)
-- passwordhash = 'newuser'
GO

PRINT '' PRINT '*** creating Role table'
GO
CREATE TABLE [dbo].[Role] (
      [RoleID] [INT] IDENTITY (1000000, 1) NOT NULL PRIMARY KEY
    , [Name] [NVARCHAR](100) NOT NULL UNIQUE
    , [Description] [NVARCHAR](255) NOT NULL UNIQUE
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

PRINT '' PRINT '*** creating Transaction table'
GO
CREATE TABLE [dbo].[Transaction] (
      [TransactionID] [INT] IDENTITY (1000000, 1) NOT NULL PRIMARY KEY
    , [UserID] [INT] NOT NULL
    , [TransactionType] [NVARCHAR](50) NOT NULL

    , CONSTRAINT [fk_Transaction_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID])
)
GO

PRINT '' PRINT '*** creating Copy table'
GO
CREATE TABLE [dbo].[Copy] (
      [CopyID] [INT] IDENTITY (1000000, 1) NOT NULL PRIMARY KEY
    , [BookID] [INT] NOT NULL
    , [Condition] [NVARCHAR](255) NOT NULL
    , [Active] [BIT] NOT NULL DEFAULT 1

    , CONSTRAINT [fk_Copy_BookID] FOREIGN KEY ([BookID]) REFERENCES [Book] ([BookID])
)
GO

PRINT '' PRINT '*** creating CopyTransaction table'
GO
CREATE TABLE [dbo].[CopyTransaction] (
      [TransactionID] [INT] NOT NULL
    , [CopyID] [INT] NOT NULL

    , CONSTRAINT [fk_CopyTransaction_TransactionID] FOREIGN KEY ([TransactionID]) REFERENCES [Transaction] ([TransactionID])
    , CONSTRAINT [fk_CopyTransaction_CopyID] FOREIGN KEY ([CopyID]) REFERENCES [Copy] ([CopyID])
    , CONSTRAINT [pk_TransactionID_CopyID] PRIMARY KEY ([TransactionID], [CopyID])
)
GO
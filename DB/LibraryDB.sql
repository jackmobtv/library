PRINT '' PRINT '*** dropping database library if found'
GO
DROP DATABASE IF EXISTS [library]
GO

PRINT '' PRINT '*** creating library database'
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
    , [Name] [NVARCHAR](255)
    , [Description] [NVARCHAR](255)

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


PRINT '' PRINT '*** Populating Publisher Table'
GO
INSERT INTO [dbo].[Publisher]
	([Name])
VALUES
	  ('Publisher1')
    , ('Publisher2')
    , ('Publisher3')
GO

PRINT '' PRINT '*** Populating Genre Table'
GO
INSERT INTO [dbo].[Genre]
	([Name], [Description])
VALUES
	  ('Horror', 'Spooky Scary Stories')
    , ('Comedy', 'Funny Books')
    , ('Non-Fiction', 'Real Stories')
GO

PRINT '' PRINT '*** Populating Book Table'
GO
INSERT INTO [dbo].[Book]
	([GenreID], [PublisherID], [Name], [Description])
VALUES
	  (1000001, 1000001, 'Birds and Me', 'Learn about birds')
    , (1000002, 1000002, 'Monty Python''s Big Red Book', 'Tis But A Scratch')
    , (1000000, 1000000, 'Eat Yourself Smart', 'Get Smarter By Eating')
    , (1000002, 1000000, 'Faster Than The Speed Of Love', 'Once you start, you can never stop reading')
    , (1000001, 1000002, 'Around The World In 80 Buffets', 'I am the Bane of the All You Can Eat Buffet')
    , (1000000, 1000001, 'My Horrible Father', 'My father is so horrible')
    , (1000002, 1000001, 'The Big Goodbye', 'GOODBYE')
GO

PRINT '' PRINT '*** Populating Author Table'
GO
INSERT INTO [dbo].[Author]
	([Name])
VALUES
	  ('Peter Ian Staker')
    , ('Revolver Ocelot')
    , ('Tracy Torme')
	, ('Morty Jr.')
GO

PRINT '' PRINT '*** Populating BookAuthor Table'
GO
INSERT INTO [dbo].[BookAuthor]
	([BookID], [AuthorID])
VALUES
	  (1000000, 1000000)
    , (1000001, 1000001)
    , (1000002, 1000002)
    , (1000003, 1000001)
    , (1000004, 1000002)
    , (1000005, 1000003)
    , (1000006, 1000002)
GO

PRINT '' PRINT '*** Populating User Table'
GO
INSERT INTO [dbo].[User]
	([FirstName], [LastName], [Email])
VALUES
	  ('Ad', 'Min', 'admin@company.com')
    , ('Big', 'Boss', 'nkdsnk123@aol.com')
    , ('Kil', 'Yu', 'fakeemail@fakedomain.net')
    , ('Book', 'Eater', 'iluveatingbooks@yahoo.com')
GO

UPDATE [User]
SET [Active] = 0
WHERE [UserID] = 1000003
GO

PRINT '' PRINT '*** Populating Role Table'
GO
INSERT INTO [dbo].[Role]
	([Name], [Description])
VALUES
	  ('Admin', 'System Admin')
    , ('Librarian', 'Librarian')
    , ('User', 'Users of the System')
GO

PRINT '' PRINT '*** Populating UserRole Table'
GO
INSERT INTO [dbo].[UserRole]
	([UserID], [RoleID])
VALUES
	  (1000000, 1000000)
    , (1000001, 1000001)
    , (1000002, 1000002)
    , (1000003, 1000002)
GO

PRINT '' PRINT '*** Populating Transaction Table'
GO
INSERT INTO [dbo].[Transaction]
	([UserID], [TransactionType])
VALUES
	  (1000002, 'Check Out')
    , (1000002, 'Check In')
    , (1000003, 'Check Out')
    , (1000003, 'Check Out')
GO

PRINT '' PRINT '*** Populating Copy Table'
GO
INSERT INTO [dbo].[Copy]
	([BookID], [Condition], [Active])
VALUES
	  (1000000, 'Good', 1)
    , (1000000, 'Spine Damage', 1)
    , (1000000, 'Excellent', 0)
    , (1000001, 'Missing Pages', 0)
    , (1000002, 'Torn Cover', 1)
    , (1000003, 'Good', 1)
    , (1000005, 'Some Torn Pages', 1)
    , (1000005, 'Poor', 1)
    , (1000006, 'Good', 1)
GO

PRINT '' PRINT '*** Populating CopyTransaction Table'
GO
INSERT INTO [dbo].[CopyTransaction]
	([TransactionID], [CopyID])
VALUES
	  (1000000, 1000001)
    , (1000001, 1000001)
    , (1000002, 1000002)
    , (1000003, 1000003)
GO

PRINT '' PRINT '*** Creating Procedure sp_select_roles_by_user_id'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_user_id]
	(
		@UserId [INT]
	)
AS
	BEGIN

		SELECT
			[Role].[Name]
		FROM [UserRole]
		JOIN [Role]
		ON [UserRole].[RoleID] = [Role].[RoleID]
		WHERE [UserRole].[UserID] = @UserID

	END
GO

PRINT '' PRINT '*** Creating Procedure sp_select_user_by_email'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
	(
		@Email [NVARCHAR] (255)
	)
AS
	BEGIN

		SELECT
			  [UserID]
			, [FirstName]
			, [LastName]
			, [Email]
			, [Active]
		FROM [dbo].[User]
		WHERE
			[Email] = @Email
				AND
			[Active] = 1

	END
GO

PRINT '' PRINT '*** Creating Procedure sp_select_user_by_email_and_password'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email_and_password]
	(
		  @Email [NVARCHAR] (255)
		, @PasswordHash [CHAR] (64)
	)
AS
	BEGIN

		SELECT
			  [UserID]
			, [FirstName]
			, [LastName]
			, [Email]
			, [Active]
		FROM [dbo].[User]
		WHERE
			[Email] = @Email
				AND
			[PasswordHash] = @PasswordHash
				AND
			[Active] = 1
	END
GO

PRINT '' PRINT '*** Creating Procedure sp_insert_user'
GO
CREATE PROCEDURE [dbo].[sp_insert_user]
(
      @FirstName [NVARCHAR] (255)
    , @LastName [NVARCHAR] (255)
    , @Email [NVARCHAR] (255)
    , @PasswordHash [CHAR] (64)
)
AS
    BEGIN TRANSACTION

        INSERT INTO [dbo].[User]
            (FirstName, LastName, Email, PasswordHash)
        VALUES
            (@FirstName, @LastName, @Email, @PasswordHash)

        INSERT INTO [dbo].[UserRole]
            (UserID, RoleID)
        VALUES
            (@@IDENTITY, 1000000)

    COMMIT TRANSACTION
GO

PRINT '' PRINT '*** Creating Procedure sp_update_user'
GO
CREATE PROCEDURE [dbo].[sp_update_user]
(
      @FirstName [NVARCHAR] (255)
    , @LastName [NVARCHAR] (255)
    , @Old_Email [NVARCHAR] (255)
    , @Email [NVARCHAR] (255)
    , @PasswordHash [CHAR] (64)
)
AS
    BEGIN TRANSACTION

        UPDATE [dbo].[User]
        SET
              [FirstName] = @FirstName
            , [LastName] = @LastName
            , [Email] = @Email
            , [PasswordHash] = @PasswordHash
        WHERE [Email] = @Old_Email

    COMMIT TRANSACTION

    RETURN @@ROWCOUNT
GO

PRINT '' PRINT '*** Creating Procedure sp_select_all_books'
GO
CREATE PROCEDURE [dbo].[sp_select_all_books]
AS
    BEGIN

        SELECT
              [Book].[BookID]
            , [Book].[Name]
            , [Book].[Description]
            , [Genre].[Name]
            , [Author].[Name]
            , [Publisher].[Name]
        FROM [Book]
        JOIN [dbo].[Genre]
            ON [Book].[GenreID] = [Genre].[GenreID]
        JOIN [dbo].[Publisher]
            ON [Book].[PublisherID] = [Publisher].[PublisherID]
        JOIN [dbo].[BookAuthor]
            ON [Book].[BookID] = [BookAuthor].[BookID]
        JOIN [dbo].[Author]
            ON [BookAuthor].[AuthorID] = [Author].[AuthorID]
        ORDER BY [Book].[Name]

    END
GO

PRINT '' PRINT '*** Creating Procedure sp_select_copies_by_book_id'
GO
CREATE PROCEDURE [dbo].[sp_select_copies_by_book_id]
(
    @BookID INT
)
AS
    BEGIN

        SELECT
              [CopyID]
            , [BookID]
            , [Condition]
            , [Active]
        FROM [dbo].[Copy]
        WHERE [BookID] = @BookID
        ORDER BY [CopyID]

    END
GO

PRINT '' PRINT '*** Creating Procedure sp_select_all_users'
GO
CREATE PROCEDURE [dbo].[sp_select_all_users]
AS
	BEGIN
	
		SELECT
			  [UserID]
			, [FirstName]
			, [LastName]
			, [Email]
			, [Active]
		FROM [dbo].[User]
		ORDER BY [UserID]
	
	END
GO

PRINT '' PRINT '*** Creating Procedure sp_select_copy_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_copy_by_id]
(
    @CopyID INT
)
AS
    BEGIN

        SELECT
              [CopyID]
            , [BookID]
            , [Condition]
            , [Active]
        FROM [dbo].[Copy]
        WHERE [CopyID] = @CopyID

    END
GO

PRINT '' PRINT '*** Creating Procedure sp_insert_book'
GO
CREATE PROCEDURE [dbo].[sp_insert_book]
(
      @Name NVARCHAR(255)
    , @Description NVARCHAR(255)
    , @GenreID INT
    , @PublisherID INT
)
AS
    BEGIN

        INSERT INTO [dbo].[Book]
            (GenreID, PublisherID, Name, Description)
        VALUES
            (@GenreID, @PublisherID, @Name, @Description)

    END
GO

PRINT '' PRINT '*** Creating Procedure sp_update_book'
GO
CREATE PROCEDURE [dbo].[sp_update_book]
(
      @BookID INT
    , @Name NVARCHAR(255)
    , @Description NVARCHAR(255)
    , @GenreID INT
    , @PublisherID INT
    , @Name_Old NVARCHAR(255)
    , @Description_Old NVARCHAR(255)
    , @GenreID_Old INT
    , @PublisherID_Old INT
)
AS
    BEGIN

        UPDATE [dbo].[Book]
        SET
              [Name] = @Name
            , [Description] = @Description
            , [GenreID] = @GenreID
            , [PublisherID] = @PublisherID
        WHERE
            [BookID] = @BookID
                AND
            [Name] = @Name_Old
                AND
            [Description] = @Description_Old
                AND
            [GenreID] = @GenreID_Old
                AND
            [PublisherID] = @PublisherID_Old

    END
GO

PRINT '' PRINT '*** Creating Procedure sp_insert_copy'
GO
CREATE PROCEDURE [dbo].[sp_insert_copy]
(
      @BookID INT
    , @Condition NVARCHAR(255)
)
AS
    BEGIN

        INSERT INTO [dbo].[Copy]
            (BookID, Condition)
        VALUES
            (@BookID, @Condition)

    END
GO

PRINT '' PRINT '*** Creating Procedure sp_update_copy'
GO
CREATE PROCEDURE [dbo].[sp_update_copy]
(
      @CopyID INT
    , @Condition NVARCHAR(255)
    , @Condition_Old NVARCHAR(255)
)
AS
    BEGIN

        UPDATE [dbo].[Copy]
        SET
            [Condition] = @Condition
        WHERE
            [CopyID] = @CopyID
                AND
            [Condition] = @Condition_Old

    END
GO

PRINT '' PRINT '*** Create Procedure sp_deactivate_copy'
GO
CREATE PROCEDURE [dbo].[sp_deactivate_copy]
(
    @CopyID INT
)
AS
    BEGIN

        UPDATE [dbo].[Copy]
        SET
            [Active] = 0
        WHERE [CopyID] = @CopyID

    END
GO

PRINT '' PRINT '*** Create Procedure sp_activate_copy'
GO
CREATE PROCEDURE [dbo].[sp_activate_copy]
(
    @CopyID INT
)
AS
    BEGIN

        UPDATE [dbo].[Copy]
        SET
            [Active] = 1
        WHERE [CopyID] = @CopyID

    END
GO

PRINT '' PRINT '*** Create Procedure sp_select_all_genres'
GO
CREATE PROCEDURE [dbo].[sp_select_all_genres]
AS
    BEGIN

        SELECT
              [GenreID]
            , [Name]
            , [Description]
        FROM [dbo].[Genre]

    END
GO

PRINT '' PRINT '*** Create Procedure sp_insert_genre'
GO
CREATE PROCEDURE [dbo].[sp_insert_genre]
(
      @Name NVARCHAR(100)
    , @Description NVARCHAR(255)
)
AS
    BEGIN

        INSERT INTO [dbo].[Genre]
            (Name, Description)
        VALUES
            (@Name, @Description)

    END
GO

PRINT '' PRINT '*** Create Procedure sp_select_all_publishers'
GO
CREATE PROCEDURE [dbo].[sp_select_all_publishers]
AS
    BEGIN

        SELECT
              [PublisherID]
            , [Name]
        FROM [dbo].[Publisher]

    END
GO

PRINT '' PRINT '*** Create Procedure sp_insert_publisher'
GO
CREATE PROCEDURE [dbo].[sp_insert_publisher]
(
    @Name NVARCHAR(100)
)
AS
    BEGIN

        INSERT INTO [dbo].[Publisher]
            (Name)
        VALUES
            (@Name)

    END
GO

PRINT '' PRINT '*** Create Procedure sp_select_book_by_id'
GO
CREATE PROCEDURE [dbo].[sp_select_book_by_id]
(
	@BookID INT
)
AS
	BEGIN
	
		SELECT
			  [Book].[BookID]
            , [Name]
            , [Description]
            , [GenreID]
            , [AuthorID]
            , [PublisherID]
		FROM [Book]
		JOIN [BookAuthor]
			ON [Book].[BookID] = [BookAuthor].[BookID]
		WHERE [Book].[BookID] = @BookID
	
	END
GO

PRINT '' PRINT '*** Creating Procedure sp_select_all_authors'
GO
CREATE PROCEDURE [dbo].[sp_select_all_authors]
AS
	BEGIN
	
		SELECT
			  [BookAuthor].[AuthorID]
			, [BookID]
			, [Name]
		FROM [BookAuthor]
		JOIN [Author] ON [BookAuthor].[AuthorID] = [Author].[AuthorID]
	
	END
GO

PRINT '' PRINT '*** Creating Procedure sp_insert_author'
GO
CREATE PROCEDURE [dbo].[sp_insert_author]
(
	  @Name NVARCHAR(255)
	, @BookID INT
)
AS
	BEGIN
	
		INSERT INTO [dbo].[Author]
			(Name)
		VALUES
			(@Name)
		
		UPDATE [dbo].[BookAuthor]
		SET [AuthorID] = @@IDENTITY
		WHERE [BookID] = @BookID
	
	END
GO

PRINT '' PRINT '*** Creating Procedure sp_insert_bookauthor'
GO
CREATE PROCEDURE [dbo].[sp_insert_bookauthor]
(
	  @BookID INT
	, @AuthorID INT
)
AS
	BEGIN
	
		INSERT INTO [dbo].[BookAuthor]
			(BookID, AuthorID)
		VALUES
			(@BookID, @AuthorID)
	
	END
GO

PRINT '' PRINT '*** Creating Procedure sp_update_author'
GO
CREATE PROCEDURE [dbo].[sp_update_author]
(
	  @BookID INT
	, @AuthorID INT
)
AS
	BEGIN
	
		UPDATE [dbo].[BookAuthor]
		SET
			[AuthorID] = @AuthorID
		WHERE [BookID] = @BookID
	
	END
GO

PRINT '' PRINT '*** Creating Procedure sp_select_book_table'
GO
CREATE PROCEDURE [dbo].[sp_select_book_table]
AS
	BEGIN
	
		SELECT
			  [BookID]
            , [Name]
            , [Description]
            , [GenreID]
            , [PublisherID]
		FROM [Book]
	
	END
GO
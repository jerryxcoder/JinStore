CREATE TABLE [dbo].[CustomerList] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (100) NULL,
    [LastName]         NVARCHAR (100) NULL,
    [DateCreated]      DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [DateLastModified] DATETIME       DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


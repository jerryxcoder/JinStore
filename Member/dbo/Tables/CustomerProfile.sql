CREATE TABLE [dbo].[CustomerProfile] (
    [Id]           INT           NOT NULL,
    [CustomeID ]   NCHAR (10)    NULL,
    [FirstName]    NVARCHAR (50) NULL,
    [LastName]     NVARCHAR (50) NULL,
    [OrderID]      NVARCHAR (50) NULL,
    [EmailAddress] NVARCHAR (50) NULL,
    [DateOfBirth]  DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




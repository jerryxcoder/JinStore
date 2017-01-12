CREATE TABLE [dbo].[Contact] (
    [Id]      INT           NOT NULL,
    [Name]    NVARCHAR (50) NULL,
    [Email]   NVARCHAR (50) NULL,
    [Phone]   NVARCHAR (50) NULL,
    [Message] TEXT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


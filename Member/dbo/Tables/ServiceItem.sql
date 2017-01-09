CREATE TABLE [dbo].[ServiceItem] (
    [Id]       INT           NOT NULL,
    [ItemName] NVARCHAR (50) NULL,
    [Price]    DECIMAL (18)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


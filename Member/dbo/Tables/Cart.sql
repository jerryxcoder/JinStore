CREATE TABLE [dbo].[Cart] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [TicketId]      NVARCHAR (50)    NULL,
    [origin]        NVARCHAR (50)    NULL,
    [destination]   NVARCHAR (50)    NULL,
    [departureTime] NVARCHAR (50)    NULL,
    [arrivalTime]   NVARCHAR (50)    NULL,
    [dateCreated]   DATETIME         DEFAULT (getutcdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);








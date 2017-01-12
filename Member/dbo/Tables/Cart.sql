CREATE TABLE [dbo].[Cart] (
    [Id]            UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [TicketID]      NVARCHAR (50)    NULL,
    [origin]        NVARCHAR (50)    NULL,
    [destination]   NVARCHAR (50)    NULL,
    [departureTime] NVARCHAR (50)    NULL,
    [arrivalTime]   NVARCHAR (50)    NULL,
    [saleTotal]     DECIMAL (18)     NULL,
    [carrier]       NVARCHAR (50)    NULL,
    [number]        NCHAR (10)       NULL,
    [NumStops]      INT              NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);






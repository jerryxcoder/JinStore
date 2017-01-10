CREATE TABLE [dbo].[Orders] (
    [Id]                       INT           NOT NULL,
    [TicketID]                 NVARCHAR (50) NULL,
    [FirstName]                NVARCHAR (50) NULL,
    [LastName]                 NVARCHAR (50) NULL,
    [PhoneNumber]              NVARCHAR (50) NULL,
    [CreditCardNumber]         NVARCHAR (50) NULL,
    [CreditCardName]           NVARCHAR (50) NULL,
    [CVV]                      NVARCHAR (50) NULL,
    [EmailAddress]             NVARCHAR (50) NULL,
    [CreditCardExpirationDate] DATETIME      NULL,
    [origin]                   NVARCHAR (50) NULL,
    [destination]              NVARCHAR (50) NULL,
    [departureTime]            NVARCHAR (50) NULL,
    [arrivalTime]              NVARCHAR (50) NULL,
    [saleTotal]                DECIMAL (18)  NULL,
    [carrier]                  NVARCHAR (50) NULL,
    [number]                   NCHAR (10)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);




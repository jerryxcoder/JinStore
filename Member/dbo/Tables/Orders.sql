CREATE TABLE [dbo].[Orders] (
    [Id]                       INT           NOT NULL,
    [TicketID]                 NCHAR (10)    NULL,
    [FirstName]                NVARCHAR (50) NULL,
    [LastName]                 NVARCHAR (50) NULL,
    [PhoneNumber]              NVARCHAR (50) NULL,
    [CreditCardNumber]         NVARCHAR (50) NULL,
    [CreditCardName]           NVARCHAR (50) NULL,
    [CVV]                      NVARCHAR (50) NULL,
    [EmailAddress]             NVARCHAR (50) NULL,
    [CreditCardExpirationDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


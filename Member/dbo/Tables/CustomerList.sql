CREATE TABLE [dbo].[CustomerList] (
    [ID]                       INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]                NVARCHAR (100) NULL,
    [LastName]                 NVARCHAR (100) NULL,
    [DateCreated]              DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [DateLastModified]         DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [EmailAddress]             NVARCHAR (500) NOT NULL,
    [PhoneNumber]              NVARCHAR (50)  NULL,
    [CreditCardNumber]         NVARCHAR (50)  NULL,
    [CreditCardName]           NVARCHAR (50)  NULL,
    [CVV]                      NVARCHAR (50)  NULL,
    [CreditCardExpirationDate] DATETIME       NULL,
    [BillingStreet1]           NVARCHAR (50)  NULL,
    [BillingStreet2]           NVARCHAR (50)  NULL,
    [BillingCity]              NVARCHAR (50)  NULL,
    [BillingState]             NVARCHAR (50)  NULL,
    [BillingPostalCode]        NVARCHAR (50)  NULL,
    [BillingReceipient]        NVARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);




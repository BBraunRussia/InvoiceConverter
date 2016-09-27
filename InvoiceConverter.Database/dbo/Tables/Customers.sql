CREATE TABLE [dbo].[Customers] (
    [ID]            INT  IDENTITY(1,1)         NOT NULL,
    [Number]        VARCHAR (10)  NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [Subject]   VARCHAR (100) NOT NULL,
    [Recipient] VARCHAR (500) NOT NULL,
    [Body]      VARCHAR (MAX) NULL,
    [Contacts]      VARCHAR (MAX) NULL,
    [Enable]        BIT           NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([ID] ASC)
);


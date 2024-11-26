CREATE TABLE [Person].[PhoneType] (
    [PhoneTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (2)  NOT NULL,
    [Name]        NVARCHAR (30) NOT NULL,
    [SortKey]     INT           NULL,
    CONSTRAINT [PK_PhoneType] PRIMARY KEY CLUSTERED ([PhoneTypeID] ASC)
);


GO


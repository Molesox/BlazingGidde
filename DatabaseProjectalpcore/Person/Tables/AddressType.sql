CREATE TABLE [Person].[AddressType] (
    [AddressTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Code]          NVARCHAR (1)  NOT NULL,
    [Name]          NVARCHAR (30) NOT NULL,
    [SortKey]       INT           NULL,
    CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED ([AddressTypeID] ASC)
);


GO


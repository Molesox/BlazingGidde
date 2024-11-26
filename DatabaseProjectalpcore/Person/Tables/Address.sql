CREATE TABLE [Person].[Address] (
    [AddressID]     INT            IDENTITY (1, 1) NOT NULL,
    [PersonID]      INT            NOT NULL,
    [AddressTypeID] INT            NOT NULL,
    [AddressLine1]  NVARCHAR (60)  NOT NULL,
    [AddressLine2]  NVARCHAR (60)  NOT NULL,
    [AddressLine3]  NVARCHAR (60)  NOT NULL,
    [PostalCode]    NVARCHAR (20)  NOT NULL,
    [City]          NVARCHAR (50)  NOT NULL,
    [Region]        NVARCHAR (30)  NOT NULL,
    [Country]       NVARCHAR (30)  NOT NULL,
    [SortKey]       INT            NULL,
    [Remarks]       NVARCHAR (200) NOT NULL,
    [IsDefault]     BIT            NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC),
    CONSTRAINT [FK_Address_AddressType_AddressTypeID] FOREIGN KEY ([AddressTypeID]) REFERENCES [Person].[AddressType] ([AddressTypeID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Address_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person].[Person] ([PersonID]) ON DELETE CASCADE
);


GO

CREATE NONCLUSTERED INDEX [IX_Address_PersonID]
    ON [Person].[Address]([PersonID] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_Address_AddressTypeID]
    ON [Person].[Address]([AddressTypeID] ASC);


GO


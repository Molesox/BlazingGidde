CREATE TABLE [Person].[Address] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]      INT            NOT NULL,
    [AddressTypeId] INT            NOT NULL,
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
    [UpdateDate]    DATETIME2 (7)  NULL,
    [CreateDate]    DATETIME2 (7)  NOT NULL,
    [CreateUser]    NVARCHAR (256) NOT NULL,
    [UpdateUser]    NVARCHAR (256) NULL
);
GO

ALTER TABLE [Person].[Address]
    ADD CONSTRAINT [FK_Address_Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Person] ([PersonId]) ON DELETE CASCADE;
GO

ALTER TABLE [Person].[Address]
    ADD CONSTRAINT [FK_Address_AddressType_AddressTypeId] FOREIGN KEY ([AddressTypeId]) REFERENCES [Person].[AddressType] ([Id]) ON DELETE CASCADE;
GO

CREATE NONCLUSTERED INDEX [IX_Address_PersonId]
    ON [Person].[Address]([PersonId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Address_AddressTypeId]
    ON [Person].[Address]([AddressTypeId] ASC);
GO

ALTER TABLE [Person].[Address]
    ADD CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id] ASC);
GO


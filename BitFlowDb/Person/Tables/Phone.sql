CREATE TABLE [Person].[Phone] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]    INT            NOT NULL,
    [PhoneTypeId] INT            NOT NULL,
    [PhoneNumber] NVARCHAR (30)  NOT NULL,
    [SortKey]     INT            NULL,
    [Remarks]     NVARCHAR (200) NOT NULL,
    [IsDefault]   BIT            NOT NULL,
    [UpdateDate]  DATETIME2 (7)  NULL,
    [CreateDate]  DATETIME2 (7)  NOT NULL,
    [CreateUser]  NVARCHAR (256) NOT NULL,
    [UpdateUser]  NVARCHAR (256) NULL
);
GO

ALTER TABLE [Person].[Phone]
    ADD CONSTRAINT [FK_Phone_Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Person] ([PersonId]) ON DELETE CASCADE;
GO

ALTER TABLE [Person].[Phone]
    ADD CONSTRAINT [FK_Phone_PhoneType_PhoneTypeId] FOREIGN KEY ([PhoneTypeId]) REFERENCES [Person].[PhoneType] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Person].[Phone]
    ADD CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Phone_PhoneTypeId]
    ON [Person].[Phone]([PhoneTypeId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Phone_PersonId]
    ON [Person].[Phone]([PersonId] ASC);
GO


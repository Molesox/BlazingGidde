CREATE TABLE [Person].[Phone] (
    [PhoneID]     INT            IDENTITY (1, 1) NOT NULL,
    [PersonID]    INT            NOT NULL,
    [PhoneTypeID] INT            NOT NULL,
    [PhoneNumber] NVARCHAR (30)  NOT NULL,
    [SortKey]     INT            NULL,
    [Remarks]     NVARCHAR (200) NOT NULL,
    [IsDefault]   BIT            NOT NULL,
    CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED ([PhoneID] ASC),
    CONSTRAINT [FK_Phone_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person].[Person] ([PersonID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Phone_PhoneType_PhoneTypeID] FOREIGN KEY ([PhoneTypeID]) REFERENCES [Person].[PhoneType] ([PhoneTypeID]) ON DELETE CASCADE
);


GO

CREATE NONCLUSTERED INDEX [IX_Phone_PersonID]
    ON [Person].[Phone]([PersonID] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_Phone_PhoneTypeID]
    ON [Person].[Phone]([PhoneTypeID] ASC);


GO


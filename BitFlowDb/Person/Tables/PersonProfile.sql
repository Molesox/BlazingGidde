CREATE TABLE [Person].[PersonProfile] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [PersonId]  INT             NOT NULL,
    [Photo]     VARBINARY (MAX) NULL,
    [BirthDate] DATE            NULL,
    [Gender]    INT             NOT NULL
);
GO

ALTER TABLE [Person].[PersonProfile]
    ADD CONSTRAINT [PK_PersonProfile] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_PersonProfile_PersonId]
    ON [Person].[PersonProfile]([PersonId] ASC);
GO

ALTER TABLE [Person].[PersonProfile]
    ADD CONSTRAINT [FK_PersonProfile_Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Person] ([PersonId]) ON DELETE CASCADE;
GO


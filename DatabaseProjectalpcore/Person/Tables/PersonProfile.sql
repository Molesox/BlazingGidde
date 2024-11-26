CREATE TABLE [Person].[PersonProfile] (
    [PersonProfileID] INT             IDENTITY (1, 1) NOT NULL,
    [PersonID]        INT             NOT NULL,
    [Photo]           VARBINARY (MAX) NOT NULL,
    [BirthDate]       DATE            NULL,
    [Gender]          INT             NOT NULL,
    CONSTRAINT [PK_PersonProfile] PRIMARY KEY CLUSTERED ([PersonProfileID] ASC),
    CONSTRAINT [FK_PersonProfile_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person].[Person] ([PersonID]) ON DELETE CASCADE
);


GO

CREATE NONCLUSTERED INDEX [IX_PersonProfile_PersonID]
    ON [Person].[PersonProfile]([PersonID] ASC);


GO


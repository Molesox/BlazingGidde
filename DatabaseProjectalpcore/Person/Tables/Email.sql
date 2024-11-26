CREATE TABLE [Person].[Email] (
    [EmailID]      INT            IDENTITY (1, 1) NOT NULL,
    [PersonID]     INT            NOT NULL,
    [EmailTypeID]  INT            NOT NULL,
    [EmailAddress] NVARCHAR (255) NOT NULL,
    [SortKey]      INT            NULL,
    [Remarks]      NVARCHAR (200) NOT NULL,
    [IsDefault]    BIT            NOT NULL,
    CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED ([EmailID] ASC),
    CONSTRAINT [FK_Email_EmailType_EmailTypeID] FOREIGN KEY ([EmailTypeID]) REFERENCES [Person].[EmailType] ([EmailTypeID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Email_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person].[Person] ([PersonID]) ON DELETE CASCADE
);


GO

CREATE NONCLUSTERED INDEX [IX_Email_PersonID]
    ON [Person].[Email]([PersonID] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_Email_EmailTypeID]
    ON [Person].[Email]([EmailTypeID] ASC);


GO


CREATE TABLE [Person].[Email] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]     INT            NOT NULL,
    [EmailTypeId]  INT            NOT NULL,
    [EmailAddress] NVARCHAR (255) NOT NULL,
    [SortKey]      INT            NULL,
    [Remarks]      NVARCHAR (200) NOT NULL,
    [IsDefault]    BIT            NOT NULL,
    [UpdateDate]   DATETIME2 (7)  NULL,
    [CreateDate]   DATETIME2 (7)  NOT NULL,
    [CreateUser]   NVARCHAR (256) NOT NULL,
    [UpdateUser]   NVARCHAR (256) NULL
);
GO

CREATE NONCLUSTERED INDEX [IX_Email_PersonId]
    ON [Person].[Email]([PersonId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Email_EmailTypeId]
    ON [Person].[Email]([EmailTypeId] ASC);
GO

ALTER TABLE [Person].[Email]
    ADD CONSTRAINT [PK_Email] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [Person].[Email]
    ADD CONSTRAINT [FK_Email_Person_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Person].[Person] ([PersonId]) ON DELETE CASCADE;
GO

ALTER TABLE [Person].[Email]
    ADD CONSTRAINT [FK_Email_EmailType_EmailTypeId] FOREIGN KEY ([EmailTypeId]) REFERENCES [Person].[EmailType] ([Id]) ON DELETE CASCADE;
GO


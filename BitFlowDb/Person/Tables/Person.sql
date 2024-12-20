CREATE TABLE [Person].[Person] (
    [PersonId]          INT             IDENTITY (1, 1) NOT NULL,
    [PersonTypeId]      INT             NOT NULL,
    [Culture]           NVARCHAR (20)   NOT NULL,
    [Title]             NVARCHAR (80)   NOT NULL,
    [LastName]          NVARCHAR (80)   NOT NULL,
    [FirstName]         NVARCHAR (80)   NOT NULL,
    [VatNumber]         NVARCHAR (20)   NULL,
    [Remarks]           NVARCHAR (200)  NULL,
    [AnnualRevenue]     DECIMAL (18, 2) NULL,
    [ApplicationUserId] NVARCHAR (450)  NULL
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Person_ApplicationUserId]
    ON [Person].[Person]([ApplicationUserId] ASC) WHERE ([ApplicationUserId] IS NOT NULL);
GO

CREATE NONCLUSTERED INDEX [IX_Person_PersonTypeId]
    ON [Person].[Person]([PersonTypeId] ASC);
GO

ALTER TABLE [Person].[Person]
    ADD CONSTRAINT [FK_Person_FlowUser_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [FlowCheck].[FlowUser] ([Id]);
GO

ALTER TABLE [Person].[Person]
    ADD CONSTRAINT [FK_Person_PersonType_PersonTypeId] FOREIGN KEY ([PersonTypeId]) REFERENCES [Person].[PersonType] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Person].[Person]
    ADD CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO


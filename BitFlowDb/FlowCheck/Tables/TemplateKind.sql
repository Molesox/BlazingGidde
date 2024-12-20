CREATE TABLE [FlowCheck].[TemplateKind] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (MAX) NOT NULL,
    [DefaultItemCount] INT            NOT NULL,
    [TemplateTypeId]   INT            NOT NULL,
    [Version]          INT            NOT NULL,
    [TemplateCode]     INT            NOT NULL,
    [UpdateDate]       DATETIME2 (7)  NULL,
    [CreateDate]       DATETIME2 (7)  NOT NULL,
    [CreateUser]       NVARCHAR (256) NOT NULL,
    [UpdateUser]       NVARCHAR (256) NULL
);
GO

ALTER TABLE [FlowCheck].[TemplateKind]
    ADD CONSTRAINT [PK_TemplateKind] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_TemplateKind_TemplateTypeId]
    ON [FlowCheck].[TemplateKind]([TemplateTypeId] ASC);
GO

ALTER TABLE [FlowCheck].[TemplateKind]
    ADD CONSTRAINT [FK_TemplateKind_TemplateType_TemplateTypeId] FOREIGN KEY ([TemplateTypeId]) REFERENCES [FlowCheck].[TemplateType] ([Id]) ON DELETE CASCADE;
GO


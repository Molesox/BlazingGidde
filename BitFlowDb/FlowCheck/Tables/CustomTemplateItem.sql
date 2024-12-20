CREATE TABLE [FlowCheck].[CustomTemplateItem] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [ImageUrl]       NVARCHAR (MAX) NULL,
    [TemplateKindId] INT            NOT NULL,
    [TemplateCode]   INT            NOT NULL
);
GO

ALTER TABLE [FlowCheck].[CustomTemplateItem]
    ADD CONSTRAINT [PK_CustomTemplateItem] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [FlowCheck].[CustomTemplateItem]
    ADD CONSTRAINT [FK_CustomTemplateItem_TemplateKind_TemplateKindId] FOREIGN KEY ([TemplateKindId]) REFERENCES [FlowCheck].[TemplateKind] ([Id]) ON DELETE CASCADE;
GO

CREATE NONCLUSTERED INDEX [IX_CustomTemplateItem_TemplateKindId]
    ON [FlowCheck].[CustomTemplateItem]([TemplateKindId] ASC);
GO


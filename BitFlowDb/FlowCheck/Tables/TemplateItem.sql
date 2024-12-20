CREATE TABLE [FlowCheck].[TemplateItem] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Success]      BIT            NULL,
    [Traceability] NVARCHAR (MAX) NULL,
    [Line]         NVARCHAR (MAX) NULL,
    [TemplateId]   INT            NOT NULL,
    [UpdateDate]   DATETIME2 (7)  NULL,
    [CreateDate]   DATETIME2 (7)  NOT NULL,
    [CreateUser]   NVARCHAR (256) NOT NULL,
    [UpdateUser]   NVARCHAR (256) NULL
);
GO

ALTER TABLE [FlowCheck].[TemplateItem]
    ADD CONSTRAINT [PK_TemplateItem] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_TemplateItem_TemplateId]
    ON [FlowCheck].[TemplateItem]([TemplateId] ASC);
GO

ALTER TABLE [FlowCheck].[TemplateItem]
    ADD CONSTRAINT [FK_TemplateItem_Template_TemplateId] FOREIGN KEY ([TemplateId]) REFERENCES [FlowCheck].[Template] ([Id]) ON DELETE CASCADE;
GO


CREATE TABLE [FlowCheck].[Template] (
    [Id]             INT            DEFAULT (NEXT VALUE FOR [TemplateSequence]) NOT NULL,
    [FlowUserId]     NVARCHAR (450) NULL,
    [TemplateKindId] INT            NULL,
    [UpdateDate]     DATETIME2 (7)  NULL,
    [CreateDate]     DATETIME2 (7)  NOT NULL,
    [CreateUser]     NVARCHAR (256) NOT NULL,
    [UpdateUser]     NVARCHAR (256) NULL
);
GO

CREATE NONCLUSTERED INDEX [IX_Template_TemplateKindId]
    ON [FlowCheck].[Template]([TemplateKindId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_Template_FlowUserId]
    ON [FlowCheck].[Template]([FlowUserId] ASC);
GO

ALTER TABLE [FlowCheck].[Template]
    ADD CONSTRAINT [FK_Template_FlowUser_FlowUserId] FOREIGN KEY ([FlowUserId]) REFERENCES [FlowCheck].[FlowUser] ([Id]);
GO

ALTER TABLE [FlowCheck].[Template]
    ADD CONSTRAINT [FK_Template_TemplateKind_TemplateKindId] FOREIGN KEY ([TemplateKindId]) REFERENCES [FlowCheck].[TemplateKind] ([Id]);
GO

ALTER TABLE [FlowCheck].[Template]
    ADD CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED ([Id] ASC);
GO


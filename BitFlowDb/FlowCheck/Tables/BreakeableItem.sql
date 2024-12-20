CREATE TABLE [FlowCheck].[BreakeableItem] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL
);
GO

ALTER TABLE [FlowCheck].[BreakeableItem]
    ADD CONSTRAINT [PK_BreakeableItem] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [FlowCheck].[BreakeableItem]
    ADD CONSTRAINT [FK_BreakeableItem_TemplateItem_Id] FOREIGN KEY ([Id]) REFERENCES [FlowCheck].[TemplateItem] ([Id]) ON DELETE CASCADE;
GO


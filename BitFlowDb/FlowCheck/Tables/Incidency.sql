CREATE TABLE [FlowCheck].[Incidency] (
    [Id]                  INT            NOT NULL,
    [IsQualityAdvised]    BIT            NULL,
    [IsMaintenancAdvised] BIT            NULL,
    [Signature]           NVARCHAR (MAX) NOT NULL,
    [CorrectiveActions]   NVARCHAR (MAX) NOT NULL
);
GO

ALTER TABLE [FlowCheck].[Incidency]
    ADD CONSTRAINT [FK_Incidency_TemplateItem_Id] FOREIGN KEY ([Id]) REFERENCES [FlowCheck].[TemplateItem] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [FlowCheck].[Incidency]
    ADD CONSTRAINT [PK_Incidency] PRIMARY KEY CLUSTERED ([Id] ASC);
GO


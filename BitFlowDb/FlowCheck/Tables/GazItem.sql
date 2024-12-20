CREATE TABLE [FlowCheck].[GazItem] (
    [Id]         INT             NOT NULL,
    [O2]         DECIMAL (18, 2) NOT NULL,
    [N2]         DECIMAL (18, 2) NOT NULL,
    [CO2]        DECIMAL (18, 2) NOT NULL,
    [IsGasOk]    BIT             NULL,
    [IsSealedOk] BIT             NULL
);
GO

ALTER TABLE [FlowCheck].[GazItem]
    ADD CONSTRAINT [PK_GazItem] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [FlowCheck].[GazItem]
    ADD CONSTRAINT [FK_GazItem_TemplateItem_Id] FOREIGN KEY ([Id]) REFERENCES [FlowCheck].[TemplateItem] ([Id]) ON DELETE CASCADE;
GO


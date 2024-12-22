CREATE TABLE [FlowCheck].[FlowRole] (
    [Id] NVARCHAR (450) NOT NULL
);
GO

ALTER TABLE [FlowCheck].[FlowRole]
    ADD CONSTRAINT [PK_FlowRole] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [FlowCheck].[FlowRole]
    ADD CONSTRAINT [FK_FlowRole_AspNetRoles_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE;
GO


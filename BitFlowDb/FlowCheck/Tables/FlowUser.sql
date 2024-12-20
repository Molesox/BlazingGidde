CREATE TABLE [FlowCheck].[FlowUser] (
    [Id]         NVARCHAR (450) NOT NULL,
    [UpdateDate] DATETIME2 (7)  NULL,
    [CreateDate] DATETIME2 (7)  NOT NULL,
    [CreateUser] NVARCHAR (256) NOT NULL,
    [UpdateUser] NVARCHAR (256) NULL
);
GO

ALTER TABLE [FlowCheck].[FlowUser]
    ADD CONSTRAINT [FK_FlowUser_AspNetUsers_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [FlowCheck].[FlowUser]
    ADD CONSTRAINT [PK_FlowUser] PRIMARY KEY CLUSTERED ([Id] ASC);
GO


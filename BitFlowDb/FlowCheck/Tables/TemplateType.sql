CREATE TABLE [FlowCheck].[TemplateType] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX) NOT NULL,
    [ImgUrl]     NVARCHAR (MAX) NOT NULL,
    [Code]       INT            NOT NULL,
    [UpdateDate] DATETIME2 (7)  NULL,
    [CreateDate] DATETIME2 (7)  NOT NULL,
    [CreateUser] NVARCHAR (256) NOT NULL,
    [UpdateUser] NVARCHAR (256) NULL
);
GO

ALTER TABLE [FlowCheck].[TemplateType]
    ADD CONSTRAINT [PK_TemplateType] PRIMARY KEY CLUSTERED ([Id] ASC);
GO


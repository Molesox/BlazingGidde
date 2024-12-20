CREATE TABLE [Person].[AddressType] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Code]       NVARCHAR (1)   NOT NULL,
    [Name]       NVARCHAR (30)  NOT NULL,
    [SortKey]    INT            NULL,
    [UpdateDate] DATETIME2 (7)  NULL,
    [CreateDate] DATETIME2 (7)  NOT NULL,
    [CreateUser] NVARCHAR (256) NOT NULL,
    [UpdateUser] NVARCHAR (256) NULL
);
GO

ALTER TABLE [Person].[AddressType]
    ADD CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED ([Id] ASC);
GO


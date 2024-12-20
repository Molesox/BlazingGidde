CREATE TABLE [Patois].[DictionaryEntry] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Parent]         NVARCHAR (1)   NOT NULL,
    [FrenchWord]     NVARCHAR (100) NULL,
    [DialectWord]    NVARCHAR (100) NULL,
    [FrenchExample]  NVARCHAR (500) NULL,
    [DialectExample] NVARCHAR (500) NULL,
    [AudioId]        NVARCHAR (MAX) NULL,
    [Created]        DATETIME2 (7)  NOT NULL,
    [CreatedVisa]    NVARCHAR (100) NULL,
    [Updated]        DATETIME2 (7)  NULL,
    [UpdatedVisa]    NVARCHAR (100) NULL
);
GO

ALTER TABLE [Patois].[DictionaryEntry]
    ADD CONSTRAINT [PK_DictionaryEntry] PRIMARY KEY CLUSTERED ([Id] ASC);
GO


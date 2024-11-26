CREATE TABLE [Patois].[DictionaryEntry] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Parent]         NVARCHAR (1)   NOT NULL,
    [FrenchWord]     NVARCHAR (100) NOT NULL,
    [DialectWord]    NVARCHAR (100) NOT NULL,
    [FrenchExample]  NVARCHAR (500) NOT NULL,
    [DialectExample] NVARCHAR (500) NOT NULL,
    [AudioId]        NVARCHAR (MAX) NOT NULL,
    [Created]        DATETIME2 (7)  NOT NULL,
    [CreatedVisa]    NVARCHAR (100) NOT NULL,
    [Updated]        DATETIME2 (7)  NULL,
    [UpdatedVisa]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_DictionaryEntry] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO


CREATE TABLE [Person].[EmailType] (
    [EmailTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Code]        NVARCHAR (1)  NOT NULL,
    [Name]        NVARCHAR (30) NOT NULL,
    [SortKey]     INT           NULL,
    CONSTRAINT [PK_EmailType] PRIMARY KEY CLUSTERED ([EmailTypeID] ASC)
);


GO


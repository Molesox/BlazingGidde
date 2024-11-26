CREATE TABLE [Person].[Person] (
    [PersonID]      INT             IDENTITY (1, 1) NOT NULL,
    [Culture]       NVARCHAR (20)   NOT NULL,
    [Title]         NVARCHAR (80)   NOT NULL,
    [LastName]      NVARCHAR (80)   NOT NULL,
    [FirstName]     NVARCHAR (80)   NOT NULL,
    [VatNumber]     NVARCHAR (20)   NULL,
    [Remarks]       NVARCHAR (200)  NULL,
    [AnnualRevenue] DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([PersonID] ASC)
);


GO


CREATE TABLE [dbo].[tblLabTests] (
    [id]             INT          IDENTITY (1, 1) NOT NULL,
    [biodata]        INT          NOT NULL,
    [method]         INT          NOT NULL,
    [interpretation] INT          CONSTRAINT [DF_tblLabTests_interpretation] DEFAULT ((97)) NOT NULL,
    [testing_date]   DATE         NULL,
    [testing_time]   TIME (0)     NULL,
    [reporting_date] DATE         NULL,
    [reporting_time] TIME (0)     NULL,
    [insert_time]    DATETIME     CONSTRAINT [DF_tblLabTests_insert_time] DEFAULT (getdate()) NOT NULL,
    [insert_by]      VARCHAR (50) NOT NULL,
    [update_time]    DATETIME     CONSTRAINT [DF_tblLabTests_update_time] DEFAULT (getdate()) NOT NULL,
    [update_by]      VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tblLabTests] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblLabTests_tblBiodata] FOREIGN KEY ([biodata]) REFERENCES [dbo].[tblBiodata] ([id]),
    CONSTRAINT [FK_tblLabTests_tlkpTestMethods] FOREIGN KEY ([method]) REFERENCES [dbo].[tlkpTestMethods] ([id])
);


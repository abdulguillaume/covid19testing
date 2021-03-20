CREATE TABLE [dbo].[tlkpTestMethods] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [methodname]  VARCHAR (50) NOT NULL,
    [insert_time] DATETIME     CONSTRAINT [DF_tlkpTestMethod_insert_time] DEFAULT (getdate()) NOT NULL,
    [insert_by]   VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tlkpTestMethod] PRIMARY KEY CLUSTERED ([id] ASC)
);


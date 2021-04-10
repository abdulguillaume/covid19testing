CREATE TABLE [dbo].[tblLabTestsIndicatorsValues] (
    [id]              INT             IDENTITY (1, 1) NOT NULL,
    [labtest]         INT             NOT NULL,
    [indicator]       INT             NOT NULL,
    [method]          INT             NOT NULL,
    [indicator_value] DECIMAL (18, 2) NULL,
    [insert_time]     DATETIME        CONSTRAINT [DF_tblLabTestsIndicatorsValues_insert_time] DEFAULT (getdate()) NULL,
    [insert_by]       VARCHAR (50)    NULL,
    [update_time]     DATETIME        CONSTRAINT [DF_tblLabTestsIndicatorsValues_update_time] DEFAULT (getdate()) NULL,
    [update_by]       VARCHAR (50)    NULL,
    CONSTRAINT [PK_tblLabTestsIndicatorsValues] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblLabTestsIndicatorsValues_tblLabTests] FOREIGN KEY ([labtest]) REFERENCES [dbo].[tblLabTests] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_tblLabTestsIndicatorsValues_tlkpTestIndicators] FOREIGN KEY ([indicator], [method]) REFERENCES [dbo].[tlkpTestIndicators] ([id], [method])
);




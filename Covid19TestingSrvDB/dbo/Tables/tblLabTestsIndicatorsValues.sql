CREATE TABLE [dbo].[tblLabTestsIndicatorsValues] (
    [id]              INT             NOT NULL,
    [labtest]         INT             NOT NULL,
    [indicator]       INT             NOT NULL,
    [method]          INT             NOT NULL,
    [indicator_value] DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tblLabTestsIndicatorsValues] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblLabTestsIndicatorsValues_tblLabTests] FOREIGN KEY ([labtest]) REFERENCES [dbo].[tblLabTests] ([id]),
    CONSTRAINT [FK_tblLabTestsIndicatorsValues_tlkpTestIndicators] FOREIGN KEY ([indicator], [method]) REFERENCES [dbo].[tlkpTestIndicators] ([id], [method])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblLabTestsIndicatorsValues_indicator_method]
    ON [dbo].[tblLabTestsIndicatorsValues]([indicator] ASC, [method] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblLabTestsIndicatorsValues_labtest]
    ON [dbo].[tblLabTestsIndicatorsValues]([labtest] ASC);


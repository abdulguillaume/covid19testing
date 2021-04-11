CREATE TABLE [dbo].[tblLabTestsSpecimen] (
    [id]             INT          NOT NULL,
    [labtest]        INT          NOT NULL,
    [specimen]       INT          NOT NULL,
    [specimen_other] VARCHAR (50) NULL,
    [checked]        BIT          NOT NULL,
    CONSTRAINT [PK_tblLabTestsSpecimen] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblLabTestsSpecimen_tblLabTests] FOREIGN KEY ([labtest]) REFERENCES [dbo].[tblLabTests] ([id]),
    CONSTRAINT [FK_tblLabTestsSpecimen_tlkpSpecimen] FOREIGN KEY ([specimen]) REFERENCES [dbo].[tlkpSpecimen] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblLabTestsSpecimen]
    ON [dbo].[tblLabTestsSpecimen]([labtest] ASC, [specimen] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblLabTestsSpecimen_specimen]
    ON [dbo].[tblLabTestsSpecimen]([specimen] ASC);


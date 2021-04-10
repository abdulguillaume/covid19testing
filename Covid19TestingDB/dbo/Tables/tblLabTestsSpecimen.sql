CREATE TABLE [dbo].[tblLabTestsSpecimen] (
    [id]             INT          IDENTITY (1, 1) NOT NULL,
    [labtest]        INT          NOT NULL,
    [specimen]       INT          NOT NULL,
    [specimen_other] VARCHAR (50) NULL,
    [insert_time]    DATETIME     CONSTRAINT [DF_tblLabTestsSpecimen_insert_time] DEFAULT (getdate()) NULL,
    [insert_by]      VARCHAR (50) NULL,
    [update_time]    DATETIME     CONSTRAINT [DF_tblLabTestsSpecimen_update_time] DEFAULT (getdate()) NULL,
    [update_by]      VARCHAR (50) NULL,
    [checked]        BIT          CONSTRAINT [DF_tblLabTestsSpecimen_checked] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_tblLabTestsSpecimen] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblLabTestsSpecimen_tblLabTests] FOREIGN KEY ([labtest]) REFERENCES [dbo].[tblLabTests] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_tblLabTestsSpecimen_tlkpSpecimen] FOREIGN KEY ([specimen]) REFERENCES [dbo].[tlkpSpecimen] ([id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblLabTestsSpecimen]
    ON [dbo].[tblLabTestsSpecimen]([labtest] ASC, [specimen] ASC);


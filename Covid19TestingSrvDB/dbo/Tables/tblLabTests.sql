CREATE TABLE [dbo].[tblLabTests] (
    [id]             INT      NOT NULL,
    [Biodata]        INT      NOT NULL,
    [method]         INT      NOT NULL,
    [interpretation] INT      NOT NULL,
    [testing_date]   DATE     NULL,
    [testing_time]   TIME (0) NULL,
    [reporting_date] DATE     NULL,
    [reporting_time] TIME (0) NULL,
    [transfer_time]  DATETIME CONSTRAINT [DF_transfer_time1] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_tblLabTests] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblLabTests_tblBiodata] FOREIGN KEY ([Biodata]) REFERENCES [dbo].[tblBiodata] ([id]),
    CONSTRAINT [FK_tblLabTests_tlkpTestMethods] FOREIGN KEY ([method]) REFERENCES [dbo].[tlkpTestMethods] ([id])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblLabTests_method]
    ON [dbo].[tblLabTests]([method] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tblLabTests_Biodata]
    ON [dbo].[tblLabTests]([Biodata] ASC);


GO
create TRIGGER [dbo].[update_time_tests]
   ON [dbo].[tblLabTests]
   AFTER insert
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @today datetime = getdate();

	update b
	set b.transfer_time = @today
	from [dbo].tblLabTests b join inserted i on b.id=i.id

	

END
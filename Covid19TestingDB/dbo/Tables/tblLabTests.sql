CREATE TABLE [dbo].[tblLabTests] (
    [id]              INT          IDENTITY (1, 1) NOT NULL,
    [biodata]         INT          NOT NULL,
    [method]          INT          NOT NULL,
    [interpretation]  INT          CONSTRAINT [DF_tblLabTests_interpretation] DEFAULT ((97)) NOT NULL,
    [testing_date]    DATE         NULL,
    [testing_time]    TIME (0)     NULL,
    [reporting_date]  DATE         NULL,
    [reporting_time]  TIME (0)     NULL,
    [insert_time]     DATETIME     CONSTRAINT [DF_tblLabTests_insert_time] DEFAULT (getdate()) NULL,
    [insert_by]       VARCHAR (50) NULL,
    [update_time]     DATETIME     CONSTRAINT [DF_tblLabTests_update_time] DEFAULT (getdate()) NULL,
    [update_by]       VARCHAR (50) NULL,
    [approved]        BIT          CONSTRAINT [DF_tblLabTests_approved] DEFAULT ((0)) NULL,
    [archived]        BIT          CONSTRAINT [DF_tblLabTests_archived] DEFAULT ((0)) NULL,
    [archived_time]   DATETIME     NULL,
    [archived_by]     VARCHAR (50) NULL,
    [pushed_svr_time] DATETIME     NULL,
    [sent_email_time] DATETIME     NULL,
    [pushed_svr_by]   VARCHAR (50) NULL,
    [sent_email_by]   VARCHAR (50) NULL,
    CONSTRAINT [PK_tblLabTests] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblLabTests_tblBiodata] FOREIGN KEY ([biodata]) REFERENCES [dbo].[tblBiodata] ([id]),
    CONSTRAINT [FK_tblLabTests_tlkpTestMethods] FOREIGN KEY ([method]) REFERENCES [dbo].[tlkpTestMethods] ([id])
);




GO
CREATE TRIGGER [dbo].[update_time_tests]
   ON [dbo].[tblLabTests]
   AFTER update, insert
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @today datetime = getdate();

	update b
	set b.update_time = @today
	from [dbo].tblLabTests b join inserted i on b.id=i.id

	update v
	set v.update_time = @today
	from [dbo].tblLabTests b join inserted i on b.id=i.id
	join [dbo].[tblLabTestsIndicatorsValues] v on v.labtest=b.id

	update s
	set s.update_time = @today
	from [dbo].tblLabTests b join inserted i on b.id=i.id
	join [dbo].tblLabTestsSpecimen s on s.labtest=b.id

	if UPDATE(sent_email_by)
	begin
		update b
		set b.sent_email_time = @today
		from [dbo].tblLabTests b join inserted i on b.id=i.id
		where not(i.sent_email_by is null)
	end

	if UPDATE(pushed_svr_by)
	begin
		update b
		set b.pushed_svr_time = @today
		from [dbo].tblLabTests b join inserted i on b.id=i.id
		where not(i.pushed_svr_by is null)
	end
		
	
	if not exists (select * from deleted)
	begin


		--if UPDATE(sent_email_by)
		--begin
		--	update b
		--	set b.sent_email_time = @today
		--	from [dbo].tblLabTests b join inserted i on b.id=i.id
		--	where not(i.sent_email_by is null)
		--end

		--if UPDATE(pushed_svr_by)
		--begin
		--	update b
		--	set b.pushed_svr_time = @today
		--	from [dbo].tblLabTests b join inserted i on b.id=i.id
		--	where not(i.pushed_svr_by is null)
		--end

		update b
		set b.insert_time = @today
		from [dbo].tblLabTests b join inserted i on b.id=i.id

		update v
		set v.insert_time = @today
		from [dbo].tblLabTests b join inserted i on b.id=i.id
		join [dbo].[tblLabTestsIndicatorsValues] v on v.labtest=b.id

		update s
		set s.insert_time = @today
		from [dbo].tblLabTests b join inserted i on b.id=i.id
		join [dbo].tblLabTestsSpecimen s on s.labtest=b.id

	end
	
	

END
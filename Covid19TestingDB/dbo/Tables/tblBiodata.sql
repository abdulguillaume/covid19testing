CREATE TABLE [dbo].[tblBiodata] (
    [id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Fullname]            VARCHAR (200) NOT NULL,
    [Legal_gardian_name]  VARCHAR (200) NULL,
    [dateofbirth]         DATETIME      NOT NULL,
    [gender]              INT           NOT NULL,
    [epid_no]             VARCHAR (100) NULL,
    [home_phone]          VARCHAR (50)  NULL,
    [residential_address] VARCHAR (250) NOT NULL,
    [insert_time]         DATETIME      CONSTRAINT [DF_tblBiodata_insert_time] DEFAULT (getdate()) NULL,
    [insert_by]           VARCHAR (50)  NULL,
    [update_time]         DATETIME      CONSTRAINT [DF_tblBiodata_update_time] DEFAULT (getdate()) NULL,
    [update_by]           VARCHAR (50)  NULL,
    [local_phone]         VARCHAR (50)  NULL,
    [email]               VARCHAR (100) NULL,
    CONSTRAINT [PK_tblBiodata] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblBiodata_tlkpGenders] FOREIGN KEY ([gender]) REFERENCES [dbo].[tlkpGenders] ([id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblBiodata]
    ON [dbo].[tblBiodata]([epid_no] ASC);


GO
create TRIGGER [dbo].[update_time_biodata]
   ON [dbo].[tblBiodata]
   AFTER update, insert
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @today datetime = getdate();

	update b
	set b.update_time = @today
	from [dbo].[tblBiodata] b join inserted i on b.id=i.id
		
	
	if not exists (select * from deleted)
	begin
		update b
		set b.insert_time = @today
		from [dbo].[tblBiodata] b join inserted i on b.id=i.id

	end
	
	

END
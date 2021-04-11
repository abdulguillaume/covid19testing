CREATE TABLE [dbo].[tblBiodata] (
    [id]                  INT           NOT NULL,
    [Fullname]            VARCHAR (200) NOT NULL,
    [Legal_gardian_name]  VARCHAR (200) NULL,
    [dateofbirth]         DATETIME      NOT NULL,
    [gender]              INT           NOT NULL,
    [epid_no]             VARCHAR (100) NULL,
    [home_phone]          VARCHAR (50)  NULL,
    [local_phone]         VARCHAR (50)  NULL,
    [residential_address] VARCHAR (250) NOT NULL,
    [transfer_time]       DATETIME      CONSTRAINT [DF_transfer_time] DEFAULT (getdate()) NULL,
    [email]               VARCHAR (100) NULL,
    CONSTRAINT [PK_tblBiodata] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblBiodata_tlkpGenders] FOREIGN KEY ([gender]) REFERENCES [dbo].[tlkpGenders] ([id])
);


GO
CREATE NONCLUSTERED INDEX [IX_tblBiodata_gender]
    ON [dbo].[tblBiodata]([gender] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblBiodata]
    ON [dbo].[tblBiodata]([epid_no] ASC) WHERE ([epid_no] IS NOT NULL);


GO
CREATE TRIGGER [dbo].[update_time_biodata]
   ON [dbo].[tblBiodata]
   AFTER insert
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @today datetime = getdate();

	update b
	set b.transfer_time = @today
	from [dbo].[tblBiodata] b join inserted i on b.id=i.id

	

END
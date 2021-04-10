CREATE TABLE [dbo].[tblUsers] (
    [id]              INT          IDENTITY (1, 1) NOT NULL,
    [username]        VARCHAR (50) NOT NULL,
    [userrole]        INT          CONSTRAINT [DF_tblUsers_userrole] DEFAULT ((0)) NOT NULL,
    [insert_time]     DATETIME     CONSTRAINT [DF_tblUsers_insert_time] DEFAULT (getdate()) NULL,
    [lastunlock_time] DATETIME     CONSTRAINT [DF_Table_1_unlocktime] DEFAULT (getdate()) NULL,
    [lastunlocked_by] VARCHAR (50) CONSTRAINT [DF_Table_1_unlocked_by] DEFAULT ('Self') NULL,
    [lock_time]       DATETIME     CONSTRAINT [DF_Table_1_locktime] DEFAULT (NULL) NULL,
    [locked_by]       VARCHAR (50) CONSTRAINT [DF_tblUsers_locked_by] DEFAULT (NULL) NULL,
    [update_time]     DATETIME     CONSTRAINT [DF_tblUsers_update_time] DEFAULT (getdate()) NULL,
    [update_by]       VARCHAR (50) CONSTRAINT [DF_tblUsers_update_by] DEFAULT ('Self') NULL,
    CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED ([username] ASC),
    CONSTRAINT [FK_tblUsers_tblRoles] FOREIGN KEY ([userrole]) REFERENCES [dbo].[tblRoles] ([id])
);




GO
CREATE TRIGGER [dbo].[update_time_users]
   ON [dbo].tblusers
   AFTER update
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @today datetime = getdate();

	if(update(locked_by))
	begin 
		--update u set u.lock_time= @today, u.update_time = @today from [dbo].tblusers u join inserted i on u.username=i.username
	
		if exists (select 1 from inserted i where i.locked_by is null)
			update u
			set u.update_time = @today, u.lastunlock_time = @today
			from [dbo].tblusers u join inserted i on u.username=i.username
		else
			update u
			set u.update_time = @today, u.lock_time = @today
			from [dbo].tblusers u join inserted i on u.username=i.username
		
	end
	else
	begin
		update u
		set u.update_time = @today
		from [dbo].tblusers u join inserted i on u.username=i.username
	end

	

END
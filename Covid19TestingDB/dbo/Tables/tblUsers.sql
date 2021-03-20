CREATE TABLE [dbo].[tblUsers] (
    [id]              INT          IDENTITY (1, 1) NOT NULL,
    [username]        VARCHAR (50) NOT NULL,
    [userrole]        INT          CONSTRAINT [DF_tblUsers_userrole] DEFAULT ((0)) NOT NULL,
    [insert_time]     DATETIME     CONSTRAINT [DF_tblUsers_insert_time] DEFAULT (getdate()) NOT NULL,
    [lastunlock_time] DATETIME     CONSTRAINT [DF_Table_1_unlocktime] DEFAULT (getdate()) NOT NULL,
    [lastunlocked_by] VARCHAR (50) CONSTRAINT [DF_Table_1_unlocked_by] DEFAULT ('N/A') NOT NULL,
    [lock_time]       DATETIME     CONSTRAINT [DF_Table_1_locktime] DEFAULT (NULL) NULL,
    [locked_by]       VARCHAR (50) CONSTRAINT [DF_tblUsers_locked_by] DEFAULT (NULL) NULL,
    CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED ([username] ASC),
    CONSTRAINT [FK_tblUsers_tblRoles] FOREIGN KEY ([userrole]) REFERENCES [dbo].[tblRoles] ([id])
);


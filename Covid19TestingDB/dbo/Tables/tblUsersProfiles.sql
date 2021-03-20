CREATE TABLE [dbo].[tblUsersProfiles] (
    [username]    VARCHAR (50)  NOT NULL,
    [Fullname]    VARCHAR (200) NOT NULL,
    [insert_time] DATETIME      CONSTRAINT [DF_tblUsersProfiles_insert_time] DEFAULT (getdate()) NOT NULL,
    [update_time] DATETIME      CONSTRAINT [DF_tblUsersProfiles_update_time] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tblUsersProfiles] PRIMARY KEY CLUSTERED ([username] ASC),
    CONSTRAINT [FK_tblUsersProfiles_tblUsers] FOREIGN KEY ([username]) REFERENCES [dbo].[tblUsers] ([username])
);


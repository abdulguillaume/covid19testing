CREATE TABLE [dbo].[tblMailingLists] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [email]       VARCHAR (100) NOT NULL,
    [insert_time] DATETIME      CONSTRAINT [DF_tblMailingLists_insert_time] DEFAULT (getdate()) NULL,
    [insert_by]   VARCHAR (50)  NULL,
    CONSTRAINT [PK_tblMailingList] PRIMARY KEY CLUSTERED ([id] ASC)
);


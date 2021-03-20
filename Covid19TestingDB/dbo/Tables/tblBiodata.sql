CREATE TABLE [dbo].[tblBiodata] (
    [id]                  INT           IDENTITY (1, 1) NOT NULL,
    [Fullname]            VARCHAR (200) NOT NULL,
    [Legal_gardian_name]  VARCHAR (200) NULL,
    [dateofbirth]         DATE          NOT NULL,
    [gender]              INT           NOT NULL,
    [epid_no]             VARCHAR (100) NULL,
    [home_phone]          VARCHAR (50)  NOT NULL,
    [residential_address] VARCHAR (250) NOT NULL,
    [insert_time]         DATETIME      CONSTRAINT [DF_tblBiodata_insert_time] DEFAULT (getdate()) NOT NULL,
    [insert_by]           VARCHAR (50)  NOT NULL,
    [update_time]         DATETIME      CONSTRAINT [DF_tblBiodata_update_time] DEFAULT (getdate()) NOT NULL,
    [update_by]           VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_tblBiodata] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblBiodata_tlkpGenders] FOREIGN KEY ([gender]) REFERENCES [dbo].[tlkpGenders] ([id])
);


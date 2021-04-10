CREATE TABLE [dbo].[tblBiodata_deleted] (
    [id]                  INT           IDENTITY (1, 1) NOT NULL,
    [id_biodata]          INT           NOT NULL,
    [Fullname]            VARCHAR (200) NOT NULL,
    [Legal_gardian_name]  VARCHAR (200) NULL,
    [dateofbirth]         DATETIME      NOT NULL,
    [gender]              INT           NOT NULL,
    [epid_no]             VARCHAR (100) NULL,
    [home_phone]          VARCHAR (50)  NULL,
    [residential_address] VARCHAR (250) NOT NULL,
    [insert_time]         DATETIME      NULL,
    [insert_by]           VARCHAR (50)  NULL,
    [update_time]         DATETIME      NULL,
    [update_by]           VARCHAR (50)  NULL,
    [local_phone]         VARCHAR (50)  NULL,
    [email]               VARCHAR (100) NULL,
    [deleted_time]        DATETIME      NULL,
    [deleted_by]          VARCHAR (50)  NULL,
    CONSTRAINT [PK_tblBiodata_deleted] PRIMARY KEY CLUSTERED ([id] ASC)
);


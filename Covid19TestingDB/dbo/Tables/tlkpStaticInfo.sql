CREATE TABLE [dbo].[tlkpStaticInfo] (
    [id]                       INT             NOT NULL,
    [Organization]             VARCHAR (100)   NULL,
    [Unit]                     VARCHAR (100)   NULL,
    [address]                  VARCHAR (200)   NULL,
    [Mobile]                   VARCHAR (50)    NULL,
    [Email]                    VARCHAR (50)    NULL,
    [Website]                  VARCHAR (50)    NULL,
    [Lab_supervisor]           VARCHAR (100)   NULL,
    [Lab_supervisor_signature] VARBINARY (MAX) NULL,
    [miscellaneous]            VARCHAR (MAX)   NULL,
    [insert_time]              DATETIME        CONSTRAINT [DF_tlkpStaticInfo_insert_time] DEFAULT (getdate()) NOT NULL,
    [insert_by]                VARCHAR (50)    NOT NULL,
    [update_time]              DATETIME        CONSTRAINT [DF_tlkpStaticInfo_update_time] DEFAULT (getdate()) NOT NULL,
    [update_by]                VARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_tlkpStaticInfo] PRIMARY KEY CLUSTERED ([id] ASC)
);


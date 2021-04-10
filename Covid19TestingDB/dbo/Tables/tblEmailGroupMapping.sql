CREATE TABLE [dbo].[tblEmailGroupMapping] (
    [id]            INT IDENTITY (1, 1) NOT NULL,
    [email]         INT NOT NULL,
    [mailing_group] INT NULL,
    CONSTRAINT [PK_tblEmailGroupMapping] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tblEmailGroupMapping_tblMailingLists] FOREIGN KEY ([email]) REFERENCES [dbo].[tblMailingLists] ([id]),
    CONSTRAINT [FK_tblEmailGroupMapping_tlkpMailingGroups] FOREIGN KEY ([mailing_group]) REFERENCES [dbo].[tlkpMailingGroups] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblEmailGroupMapping]
    ON [dbo].[tblEmailGroupMapping]([email] ASC, [mailing_group] ASC);


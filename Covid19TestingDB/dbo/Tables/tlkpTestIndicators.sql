CREATE TABLE [dbo].[tlkpTestIndicators] (
    [id]             INT          IDENTITY (1, 1) NOT NULL,
    [method]         INT          NOT NULL,
    [indicator_name] VARCHAR (50) NOT NULL,
    [insert_time]    DATETIME     CONSTRAINT [DF_tlkpTestIndicators_insert_time] DEFAULT (getdate()) NOT NULL,
    [insert_by]      VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tlkpTestIndicators] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tlkpTestIndicators_tlkpTestMethods] FOREIGN KEY ([method]) REFERENCES [dbo].[tlkpTestMethods] ([id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tlkpTestIndicators]
    ON [dbo].[tlkpTestIndicators]([id] ASC, [method] ASC);


CREATE TABLE [dbo].[tlkpTestIndicators] (
    [id]             INT          NOT NULL,
    [method]         INT          NOT NULL,
    [indicator_name] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tlkpTestIndicators] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_tlkpTestIndicators_tlkpTestMethods] FOREIGN KEY ([method]) REFERENCES [dbo].[tlkpTestMethods] ([id]),
    CONSTRAINT [AK_tlkpTestIndicators_id_method] UNIQUE NONCLUSTERED ([id] ASC, [method] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_tlkpTestIndicators]
    ON [dbo].[tlkpTestIndicators]([id] ASC, [method] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_tlkpTestIndicators_method]
    ON [dbo].[tlkpTestIndicators]([method] ASC);


CREATE TABLE [dbo].[tlkpMailingGroups] (
    [id]         INT           NOT NULL,
    [group_name] VARCHAR (100) NULL,
    CONSTRAINT [PK_tlkpMailingGroup] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[insert_request]
	on [dbo].[tlkpMailingGroups]
   instead of  INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here

	insert into [dbo].[tlkpMailingGroups]
	select NEXT VALUE FOR [dbo].[tlkpMailingGroups_seq]
     ,[group_name] 
	 from inserted

--	INSERT INTO contacts
--(contact_id, last_name)
--VALUES
--(NEXT VALUE FOR contacts_seq, 'Smith');


END
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_delete_test]
	-- Add the parameters for the stored procedure here
	@testid int,
	@username varchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	--if (@date1 is null) set @date1 =  cast('2020-1-1' as datetime)

	
	if exists(select 1 from [dbo].[tblLabTests] where id=@testid and reporting_date is null)
	begin 
		insert into [dbo].[tblLabTests_deleted] (
			   [id_test]
			  ,[biodata]
			  ,[method]
			  ,[interpretation]
			  ,[testing_date]
			  ,[testing_time]
			  ,[reporting_date]
			  ,[reporting_time]
			  ,[insert_time]
			  ,[insert_by]
			  ,[update_time]
			  ,[update_by]
			  ,[approved]
			  ,[archived]
			  ,[archived_time]
			  ,[archived_by]
			  ,[pushed_svr_time]
			  ,[sent_email_time]
			  ,[pushed_svr_by]
			  ,[sent_email_by]
			  ,[deleted_time]
			  ,[deleted_by]
		)
		select *,getdate(), @username from [dbo].[tblLabTests] where id=@testid

		delete from  [dbo].[tblLabTests]  where id=@testid

	end
    
	
END
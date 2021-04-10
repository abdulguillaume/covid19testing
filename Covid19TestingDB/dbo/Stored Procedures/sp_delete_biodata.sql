-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_delete_biodata]
	-- Add the parameters for the stored procedure here
	@biodata int,
	@username varchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	--if (@date1 is null) set @date1 =  cast('2020-1-1' as datetime)

	
	if not exists(select 1 from [dbo].[tblLabTests] where biodata=@biodata)
	begin 

		insert into [dbo].[tblBiodata_deleted](
			   [id_biodata]
			  ,[Fullname]
			  ,[Legal_gardian_name]
			  ,[dateofbirth]
			  ,[gender]
			  ,[epid_no]
			  ,[home_phone]
			  ,[residential_address]
			  ,[insert_time]
			  ,[insert_by]
			  ,[update_time]
			  ,[update_by]
			  ,[local_phone]
			  ,[email]
			  ,[deleted_time]
			  ,[deleted_by]
		)
		select *, getdate(), @username from [dbo].[tblBiodata]  where id=@biodata


		delete from  [dbo].[tblBiodata]  where id=@biodata

	end
    
	
END
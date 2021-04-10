-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_report1]
	-- Add the parameters for the stored procedure here
	@date1 datetime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	--if (@date1 is null) set @date1 =  cast('2020-1-1' as datetime)

	
	select ISNULL(sum(A.Tested),0) [Sample Tested], ISNULL(sum(Processing),0) [Processing-In Lab], ISNULL(sum(A.Pending_Approval),0) [Pending Approval], ISNULL(sum(A.Approved),0) Approved, ISNULL(sum(A.Published),0) Published from (

	select 
	1 Tested
	,case when approved<>1 and reporting_date is null then 1 else 0 end [Processing]
	,case when approved=1 then 1 else 0 end Approved
	,case when approved<>1 and not(reporting_date is null) then 1 else 0 end Pending_Approval
	,case when not(pushed_svr_time is null) then 1 else 0 end Published
	from [dbo].[tblLabTests] t right join [dbo].[lkpInterpretation] i on i.id=t.interpretation
	where t.testing_date>=@date1

	) A 


	select B.Interpretation, sum(B.Tested) [Sample Tested], sum(B.Processing) [Processing-In Lab],  sum(B.Pending_Approval) [Pending Approval], sum(B.Approved) Approved, sum(B.Published) Published from (
	select 
	i.interpretationName [Interpretation]
	, 1 Tested
	,case when approved<>1 and reporting_date is null then 1 else 0 end [Processing]
	,case when approved=1 then 1 else 0 end Approved
	,case when approved<>1 and not(reporting_date is null) then 1 else 0 end Pending_Approval
	,case when not(pushed_svr_time is null) then 1 else 0 end Published
	from [dbo].[tblLabTests] t right join [dbo].[lkpInterpretation] i on i.id=t.interpretation
	where t.testing_date>=@date1
	) B 
	group by  B.Interpretation



	select C.[interpretationName], [M = Male],[F = Female], [I = Indeterminate], [N = Non-conforming], [TF = Transgender female], [TM = Transgender male], [U = Unknown]
	from (
	select 
	i.interpretationName 
	, g.gender
	from [dbo].[tblLabTests] tt join [dbo].[tblBiodata] b on tt.biodata=b.id
	join [dbo].[tlkpGenders] g on b.gender=g.id
	right join [dbo].[lkpInterpretation] i on i.id=tt.interpretation
	where tt.testing_date>=@date1

	) as t
	pivot 
	(
	count(gender)
	for gender in ([M = Male],[F = Female], [I = Indeterminate], [N = Non-conforming], [TF = Transgender female], [TM = Transgender male], [U = Unknown])
	) as C
    
	
END
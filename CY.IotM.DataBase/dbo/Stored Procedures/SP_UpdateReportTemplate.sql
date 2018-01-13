

Create PROCEDURE [dbo].[SP_UpdateReportTemplate]
	@RID int,
	@ReportName varchar(50),
	@ReportType smallint,
	@RD_ID int,
	@ReportTemplate image,
	@MenuName varchar(50),
	@IsUse bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if(@RID = -1)
	begin
		--新建报表
		INSERT INTO ReportTemplate
							  (ReportName, ReportType, RD_ID, ReportTemplate, MenuName, IsUse)
		VALUES     (@ReportName,@ReportType,@RD_ID,@ReportTemplate,@MenuName,@IsUse)
		select @RID = @@IDENTITY 
	end
	else
	begin
		--更新报表数据
		UPDATE    ReportTemplate
		SET       ReportName = @ReportName, ReportType = @ReportType, RD_ID = @RD_ID, ReportTemplate = @ReportTemplate, MenuName = @MenuName, 
				  IsUse = @IsUse
		WHERE     (RID = @RID)
	end
	select @RID as RID
END
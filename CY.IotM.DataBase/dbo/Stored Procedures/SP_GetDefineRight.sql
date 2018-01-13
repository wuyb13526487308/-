/*
功能：获取单位的权限组列表
exec SP_GetDefineRight ''
*/
CREATE proc [dbo].[SP_GetDefineRight]
@CompanyID char(4)
as
begin
select * from S_DefineRight where CompanyID=@CompanyID
end
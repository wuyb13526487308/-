/*
功能：获取单位下的所有权限组对应的菜单列表
exec SP_GetDefineRightMenu ''
*/
CREATE proc [dbo].[SP_GetDefineRightMenu]
@CompanyID char(4)
as
begin
select * from S_DefineRightMenu where CompanyID=@CompanyID
end
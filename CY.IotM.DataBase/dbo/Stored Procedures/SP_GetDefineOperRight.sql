
/*
功能：获取单位的所有操作员对应的权限组列表
exec SP_GetDefineOperRight ''
*/
CREATE proc [dbo].[SP_GetDefineOperRight]
@CompanyID char(4)
as
begin
select * from S_DefineOperRight where  CompanyID=@CompanyID
end




/*
功能：获取单位的菜单列表
exec SP_GetDefineMenu ''
*/
CREATE proc [dbo].[SP_GetDefineMenu]
@CompanyID char(4)
as
begin

select a.MenuCode,b.CompanyID,Name,Type,UrlClass,ImageUrl,OrderNum,FatherCode 
from S_DefineMenu a inner join S_CompanyMenu b on  a.MenuCode=b.MenuCode where b.CompanyID=@CompanyID

end
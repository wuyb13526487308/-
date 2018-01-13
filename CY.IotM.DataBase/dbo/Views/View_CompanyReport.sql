

CREATE view [dbo].[View_CompanyReport]
as
	select  b.CompanyID, 
	c.CompanyName,       
    a.MenuCode ,
    a.Name,
    a.RID from S_CompanyMenu b 
    inner join S_DefineMenu a  on a.MenuCode=b.MenuCode 
    inner join S_CompanyInfo c  on b.CompanyID=c.CompanyID 
    Where RID>=1
GO
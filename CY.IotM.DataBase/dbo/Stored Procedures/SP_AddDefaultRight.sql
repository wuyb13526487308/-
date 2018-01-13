







/*
功能：添加单位后增加默认权限
SP_AddDefaultRight '0001'

*/
CREATE proc [dbo].[SP_AddDefaultRight]
@CompanyID char(4)
as
begin

if @CompanyID='zzcy'
begin

delete from S_CompanyMenu where CompanyID=@CompanyID

insert into  S_CompanyMenu (CompanyID,MenuCode)  
select @CompanyID,MenuCode from S_DefineMenu

end 

else
begin

declare @MenuCount int
select @MenuCount=COUNT(1) from S_CompanyMenu where CompanyID=@CompanyID and MenuCode in 
(select MenuCode from S_DefineMenu where FatherCode in 
(select MenuCode from S_CompanyMenu where CompanyID=@CompanyID) and Type='02' or Type='03')

if @MenuCount=0
begin 
insert into  S_CompanyMenu (CompanyID,MenuCode)  
select @CompanyID,MenuCode from S_DefineMenu where FatherCode in 
(select MenuCode from S_CompanyMenu where CompanyID=@CompanyID) and Type='02' or Type='03'
end

end 


--增加默认权限
declare @RightCode char(10)
set @RightCode='xtgly'

delete from S_DefineRight where RightCode=@RightCode and CompanyID=@CompanyID
insert into S_DefineRight(RightCode,CompanyID,RightName,Context)
values(@RightCode,@CompanyID,'系统管理员','系统初始化时，默认增加的权限。')


delete from S_DefineRightMenu where RightCode=@RightCode and CompanyID=@CompanyID
insert into S_DefineRightMenu(CompanyID,RightCode,MenuCode)
select @CompanyID,@RightCode,MenuCode from S_CompanyMenu where CompanyID=@CompanyID



delete from S_DefineOperRight where  CompanyID=@CompanyID and RightCode=@RightCode
insert into S_DefineOperRight(CompanyID,RightCode,OperID)
values(@CompanyID,@RightCode,@CompanyID)
end


GO

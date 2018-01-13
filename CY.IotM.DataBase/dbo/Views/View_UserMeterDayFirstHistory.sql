
CREATE view [dbo].[View_UserMeterDayFirstHistory]
as
 select  a.CompanyID as CompanyID, 
         a.UserID as UserID,
         a.UserName as UserName,
         a.State as State,
         a.Address as Address,
         a.Street as Street,
         a.Community as Community,
         a.Door as Door,
         b.MeterNo as MeterNo,
         a.MeterType as MeterType,
         a.ValveState as ValveState,
         a.LastTotal as LastTotal,
         a.TotalAmount as TotalAmount,
         b.RemainingAmount as RemainingAmount,
         b.ReadDate as ReadDate,
         a.InstallDate as InstallDate,
         b.Gas as Gas
         from  dbo.IoT_DayReadMeter b 
         inner join View_UserMeter a  on a.MeterNo=b.MeterNo 
         UNION  
         select  a.CompanyID as CompanyID, 
         a.UserID as UserID,
         c.UserName as UserName,
         c.State as State,
         c.Address as Address,
         c.Street as Street,
         c.Community as Community,
         c.Door as Door,
         b.MeterNo as MeterNo,
         a.MeterType as MeterType,
         a.ValveState as ValveState,
         a.LastTotal as LastTotal,
         a.TotalAmount as TotalAmount,
         b.RemainingAmount as RemainingAmount,
         b.ReadDate as ReadDate,
         a.InstallDate as InstallDate,
         b.Gas as Gas
         from  dbo.IoT_DayReadMeter b 
         inner join  dbo.IoT_MeterHistory a  on a.MeterNo=b.MeterNo 
         inner join  dbo.IoT_User c ON c.UserID=a.UserID
GO
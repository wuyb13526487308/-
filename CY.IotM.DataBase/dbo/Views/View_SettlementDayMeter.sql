CREATE view View_SettlementDayMeter
as
 select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
         
         b.State,
         b.FinishedDate,
         b.Context,
         b.ID AS MeterID
         from dbo.IoT_SettlementDayMeter b 
         inner join View_UserMeter a  on a.MeterNo=b.MeterNo
         UNION 
         
 select DISTINCT
  a.CompanyID, 
         a.UserID,
         c.UserName,
         c.Address,
         a.MeterNo,
         
         b.State,
         b.FinishedDate,
         b.Context,
         b.ID AS MeterID
         from dbo.IoT_SettlementDayMeter b 
         INNER JOIN dbo.IoT_MeterHistory a  on a.MeterNo=b.MeterNo
         INNER JOIN dbo.IoT_User c ON a.UserID=c.UserID
         
         
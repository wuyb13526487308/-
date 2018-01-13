
CREATE view [dbo].[View_AlarmInfo]
as
 select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
         b.ReportDate,
         b.Item,
         b.MeterID,
         b.ID,
         b.AlarmValue
         from IoT_AlarmInfo b 
         inner join View_UserMeter a  on a.MeterNo=b.MeterNo
         union
        select  c.CompanyID, 
         c.UserID,
         c.UserName,
         c.Address,
         c.MeterNo,
         d.ReportDate,
         d.Item,
         d.MeterID,
         d.ID,
         d.AlarmValue
         from IoT_AlarmInfo d 
         inner join View_UserMeter c  on c.MeterNo=d.MeterNo



CREATE view [dbo].[View_AlarmMeter]
as
 select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
         b.State,
         b.FinishedDate,
         b.Context,
         b.ID
         from IoT_AlarmMeter b 
         inner join View_UserMeter a  on a.MeterNo=b.MeterNo
         UNION
 select  d.CompanyID, 
         d.UserID,
         d.UserName,
         d.Address,
         d.MeterNo,
         c.State,
         c.FinishedDate,
         c.Context,
         c.ID
         from IoT_AlarmMeter c 
         inner join View_HistoryUserMeter d  on c.MeterNo=d.MeterNo
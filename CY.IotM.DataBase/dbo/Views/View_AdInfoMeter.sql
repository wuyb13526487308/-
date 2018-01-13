

CREATE view [dbo].[View_AdInfoMeter]
as
 select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
	     c.FileName,
         b.State,
         b.FinishedDate,
         b.Context,
         b.ID
         from IoT_AdInfoMeter b 
         inner join View_UserMeter a  on a.MeterNo=b.MeterNo
		 inner join IoT_SetAdInfo c on b.ID=c.ID
		 union
		  select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
         c.FileName,
         b.State,
         b.FinishedDate,
         b.Context,
         b.ID
         from IoT_AdInfoMeter b 
         inner join View_HistoryUserMeter a  on a.MeterNo=b.MeterNo
	     inner join IoT_SetAdInfo c on b.ID=c.ID
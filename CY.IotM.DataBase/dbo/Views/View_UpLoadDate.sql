CREATE view [dbo].[View_UpLoadDate]
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
         from IoT_UploadCycleMeter b 
         inner join View_UserMeter a  on a.MeterNo=b.MeterNo
         UNION 
  SELECT DISTINCT 
  a.CompanyID, 
         a.UserID,
         c.UserName,
         c.Address,
         a.MeterNo,
         
         b.State,
         b.FinishedDate,
         b.Context,
         b.ID AS MeterID
  FROM IoT_UploadCycleMeter b 
         inner join dbo.IoT_MeterHistory a  on a.MeterNo=b.MeterNo
         INNER JOIN dbo.IoT_User AS c ON a.UserID=c.UserID
         
         
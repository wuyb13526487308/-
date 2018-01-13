CREATE view  View_Community
as 
SELECT a.ID
      ,a.StreetID
      ,a.Name CommunityName
      ,b.Name StreetName
      ,b.CompanyID
      ,(select COUNT(1) from  View_UserMeter where Community=a.ID) as Num
      ,(select COUNT(1) from  View_UserMeter where Community=a.ID AND MeterType='00') as QLBNum
      ,(select COUNT(1) from  View_UserMeter where Community=a.ID  AND MeterType='01') as JEBNum
  FROM IoT_Community  a inner join IoT_Street  b on  
  a.StreetID=b.ID
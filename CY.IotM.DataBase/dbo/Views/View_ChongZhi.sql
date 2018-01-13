CREATE view [dbo].[View_ChongZhi]
as 
SELECT ID AS AID
      ,Ser
      ,a.UserID
      ,MeterID
      ,MeterNo
      ,Amount
      ,TopUpDate
      ,TopUpType
      ,Oper
      ,OrgCode
      ,a.State
      ,a.CompanyID
      ,Context
      ,TaskID
      ,b.Address
      ,b.Community
      ,b.Door
      ,b.Phone
      ,b.Street
      ,b.UserName
  FROM IoT_MeterTopUp a 
  inner join IoT_User b
  on a.UserID=b.UserID and a.CompanyID=b.CompanyID
   
GO
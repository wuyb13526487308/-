CREATE VIEW [dbo].[View_MeterTopUp]
AS
SELECT   dbo.IoT_MeterTopUp.ID, dbo.IoT_MeterTopUp.Ser, dbo.IoT_MeterTopUp.UserID, dbo.IoT_User.UserName, 
                dbo.IoT_User.Phone, dbo.IoT_User.Street, dbo.IoT_User.Community, dbo.IoT_User.Door, dbo.IoT_User.Address, 
                dbo.IoT_MeterTopUp.MeterID, dbo.IoT_MeterTopUp.MeterNo, dbo.IoT_MeterTopUp.Amount, 
                dbo.IoT_MeterTopUp.TopUpDate, dbo.IoT_MeterTopUp.TopUpType, dbo.IoT_MeterTopUp.Oper, 
                dbo.IoT_MeterTopUp.OrgCode, dbo.IoT_MeterTopUp.State, dbo.IoT_MeterTopUp.CompanyID, 
                dbo.IoT_MeterTopUp.TaskID, dbo.IoT_MeterTopUp.Context, dbo.IoT_MeterTopUp.IsPrint, dbo.IoT_MeterTopUp.PayType, 
                dbo.IoT_MeterTopUp.SFOperID, dbo.IoT_MeterTopUp.SFOperName
FROM      dbo.IoT_MeterTopUp INNER JOIN
                dbo.IoT_User ON dbo.IoT_MeterTopUp.CompanyID = dbo.IoT_User.CompanyID AND 
                dbo.IoT_MeterTopUp.UserID = dbo.IoT_User.UserID
GO
CREATE VIEW [dbo].[View_UserInfo]
AS
SELECT   dbo.IoT_User.CompanyID, dbo.IoT_User.UserID, dbo.IoT_User.UserName, dbo.IoT_User.Phone, dbo.IoT_User.Street, 
                dbo.IoT_User.Community, dbo.IoT_User.Door, dbo.IoT_User.Address, dbo.IoT_User.State, 
                dbo.IoT_Community.Name AS CommunityName, dbo.IoT_Street.Name AS StreetName, dbo.IoT_Meter.MeterNo, 
                dbo.IoT_User.UserType, dbo.IoT_User.SFZH, dbo.IoT_User.BZRQ, dbo.IoT_User.BZFY, dbo.IoT_User.YJBZFY, 
                dbo.IoT_User.LD, dbo.IoT_User.DY, dbo.IoT_User.BGL, dbo.IoT_User.ZS, dbo.IoT_User.YGBX
FROM      dbo.IoT_User INNER JOIN
                dbo.IoT_Street ON dbo.IoT_User.Street = dbo.IoT_Street.ID INNER JOIN
                dbo.IoT_Community ON dbo.IoT_User.Community = dbo.IoT_Community.ID INNER JOIN
                dbo.IoT_Meter ON dbo.IoT_User.UserID = dbo.IoT_Meter.UserID RIGHT OUTER JOIN
                dbo.ADUser ON dbo.IoT_User.CompanyID = dbo.ADUser.CompanyID AND dbo.IoT_User.UserID = dbo.ADUser.UserID
GO

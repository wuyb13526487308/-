CREATE VIEW [dbo].[View_AdUser]
AS
SELECT     dbo.IoT_User.UserName, dbo.IoT_Meter.MeterNo, dbo.ADUser.UserID, dbo.ADUser.CompanyID, dbo.ADUser.AP_ID, dbo.ADUser.PublishDate, dbo.ADUser.Street, dbo.ADUser.Community, 
                      dbo.ADUser.AC_ID, dbo.ADUser.Adress, dbo.ADUser.AddTime, dbo.ADUser.Ver, dbo.ADContext.Context
FROM         dbo.ADUser INNER JOIN
                      dbo.IoT_User ON dbo.ADUser.UserID = dbo.IoT_User.UserID INNER JOIN
                      dbo.IoT_Meter ON dbo.ADUser.UserID = dbo.IoT_Meter.UserID LEFT OUTER JOIN
                      dbo.ADContext ON dbo.ADContext.AC_ID = dbo.ADUser.AC_ID
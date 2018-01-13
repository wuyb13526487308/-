CREATE VIEW [dbo].[View_UserInfoADD]
AS
SELECT     dbo.IoT_User.CompanyID, dbo.IoT_User.UserID, dbo.IoT_User.UserName, dbo.IoT_User.Phone, dbo.IoT_User.Street, dbo.IoT_User.Community, dbo.IoT_User.Door, dbo.IoT_User.Address, 
                      dbo.IoT_User.State, dbo.IoT_Community.Name AS CommunityName, dbo.IoT_Street.Name AS StreetName, dbo.IoT_Meter.MeterNo
FROM         dbo.IoT_User LEFT OUTER JOIN
                      dbo.IoT_Meter ON dbo.IoT_User.UserID = dbo.IoT_Meter.UserID LEFT OUTER JOIN
                      dbo.IoT_Street ON dbo.IoT_User.Street = dbo.IoT_Street.ID LEFT OUTER JOIN
                      dbo.IoT_Community ON dbo.IoT_User.Community = dbo.IoT_Community.ID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[12] 2[34] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "IoT_User"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 298
               Right = 194
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "IoT_Meter"
            Begin Extent = 
               Top = 26
               Left = 236
               Bottom = 331
               Right = 477
            End
            DisplayFlags = 344
            TopColumn = 0
         End
         Begin Table = "IoT_Street"
            Begin Extent = 
               Top = 88
               Left = 237
               Bottom = 269
               Right = 382
            End
            DisplayFlags = 344
            TopColumn = 0
         End
         Begin Table = "IoT_Community"
            Begin Extent = 
               Top = 142
               Left = 235
               Bottom = 313
               Right = 413
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_UserInfoADD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_UserInfoADD'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_UserInfoADD'
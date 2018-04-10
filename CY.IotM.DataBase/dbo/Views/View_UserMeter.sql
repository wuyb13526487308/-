


CREATE view [dbo].[View_UserMeter]
as
SELECT   a.CompanyID, a.UserID, a.UserName, a.State, a.Address, a.Street, a.Community, a.Door, b.MeterNo, b.MeterType, 
                b.ValveState, b.LastTotal, b.TotalTopUp, b.TotalAmount, b.RemainingAmount, b.ReadDate, b.InstallDate, b.ID AS MeterID, 
                b.Gas1, b.Gas2, b.Gas3, b.Gas4, b.Price1, b.Price2, b.Price3, b.Price4, b.Price5, b.SettlementDay, b.SettlementMonth, 
                b.SettlementType, b.Ladder, b.IsUsed, b.UploadCycle, b.EnableMeterOper, b.Direction, a.UserType, a.SFZH, a.BZRQ, 
                a.BZFY, a.YJBZFY, a.LD, a.DY, a.BGL, a.ZS, a.YGBX, a.BXGMRQ, a.BXYXQ, a.BXGMRSFZ, a.YQHTQD, a.YQHTQDRQ, 
                a.YQHTBH, a.FYQHTR, a.QYQHTR, a.BZCZYBH, a.SYBWG, a.BWGCD, a.ZCZJE, a.ZYQL, a.ZQF, a.Phone, 
                b.MeterModel, b.MeterRange, b.Installer, b.IotPhone, b.InstallType, b.InstallPlace, b.FDKH1, b.FDKH2, 
                b.InstallFDK
FROM      dbo.IoT_Meter AS b INNER JOIN
                dbo.IoT_User AS a ON a.UserID = b.UserID
				
GO
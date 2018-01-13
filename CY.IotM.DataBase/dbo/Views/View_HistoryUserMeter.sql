CREATE view [dbo].[View_HistoryUserMeter]
as
 select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.State,
         a.Address,
         a.Street,
         a.Community,
         a.Door,
         b.MeterNo,
         b.MeterType,
         b.ValveState,
         b.LastTotal,
         b.TotalTopUp,
         b.TotalAmount,
         b.RemainingAmount,
         b.ReadDate,
         b.InstallDate,
         b.ID as MeterID,
         b.Gas1,
         b.Gas2,
         b.Gas3,
         b.Gas4,
         b.Price1,
         b.Price2,
         b.Price3,
         b.Price4,
         b.Price5,
         b.SettlementDay,
         b.SettlementType,
         b.Ladder,
         b.IsUsed,
         b.UploadCycle,
         b.Direction
         from  IoT_MeterHistory b 
         inner join IoT_User a  on a.UserID=b.UserID

GO

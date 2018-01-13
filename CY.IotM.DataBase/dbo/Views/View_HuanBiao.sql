
create view [dbo].[View_HuanBiao]
as
--SELECT*FROM (
--SELECT *, Row_Number() OVER (partition by dayLastTotal ORDER BY eReadDate desc) AS  RANK FROM (
SELECT DISTINCT c.ID AS HID,
 a.CompanyID, 
         a.UserID,
         a.UserName,
         a.State,
         a.Address,
         a.Street,
         a.Community,
         a.Door,
         ISNULL(b.MeterNo,f.MeterNo) as MeterNo,
         ISNULL(b.MeterType,f.MeterType) as MeterType,
         ISNULL(b.ValveState,f.ValveState) as ValveState,
         ISNULL(b.LastTotal,f.LastTotal) as LastTotal,--上次结算底数
         ISNULL(b.TotalTopUp,f.TotalTopUp) as TotalTopUp,
         ISNULL(b.TotalAmount,f.TotalAmount) as TotalAmount,
         ISNULL(b.ReadDate,f.ReadDate) as ReadDate,
         ISNULL(b.InstallDate,f.InstallDate) as InstallDate,
         ISNULL(b.ID,f.ID) as MeterID,
         ISNULL(b.Gas1,f.Gas1) as Gas1,
         ISNULL(b.Gas2,f.Gas2) as Gas2,
         ISNULL(b.Gas3,f.Gas3) as Gas3,
         ISNULL(b.Gas4,f.Gas4) as Gas4,
         ISNULL(b.Price1,f.Price1) as Price1,
         ISNULL(b.Price2,f.Price2) as Price2,
         ISNULL(b.Price3,f.Price3) as Price3,
         ISNULL(b.Price4,f.Price4) as Price4,
         ISNULL(b.Price5,f.Price5) as Price5,
         ISNULL(b.SettlementDay,f.SettlementDay) as SettlementDay,
         ISNULL(b.SettlementType,f.SettlementType) as SettlementType,
         ISNULL(b.Ladder,f.Ladder) as Ladder,
         ISNULL(b.IsUsed,f.IsUsed) as IsUsed,
         ISNULL(b.UploadCycle,f.UploadCycle) as UploadCycle,
         ISNULL(b.Direction,f.Direction) as Direction,
         c.State AS changeState,
      OldMeterNo,
      RegisterDate,
      OldGasSum,
      Reason
      ,ChangeGasSum
      ,c.RemainingAmount
      ,ChangeUseSum
      ,NewMeterNo
      ,FinishedDate
      --,D.LastTotal AS oldLastTotal
      --, b.LastTotal AS dayLastTotal
      --,e.Gas AS dayGas
      --,e.ReadDate AS eReadDate
      ,ISNULL(G.TotalAmount,0) AS NEWTotalAmount 
      --,CASE  WHEN  b.MeterType='00' THEN ISNULL(D.LastTotal,0)-ISNULL(e.LastTotal,0) WHEN  b.MeterType='01' THEN  ISNULL(D.LastTotal,0)-ISNULL(e.Gas,0) END AS Total
         from IoT_Meter b 
         RIGHT JOIN dbo.IoT_ChangeMeter AS c
         ON  b.CompanyID = c.CompanyID AND b.UserID = c.UserID AND b.MeterNo=c.OldMeterNo
         inner join IoT_User a 
          on a.UserID=c.UserID AND c.CompanyID = a.CompanyID
         LEFT JOIN dbo.IoT_MeterHistory AS f ON c.CompanyID = f.CompanyID AND c.UserID = f.UserID AND f.MeterNo=c.OldMeterNo
         LEFT JOIN dbo.IoT_Meter AS G ON  G.MeterNo=C.NewMeterNo
         --LEFT JOIN dbo.IoT_MeterDataHistory AS D
         --ON b.MeterNo=D.MeterNo 
         --LEFT JOIN IoT_DayReadMeter AS e ON b.MeterNo=e.MeterNo
   

   --) AS a
   --) AS b
   --      WHERE b.RANK=1


GO



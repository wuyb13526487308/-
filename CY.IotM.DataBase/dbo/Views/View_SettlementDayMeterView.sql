CREATE view View_SettlementDayMeterView
as
select *
      ,  (select COUNT(*) from IoT_SettlementDayMeter WHERE ID=AA.DayID and State='3' )   as FailCount from (
 SELECT  ID as DayID
      ,RegisterDate
      ,Scope
      ,Total
      ,SettlementDay
      ,SettlementMonth
      ,State
      ,Oper
      ,Context
      ,CompanyID
      ,TaskID
  FROM IoT_SetSettlementDay) as AA


         
GO
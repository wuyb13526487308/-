CREATE view [dbo].[View_UpLoadDateView]
as
select *
      ,  (select COUNT(*) from IoT_UploadCycleMeter WHERE ID=AA.CycleID and State='3' )   as FailCount from (
 SELECT ID AS CycleID
      ,RegisterDate
      ,Scope
      ,Total
      ,ReportType
      ,Par
      ,State
      ,Oper
      ,FinishedDate
      ,Context
      ,CompanyID
      ,TaskID
  FROM IoT_SetUploadCycle) as AA


         
GO
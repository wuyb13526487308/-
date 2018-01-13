



CREATE view [dbo].[View_ValveControl]
as
 select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
         b.RegisterDate,
         b.State,
         b.ControlType,
         b.Oper,
         b.Reason,
         b.FinishedDate,
         b.Context,
         b.TaskID
         from IoT_ValveControl b 
         inner join View_UserMeter a  on a.MeterNo=b.MeterNo
         union
select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
         b.RegisterDate,
         b.State,
         b.ControlType,
         b.Oper,
         b.Reason,
         b.FinishedDate,
         b.Context,
         b.TaskID
         from IoT_ValveControl b 
         inner join View_HistoryUserMeter a  on a.MeterNo=b.MeterNo
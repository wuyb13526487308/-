

CREATE view [dbo].[View_PricingMeter]
as
 select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
         c.PriceType,
         b.State,
         b.FinishedDate,
         b.Context,
         b.ID
         from IoT_PricingMeter b 
         inner join View_UserMeter a  on a.MeterNo=b.MeterNo
		 inner join IoT_Pricing c on b.ID=c.ID
		 union
		  select  a.CompanyID, 
         a.UserID,
         a.UserName,
         a.Address,
         a.MeterNo,
         c.PriceType,
         b.State,
         b.FinishedDate,
         b.Context,
         b.ID
         from IoT_PricingMeter b 
         inner join View_HistoryUserMeter a  on a.MeterNo=b.MeterNo
	     inner join IoT_Pricing c on b.ID=c.ID
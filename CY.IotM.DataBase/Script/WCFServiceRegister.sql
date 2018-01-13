-- =============================================
-- 在此文档中编写WCF 服务注册脚本
-- =============================================
/*
 注册脚本模板如下：
 ------------------------------------------------
INSERT INTO Frame_RemotingObject
                      (ObjectType, objectDll,URI, interfaceType, interfaceDll)
VALUES     ('RX.Gas.ReportLib.ReportDataService',		--wcf服务类（即：实现wcf接口契约的类）
			'CY.Gas.ReportLib.dll',						--wcf服务类所属的库文件名称
			'ReportDataService',						--wcf服务URI名称
			'CY.Gas.Report.Common.IReportDataService',	--wcf服务契约接口 
            'CY.Gas.ReportLib.dll')						--wcf服务契约接口定义的库文件名称
            
说明：一个wcf服务类实现了多个契约接口的，用“|”将多个接口连接起来，例如：

'CY.FrameLib.Common.IEmployee|CY.FrameLib.Common.IDepartment|CY.FrameLib.Common.IRight'

*/


delete from Frame_RemotingObject



--企业管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CompanyManageService',
'CY.IotM.DataService.dll',
'CompanyManageService',
'CY.IotM.Common.ICompanyManage',
'CY.IotM.Common.dll'
)


--企业查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IotM.Common.dll,CY.IotM.Common.CompanyInfo>',
'CY.IotM.DataService.dll',
'CommonSearchOf_CompanyInfo',
'CY.IotM.Common.ICommonSearch<CY.IotM.Common.dll,CY.IotM.Common.CompanyInfo>',
'CY.IotM.Common.dll'
)



--企业操作员管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CompanyOperatorService',
'CY.IotM.DataService.dll',
'CompanyOperatorService',
'CY.IotM.Common.ICompanyOperatorManage',
'CY.IotM.Common.dll'
)


--企业操作员查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IotM.Common.dll,CY.IotM.Common.CompanyOperator>',
'CY.IotM.DataService.dll',
'CommonSearchOf_CompanyOperator',
'CY.IotM.Common.ICommonSearch<CY.IotM.Common.dll,CY.IotM.Common.CompanyOperator>',
'CY.IotM.Common.dll'
)



--Web端用户登录管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.WebloginerManageService',
'CY.IotM.DataService.dll',
'WebloginerManageService',
'CY.IotM.Common.ILoginerManage',
'CY.IotM.Common.dll'
)



--权限管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.OperRightManageService',
'CY.IotM.DataService.dll',
'OperRightManageService',
'CY.IotM.Common.IOperRightManage',
'CY.IotM.Common.dll'
)




 --系统日志查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IotM.Common.dll,CY.IotM.Common.SystemLog>',
'CY.IotM.DataService.dll',
'CommonSearchOf_SystemLog',
'CY.IotM.Common.ICommonSearch<CY.IotM.Common.dll,CY.IotM.Common.SystemLog>',
'CY.IotM.Common.dll'
)    


--系统日志管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.SystemLogManageService',
'CY.IotM.DataService.dll',
'SystemLogManageService',
'CY.IotM.Common.ISystemLogManage',
'CY.IotM.Common.dll'
)



 --菜单查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IotM.Common.dll,CY.IotM.Common.MenuInfo>',
'CY.IotM.DataService.dll',
'CommonSearchOf_MenuInfo',
'CY.IotM.Common.ICommonSearch<CY.IotM.Common.dll,CY.IotM.Common.MenuInfo>',
'CY.IotM.Common.dll'
)    


--菜单管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.MenuManageService',
'CY.IotM.DataService.dll',
'MenuManageService',
'CY.IotM.Common.IMenuManage',
'CY.IotM.Common.dll'
)


-- 报表服务    
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
VALUES ('RX.Gas.ReportLib.ReportDataService',  
'CY.Gas.ReportLib.v3.0.dll', 
'ReportDataService',
'CY.Gas.Report.Common.IReportDataService', 
'CY.Gas.ReportLib.v3.0.dll')  


--报表查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IotM.Common.dll,CY.IotM.Common.ReportTemplate>',
'CY.IotM.DataService.dll',
'CommonSearchOf_ReportTemplate',
'CY.IotM.Common.ICommonSearch<CY.IotM.Common.dll,CY.IotM.Common.ReportTemplate>',
'CY.IotM.Common.dll'
)

--企业报表查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IotM.Common.dll,CY.IotM.Common.CompanyReport>',
'CY.IotM.DataService.dll',
'CommonSearchOf_CompanyReport',
'CY.IotM.Common.ICommonSearch<CY.IotM.Common.dll,CY.IotM.Common.CompanyReport>',
'CY.IotM.Common.dll'
)

--服务中心服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataCenter.DCSService',
'CY.IoTM.DataCenter.dll',
'IoTM_DCSService',
'CY.IoTM.Service.Common.IDCSService',
'CY.IoTM.Service.Common.dll'
)



--任务管理服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.TaskManageService',
'CY.IoTM.DataService.BusinessSystem.dll',
'TaskManageService',
'CY.IoTM.Common.Business.ITaskManage',
'CY.IoTM.Common.BusinessSystem.dll'
)

--点火登记服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.DianHuoService',
'CY.IoTM.DataService.BusinessSystem.dll',
'DianHuoService',
'CY.IoTM.Common.Business.IDianHuo',
'CY.IoTM.Common.BusinessSystem.dll'
)


--阀门控制服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.ValveControlService',
'CY.IoTM.DataService.BusinessSystem.dll',
'ValveControlService',
'CY.IoTM.Common.Business.IValveControl',
'CY.IoTM.Common.BusinessSystem.dll'
)

--充值服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.TopUpService',
'CY.IoTM.DataService.BusinessSystem.dll',
'TopUpService',
'CY.IoTM.Common.Business.IMeterTopUp',
'CY.IoTM.Common.BusinessSystem.dll'
)

--数据上报服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.ReportDataService',
'CY.IoTM.DataService.BusinessSystem.dll',
'MeterSubmitDataService',
'CY.IoTM.Common.Business.IReportData',
'CY.IoTM.Common.BusinessSystem.dll'
)



--街道管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.StreetManageService',
'CY.IoTM.DataService.BusinessSystem.dll',
'StreetManageService',
'CY.IoTM.Common.Business.IStreetManage',
'CY.IoTM.Common.BusinessSystem.dll'
)


--街道查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_Street>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_Street',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_Street>',
'CY.IotM.Common.dll'
)



--小区管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.CommunityManageService',
'CY.IoTM.DataService.BusinessSystem.dll',
'CommunityManageService',
'CY.IoTM.Common.Business.ICommunityManage',
'CY.IoTM.Common.BusinessSystem.dll'
)


--小区查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_Community>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_Community',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_Community>',
'CY.IotM.Common.dll'
)


--价格参数查询
INSERT [dbo].[Frame_RemotingObject] ([ObjectType], [URI], [objectDll], [interfaceType], [interfaceDll]) 
VALUES (N'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_PricePar>',
 N'CommonSearchOf_IoT_PricePar', 
 N'CY.IotM.DataService.dll',
 N'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_PricePar>',
 N'CY.IotM.Common.dll')


 --价格参数管理
 INSERT [dbo].[Frame_RemotingObject] ([ObjectType], [URI], [objectDll], [interfaceType], [interfaceDll])
  VALUES (N'CY.IoTM.DataService.Business.PriceParManageService',
  N'PriceParManageService', 
  N'CY.IoTM.DataService.BusinessSystem.dll',
  N'CY.IoTM.Common.Business.IPriceParManage', 
  N'CY.IoTM.Common.BusinessSystem.dll')


 --服务器参数查询
 INSERT [dbo].[Frame_RemotingObject] ([ObjectType], [URI], [objectDll], [interfaceType], [interfaceDll]) 
 VALUES (N'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_SystemPar>', 
 N'CommonSearchOf_IoT_SystemPar',
  N'CY.IotM.DataService.dll', 
 N'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_SystemPar>', 
 N'CY.IotM.Common.dll')


  --服务器参数管理
 INSERT [dbo].[Frame_RemotingObject] ([ObjectType], [URI], [objectDll], [interfaceType], [interfaceDll])
 VALUES (N'CY.IoTM.DataService.Business.SystemParManageService', 
 N'SystemParManageService', 
 N'CY.IoTM.DataService.BusinessSystem.dll',
 N'CY.IoTM.Common.Business.ISystemParManage',
 N'CY.IoTM.Common.BusinessSystem.dll')


 --燃气用户管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.UserManageService',
'CY.IoTM.DataService.BusinessSystem.dll',
'UserManageService',
'CY.IoTM.Common.Business.IUserManage',
'CY.IoTM.Common.BusinessSystem.dll'
)


--燃气用户查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_User>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_User',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_User>',
'CY.IotM.Common.dll'
)



 --表具管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.MeterManageService',
'CY.IoTM.DataService.BusinessSystem.dll',
'MeterManageService',
'CY.IoTM.Common.Business.IMeterManage',
'CY.IoTM.Common.BusinessSystem.dll'
)


--表具查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_Meter>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_Meter',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_Meter>',
'CY.IotM.Common.dll'
)



--档案查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_UserMeter>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_UserMeter',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_UserMeter>',
'CY.IotM.Common.dll'
)




--阀门控制记录查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_ValveControl>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_ValveControl',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_ValveControl>',
'CY.IotM.Common.dll'
)


--设置报警参数管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.SetAlarmService',
'CY.IoTM.DataService.BusinessSystem.dll',
'SetAlarmService',
'CY.IoTM.Common.Business.ISetAlarm',
'CY.IoTM.Common.BusinessSystem.dll'
)


--设置报警参数查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_SetAlarm>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_SetAlarm',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_SetAlarm>',
'CY.IotM.Common.dll'
)



--设置报警参数用户查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_AlarmMeter>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_AlarmMeter',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_AlarmMeter>',
'CY.IotM.Common.dll'
)



--报警信息查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_AlarmInfo>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_AlarmInfo',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_AlarmInfo>',
'CY.IotM.Common.dll'
)


--小区视图查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_Community>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_Community',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_Community>',
'CY.IotM.Common.dll'
)


--调价计划管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.PricingManageService',
'CY.IoTM.DataService.BusinessSystem.dll',
'PricingManageService',
'CY.IoTM.Common.Business.IPricingManage',
'CY.IoTM.Common.BusinessSystem.dll'
)


--调价计划查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_Pricing>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_Pricing',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_Pricing>',
'CY.IotM.Common.dll'
)


--调价计划用户查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_PricingMeter>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_PricingMeter',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_PricingMeter>',
'CY.IotM.Common.dll'
)


--临时用户查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_UserTemp>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_UserTemp',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_UserTemp>',
'CY.IotM.Common.dll'
)

--广告管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.AdInfoService',
'CY.IoTM.DataService.BusinessSystem.dll',
'AdInfoService',
'CY.IoTM.Common.Business.IAdInfoManage',
'CY.IoTM.Common.BusinessSystem.dll'
)

INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_AdInfo>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_AdInfo',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_AdInfo>',
'CY.IotM.Common.dll'
)
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_SetAdInfo>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_SetAdInfo',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_SetAdInfo>',
'CY.IotM.Common.dll'
)
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_AdInfoMeter>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_AdInfoMeter',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_AdInfoMeter>',
'CY.IotM.Common.dll'
)

INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.Iot_MeterAlarmPara>',
'CY.IotM.DataService.dll',
'CommonSearchOf_Iot_MeterAlarmPara',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.Iot_MeterAlarmPara>',
'CY.IotM.Common.dll'
)

--气量表气费结算


INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.MeterGasBillService',
'CY.IoTM.DataService.BusinessSystem.dll',
'MeterGasBillService',
'CY.IoTM.Common.Business.IMeterGasBill',
'CY.IoTM.Common.BusinessSystem.dll'
)

INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_MeterGasBill>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_MeterGasBill',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_MeterGasBill>',
'CY.IotM.Common.dll'
)




--========================================




--抄表服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.ChaoBiaoService',
'CY.IoTM.DataService.BusinessSystem.dll',
'ChaoBiaoService',
'CY.IoTM.Common.Business.IChaoBiao',
'CY.IoTM.Common.BusinessSystem.dll'
)

--抄表记录查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_UserMeterHistory>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_UserMeterHistory',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_UserMeterHistory>',
'CY.IotM.Common.dll'
)

--上传周期
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.SetUploadCycleService',
'CY.IoTM.DataService.BusinessSystem.dll',
'SetUploadCycleService',
'CY.IoTM.Common.Business.ISetUploadCycle',
'CY.IoTM.Common.BusinessSystem.dll'
)

--设置上传周期查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_SetUploadCycle>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_SetUploadCycle',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_SetUploadCycle>',
'CY.IotM.Common.dll'
)
--设置上传周期维护
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_UploadCycleMeter>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_UploadCycleMeter',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.IoT_UploadCycleMeter>',
'CY.IotM.Common.dll'
)
--设置上传周期视图
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_UpLoadDate>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_UpLoadDate',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_UpLoadDate>',
'CY.IotM.Common.dll'
)
--设置上传周期视图
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_UpLoadDateView>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_UpLoadDateView',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_UpLoadDateView>',
'CY.IotM.Common.dll'
)
--充值视图
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_ChongZhi>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_ChongZhi',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_ChongZhi>',
'CY.IotM.Common.dll'
)
--营业厅充值服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.ChongzhiManageService',
'CY.IoTM.DataService.BusinessSystem.dll',
'ChongzhiManageService',
'CY.IoTM.Common.Business.IChongzhiManage',
'CY.IoTM.Common.BusinessSystem.dll'
)
--结算日设定
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_SettlementDayMeter>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_SettlementDayMeter',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_SettlementDayMeter>',
'CY.IotM.Common.dll'
)
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_SettlementDayMeterView>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_SettlementDayMeterView',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_SettlementDayMeterView>',
'CY.IotM.Common.dll'
)
--结算日服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.SettlementService',
'CY.IoTM.DataService.BusinessSystem.dll',
'SettlementService',
'CY.IoTM.Common.Business.ISettlement',
'CY.IoTM.Common.BusinessSystem.dll'
)
--换表管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.HuanBiaoService',
'CY.IoTM.DataService.BusinessSystem.dll',
'HuanBiaoService',
'CY.IoTM.Common.Business.IHuanBiao',
'CY.IoTM.Common.BusinessSystem.dll'
)
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_HuanBiao>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_HuanBiao',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_HuanBiao>',
'CY.IotM.Common.dll'
)
--历史换表视图
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_HistoryUserMeter>',
'CY.IotM.DataService.dll',
'CommonSearchOf_View_HistoryUserMeter',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.BusinessSystem.dll,CY.IoTM.Common.Business.View_HistoryUserMeter>',
'CY.IotM.Common.dll'
)


INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.DataService.Business.GetMonitorInfoService',
'CY.IoTM.DataService.BusinessSystem.dll',
'GetMonitorInfoService',
'CY.IoTM.Common.Business.IGetMonitorInfo',
'CY.IoTM.Common.BusinessSystem.dll'
)


--文件服务
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.ADService.ADFileService',
'CY.IoTM.ADService.dll',
'ADFileService',
'CY.IoTM.Common.ADSystem.IADFileService',
'CY.IoTM.Common.ADSystem.dll'
)

INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.ADService.ADPublishManager',
'CY.IoTM.ADService.dll',
'ADPublishManager',
'CY.IoTM.Common.ADSystem.IADPublishManager',
'CY.IoTM.Common.ADSystem.dll'
)


 --广告主题管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.ADService.ADContextDAL',
'CY.IoTM.ADService.dll',
'ADContextDAL',
'CY.IoTM.Common.ADSystem.IADContextDAL',
'CY.IoTM.Common.ADSystem.dll'
)
--广告Linq查询查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADContext>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_ADContext',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADContext>',
'CY.IotM.Common.dll'
)

 --广告内容管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.ADService.ADItemDAL',
'CY.IoTM.ADService.dll',
'ADItemDAL',
'CY.IoTM.Common.ADSystem.IADItemDAL',
'CY.IoTM.Common.ADSystem.dll'
)
 --广告内容Linq查询管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADItem>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_ADItem',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADItem>',
'CY.IotM.Common.dll'
)

 --广告用户管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.ADService.ADUserDAL',
'CY.IoTM.ADService.dll',
'ADUserDAL',
'CY.IoTM.Common.ADSystem.IADUserDAL',
'CY.IoTM.Common.ADSystem.dll'
)
 --广告用户查询管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADUser>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_ADUser',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADUser>',
'CY.IotM.Common.dll'
)

 --广告用户视图查询Linq查询管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.View_AdUser>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_View_AdUser',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.View_AdUser>',
'CY.IotM.Common.dll')

 --广告用户信息查询
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.View_UserInfo>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_View_UserInfo',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.View_UserInfo>',
'CY.IotM.Common.dll')


 --广告发布视图查询Linq查询管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.View_AdPublish>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_View_AdPublish',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.View_AdPublish>',
'CY.IotM.Common.dll')

 --广告发布信息查询Linq查询管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADPublish>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_ADPublish',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADPublish>',
'CY.IotM.Common.dll')
--广告发布信息查询 管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.ADService.ADPublishDAL',
'CY.IoTM.ADService.dll',
'ADPublishDAL',
'CY.IoTM.Common.ADSystem.IADPublishDAL',
'CY.IoTM.Common.ADSystem.dll'
)


 --广告发布用户查询Linq查询管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADPublishUser>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_ADPublishUser',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.ADPublishUser>',
'CY.IotM.Common.dll')
--广告发布用户信息查询 管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IoTM.ADService.ADPublishUserDAL',
'CY.IoTM.ADService.dll',
'ADPublishUserDAL',
'CY.IoTM.Common.ADSystem.IADPublishUserDAL',
'CY.IoTM.Common.ADSystem.dll'
)



 --广告发布用户详细查询Linq查询管理
INSERT INTO Frame_RemotingObject(ObjectType, objectDll,URI, interfaceType, interfaceDll)
values( 'CY.IotM.DataService.CommonSearchService<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.View_AdPublishUserInfo>',
'CY.IotM.DataService.dll',
'CommonSearchOf_IoT_View_AdPublishUserInfo',
'CY.IotM.Common.ICommonSearch<CY.IoTM.Common.ADSystem.dll,CY.IoTM.Common.ADSystem.View_AdPublishUserInfo>',
'CY.IotM.Common.dll')
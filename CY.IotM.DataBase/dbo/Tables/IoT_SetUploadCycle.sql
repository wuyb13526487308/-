CREATE TABLE [dbo].[IoT_SetUploadCycle] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [RegisterDate] DATETIME      NULL,
    [Scope]        VARCHAR (100) NULL,
    [Total]        INT           NULL,
    [ReportType]   CHAR (2)      NULL,
    [Par]          CHAR (6)      NULL,
    [State]        CHAR (1)      NULL,
    [Oper]         VARCHAR (50)  NULL,
    [FinishedDate] DATETIME      NULL,
    [Context]      VARCHAR (100) NULL,
    [CompanyID]    CHAR (4)      NULL,
	[TaskID]       varchar(50)   NULL,
    CONSTRAINT [PK_IoT_MeterUploadCycle] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'Context';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'FinishedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'Oper';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态：0 申请 1 撤销  2 完成 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '周期参数，
DD 天        范围：00-31
HH 小时   范围：00-23
MM 分钟  范围：00-59', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'Par';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上传周期模式：
00：以月为周期，在每个月的XX日YY时ZZ分上传数据；
01：以XX天为周期，在每XX天的YY时ZZ分上传数据，起点为每月的01日00时00分；
02：以YY时为周期，在每YY小时的ZZ分上传数据，起点为每天的00时00分； 
03：以燃气表启动开始计时，以XX日YY时ZZ分上传数据', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'ReportType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '设置总户数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'Total';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '区域：设置范围描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '申请日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle', @level2type = N'COLUMN', @level2name = N'RegisterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '表上传周期管理', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetUploadCycle';


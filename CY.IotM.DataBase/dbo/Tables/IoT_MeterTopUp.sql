﻿--  -------------------------------------------------- 
--  Generated by Enterprise Architect Version 11.0.1106
--  Created On : 星期二, 14 七月, 2015 
--  DBMS       : SQL Server 2008 
--  -------------------------------------------------- 

CREATE TABLE [dbo].[IoT_MeterTopUp] (
    [ID]        BIGINT       IDENTITY (1, 1) NOT NULL,
    [Ser]       INT          NULL,
    [UserID]    VARCHAR (50) NULL,
    [MeterID]   BIGINT       NULL,
    [MeterNo]   VARCHAR (20) NULL,
    [Amount]    MONEY        NULL,
    [TopUpDate] DATETIME     NULL,
    [TopUpType] CHAR (1)     NULL,
    [Oper]      VARCHAR (50) NULL,
    [OrgCode]   VARCHAR (10) NULL,
    [State]     CHAR (1)     NULL,
    [CompanyID] CHAR (4)     NULL,
	[TaskID]    varchar(50)  NULL,
	[Context]   varchar(50)  NULL,
    [IsPrint] BIT NULL DEFAULT 0, 
    [PayType] CHAR(1) NULL DEFAULT '0', 
    [SFOperID] NVARCHAR(50) NULL, 
    [SFOperName] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_IoT_MeterTopUp] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

GO

GO
EXEC sp_addextendedproperty 'MS_Description', '金额表充值记录表', 'Schema', dbo, 'table', IoT_MeterTopUp
;
GO
EXEC sp_addextendedproperty 'MS_Description', '索引号', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', ID
;
GO
EXEC sp_addextendedproperty 'MS_Description', '充值顺序号', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', Ser
;
GO
EXEC sp_addextendedproperty 'MS_Description', '用户ID', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', UserID
;
GO
EXEC sp_addextendedproperty 'MS_Description', '表索引号', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', MeterID
;
GO
EXEC sp_addextendedproperty 'MS_Description', '表号', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', MeterNo
;
GO
EXEC sp_addextendedproperty 'MS_Description', '充值金额', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', Amount
;
GO
EXEC sp_addextendedproperty 'MS_Description', '充值时间', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', TopUpDate
;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'充值方式：0  本地营业厅 1 接口  2 本地网站 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterTopUp', @level2type = N'COLUMN', @level2name = N'TopUpType';


GO
EXEC sp_addextendedproperty 'MS_Description', '操作员，本地营业厅充值方式', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', Oper
;
GO
EXEC sp_addextendedproperty 'MS_Description', '充值机构代码（通过接口操作）', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', OrgCode
;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态：0 等待执行 1 充值完成 2撤销充值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterTopUp', @level2type = N'COLUMN', @level2name = N'State';


GO
EXEC sp_addextendedproperty 'MS_Description', '企业代码', 'Schema', dbo, 'table', IoT_MeterTopUp, 'column', CompanyID
;
GO

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'票据打印状态：0 未打印 1 已打印',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'IoT_MeterTopUp',
    @level2type = N'COLUMN',
    @level2name = N'IsPrint'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'付款方式：0 现金 1 支付宝 2 微信',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'IoT_MeterTopUp',
    @level2type = N'COLUMN',
    @level2name = N'PayType'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'备注',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'IoT_MeterTopUp',
    @level2type = N'COLUMN',
    @level2name = N'Context'
CREATE TABLE [dbo].[IoT_SetSettlementDay] (
    [ID]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [RegisterDate]  DATETIME      NULL,
    [Scope]         VARCHAR (100) NULL,
    [Total]         INT           NULL,
    [SettlementDay] INT           NULL,
    [SettlementMonth] INT           NULL,
    [State]         CHAR (1)      NULL,
    [Oper]          VARCHAR (50)  NULL,
    [Context]       VARCHAR (100) NULL,
    [CompanyID]     CHAR (4)      NULL,
    [TaskID]        VARCHAR (50)  NULL,
    CONSTRAINT [PK_IoT_SetSettlementDay] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'Context';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'Oper';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态：状态：0 申请 1 撤销  2 完成 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'SettlementDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算月', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'SettlementMonth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '总户数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'Total';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '区域：设置结算日的用户范围', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '申请日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'RegisterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '设置结算日', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetSettlementDay';


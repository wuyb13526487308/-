CREATE TABLE [dbo].[IoT_PricePar] (
    [ID]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [CompanyID]      CHAR (4)        NULL,
    [PriceName]      VARCHAR (50)    NULL,
    [Ser]            VARCHAR (4)     NULL,
    [IsUsed]         BIT             DEFAULT ((1)) NULL,
    [Ladder]         INT             NULL,
    [SettlementType] CHAR (2)        NULL,
	[SettlementMonth]  INT             NULL,
    [SettlementDay]  INT             NULL,
    [Price1]         MONEY           NULL,
    [Gas1]           DECIMAL (10, 2) NULL,
    [Price2]         MONEY           NULL,
    [Gas2]           DECIMAL (10, 2) NULL,
    [Price3]         MONEY           NULL,
    [Gas3]           DECIMAL (10, 2) NULL,
    [Price4]         MONEY           NULL,
    [Gas4]           DECIMAL (10, 2) NULL,
    [Price5]         MONEY           NULL,
    [PeriodStartDate] DATETIME NULL, 
    CONSTRAINT [PK_IoT_PricePar] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算日，值范围：1-31', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricePar', @level2type = N'COLUMN', @level2name = N'SettlementDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算类型
00 按月
01 季度
10 半年
11 全年', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricePar', @level2type = N'COLUMN', @level2name = N'SettlementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '阶梯数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricePar', @level2type = N'COLUMN', @level2name = N'Ladder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '启用阶梯价 0 未启用 1 启用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricePar', @level2type = N'COLUMN', @level2name = N'IsUsed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricePar', @level2type = N'COLUMN', @level2name = N'Ser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '价格类型名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricePar', @level2type = N'COLUMN', @level2name = N'PriceName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricePar', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统价格参数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricePar';


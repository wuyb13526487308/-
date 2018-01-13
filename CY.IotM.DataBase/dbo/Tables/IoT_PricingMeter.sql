CREATE TABLE [dbo].[IoT_PricingMeter] (
    [ID]           BIGINT       NOT NULL,
    [MeterID]      BIGINT       NOT NULL,
    [MeterNo]      VARCHAR (20) NULL,
    [State]        char(1)          NULL,
    [FinishedDate] DATETIME     NULL,
    [Context]      VARCHAR (50) NULL,
    [TaskID]       VARCHAR (50) NULL,
    CONSTRAINT [PK_IoT_PricingMeter] PRIMARY KEY CLUSTERED ([ID] ASC, [MeterID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注用于记录失败原因。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricingMeter', @level2type = N'COLUMN', @level2name = N'Context';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricingMeter', @level2type = N'COLUMN', @level2name = N'FinishedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '0 申请 1 完成 2 撤销  3 失败', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricingMeter', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '表号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricingMeter', @level2type = N'COLUMN', @level2name = N'MeterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条件涉及表索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricingMeter', @level2type = N'COLUMN', @level2name = N'MeterID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '调价索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricingMeter', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '调价涉及的相关表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_PricingMeter';


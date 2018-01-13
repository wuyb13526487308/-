CREATE TABLE [dbo].[IoT_AlarmInfo] (
    [ID]         BIGINT       IDENTITY (1, 1) NOT NULL,
    [ReportDate] DATETIME     NULL,
    [Item]       VARCHAR(50)     NULL,
    [AlarmValue] VARCHAR (50) NULL,
    [MeterID]    BIGINT       NULL,
    [MeterNo]    VARCHAR (20) NULL,
    CONSTRAINT [PK_IoT_AlarmInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '表号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_AlarmInfo', @level2type = N'COLUMN', @level2name = N'MeterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '表索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_AlarmInfo', @level2type = N'COLUMN', @level2name = N'MeterID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '报警值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_AlarmInfo', @level2type = N'COLUMN', @level2name = N'AlarmValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '报警项目', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_AlarmInfo', @level2type = N'COLUMN', @level2name = N'Item';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '报告时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_AlarmInfo', @level2type = N'COLUMN', @level2name = N'ReportDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_AlarmInfo', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '表报警信息存储表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_AlarmInfo';


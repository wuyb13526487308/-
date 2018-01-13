CREATE TABLE [dbo].[IoT_UploadCycleMeter] (
    [ID]           BIGINT       NOT NULL,
    [MeterID]      BIGINT       NOT NULL,
    [MeterNo]      VARCHAR (20) NULL,
    [State]        CHAR (1)     NULL,
    [FinishedDate] DATETIME     NULL,
    [Context]      VARCHAR (50) NULL,
    [TaskID] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_IoT_UploadCycleMeter] PRIMARY KEY CLUSTERED ([ID] ASC, [MeterID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '设置完成时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_UploadCycleMeter', @level2type = N'COLUMN', @level2name = N'FinishedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态：0 申请 1 完成 2 撤销  3 失败', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_UploadCycleMeter', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '表号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_UploadCycleMeter', @level2type = N'COLUMN', @level2name = N'MeterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '相关表索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_UploadCycleMeter', @level2type = N'COLUMN', @level2name = N'MeterID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '设置索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_UploadCycleMeter', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '设置上传周期相关表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_UploadCycleMeter';


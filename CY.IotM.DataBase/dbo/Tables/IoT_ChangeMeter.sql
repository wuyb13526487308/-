CREATE TABLE [dbo].[IoT_ChangeMeter] (
    [ID]              BIGINT          IDENTITY (1, 1)NOT NULL,
    [CompanyID]       CHAR (4)        NULL,
    [UserID]          CHAR (10)       NULL,
    [State]           CHAR (1)        NULL,
    [OldMeterNo]      CHAR(20)    NULL,
    [RegisterDate]    DATETIME        NULL,
    [OldGasSum]       DECIMAL (10, 2) NULL,
    [Reason]          VARCHAR (500)   NULL,
    [ChangeGasSum]    DECIMAL (10, 2) NULL,
    [RemainingAmount] MONEY           DEFAULT ((0)) NULL,
    [ChangeUseSum]    DECIMAL (10, 2) NULL,
    [NewMeterNo]      CHAR (20)       NULL,
    [FinishedDate]    DATETIME        NULL,
    [TaskID] VARCHAR(50) NULL, 
    CONSTRAINT [PK_IoT_ChangeMeter] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '换表完成日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'FinishedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '新表号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'NewMeterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '换表气量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'ChangeUseSum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '换表时剩余金额（仅针对金额表）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'RemainingAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '换表时表用气量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'ChangeGasSum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '换表原因', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'Reason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '旧表底，换表前的最后一次抄表数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'OldGasSum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '申请时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'RegisterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '原表号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'OldMeterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作状态 1  换表申请  2 换表登记  3 换表完成', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '换表记录表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_ChangeMeter';


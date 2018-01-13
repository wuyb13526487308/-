CREATE TABLE [dbo].[IoT_MeterHistory] (
    [ID]              BIGINT          NOT NULL,
    [MeterNo]         CHAR (20)       NOT NULL,
    [MeterType]       CHAR (2)        NULL,
    [CompanyID]       CHAR (4)        NULL,
    [UserID]          CHAR (10)       NULL,
    [TotalAmount]     DECIMAL (10, 2) DEFAULT ((0)) NULL,
    [TotalTopUp]      MONEY           DEFAULT ((0)) NULL,
    [Direction]       CHAR (4)        NULL,
    [InstallDate]     DATETIME        NULL,
    [Price1]          MONEY           DEFAULT ((0)) NULL,
    [Gas1]            DECIMAL (10, 2) DEFAULT ((0)) NULL,
    [Price2]          MONEY           DEFAULT ((0)) NULL,
    [Gas2]            DECIMAL (10, 2) DEFAULT ((0)) NULL,
    [Price3]          MONEY           DEFAULT ((0)) NULL,
    [Gas3]            DECIMAL (10, 2) DEFAULT ((0)) NULL,
    [Price4]          MONEY           DEFAULT ((0)) NULL,
    [Gas4]            DECIMAL (10, 2) DEFAULT ((0)) NULL,
    [Price5]          MONEY           DEFAULT ((0)) NULL,
    [IsUsed]          BIT             DEFAULT ((0)) NULL,
    [Ladder]          INT             DEFAULT ((3)) NULL,
    [SettlementType]  CHAR (2)        DEFAULT ((0)) NULL,
    [SettlementDay]   INT             DEFAULT ((28)) NULL,
    [ValveState]      CHAR (1)        NULL,
    [MeterState]      CHAR (1)        NULL,
    [ReadDate]        DATETIME        NULL,
    [RemainingAmount] MONEY           DEFAULT ((0)) NULL,
    [LastTotal]       DECIMAL (10, 2) NULL,
    [PriceCheck]      CHAR (1)        DEFAULT ((0)) NULL,
    [MKeyVer]         SMALLINT        DEFAULT ((0)) NULL,
    [MKey]            CHAR (16)       DEFAULT ((8888888888888888.)) NULL,
    [EnableMeterDate] DATETIME        NULL,
    [EnableMeterOper] VARCHAR(200) NULL,
    [UploadCycle]     CHAR (8)        DEFAULT ((1012359)) NULL,
    [SettlementMonth] INT             NULL,
    CONSTRAINT [PK_IoT_MeterHistory] PRIMARY KEY CLUSTERED ([MeterNo] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上传周期，默认值为每天的23点59分上传', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'UploadCycle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'点火参与人员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'EnableMeterOper';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'点火时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'EnableMeterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通信密钥：用于加密物联网表和后台的通信数据，由{color:red}0~9和A~F{color}之间的16个字符组成
表出厂时默认的密钥为：16个8', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'MKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通讯密钥版本0-255，0表示出厂版本', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'MKeyVer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前价格是否 和表中价格进行校验，0 未校验 1 已校验
当表新安装、重新设置价格、换表后，必须要校验系统设置和表是否一致，当发现不一致，用当前值自动重新设置。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'PriceCheck';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上次结算总用量：最后一次结算是的的表总用气量
注：当出现负数时，表示由于换表导致，其绝对值为本期间换表时期间累计用气量', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'LastTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'剩余金额，燃气表中的剩余金额（最新抄表），该值和燃气表实时值不同步。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'RemainingAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最新抄表时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'ReadDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'表状态：0 正常 1 换表申请 2 换表登记  4 已安装  5 点火（表示点火登记已完成，等待表通讯设置完成）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'MeterState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'阀门状态： 0 开  1 关', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'ValveState';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算日 1-31', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'SettlementDay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算周期：00 月  01 季度 10 半年 11 全年', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'SettlementType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'阶梯数 值范围为1-5，仅当启用阶梯价时有效。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Ladder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'启用阶梯价 0 不启用 1 启用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'IsUsed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'价格5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Price5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'气量4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Gas4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'价格4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Price4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'气量3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Gas3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'价格3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Price3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'气量2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Gas2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'价格2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Price2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'气量1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Gas1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'价格1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Price1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'燃气表安装日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'InstallDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'燃气表进气方向 ：左 表示左进气 右 表示右进气', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'Direction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总充值金额：指当前表到目前的总充值金额，仅对金额表有效。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'TotalTopUp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前表的燃气总用量，单位：立方米', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'TotalAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'00 气量表 01 金额表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'MeterType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'表号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'MeterNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'表信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_MeterHistory';


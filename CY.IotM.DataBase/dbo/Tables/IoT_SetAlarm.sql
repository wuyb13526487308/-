CREATE TABLE [dbo].[IoT_SetAlarm] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [RegisterDate] DATETIME      NULL,
    [Scope]        VARCHAR (100) NULL,
    [Total]        INT           NULL,
    [State]        CHAR (1)      NULL,
    [SwitchTag]    CHAR (16)     NULL,
    [Par1]         INT           NULL,
    [Par2]         INT           NULL,
    [Par3]         INT           NULL,
    [Par4]         CHAR (4)      NULL,
    [Par5]         INT           NULL,
    [Par6]         INT           NULL,
    [Par7]         INT           NULL,
    [Par8]         INT           NULL,
    [Par9]         CHAR (2)      NULL,
    [Oper]         VARCHAR (50)  NULL,
    [Context]      VARCHAR (100) NULL,
    [CompanyID]    CHAR (4)      NULL,
    [TaskID]       VARCHAR (50)  NULL,
    CONSTRAINT [PK_IoT_SetAlarm] PRIMARY KEY CLUSTERED ([ID] ASC)
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Oper';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '燃气表公称流量                    1              BCD        m?h       放大10倍        每个字节表示00~99之间BCD码(先低后高)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '长期未使用切断报警时间                    1              hex          天                                每个字节表示00~FF之间十六进制（先低后高）。最大1EH（30天）。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '持续流量切断报警时间                        1              hex          h                                每个字节表示00~FF之间十六进制（先低后高）。范围120s≤x≤240h', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '异常微小流量切断报警时间                                1              hex                h                              每个字节表示00~FF之间十六进制（先低后高）。最大F0H（240h）。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '异常大流量切断报警时间                    1              hex          s                                每个字节表示00~FF之间十六进制（先低后高）。最大78H（120s）。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '异常大流量值                        2              BCD        m?h       放大100倍      每个字节表示00~99之间BCD码（先低后高）。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '燃气流量过载切断报警时间                                1              hex                s                              每个字节表示00~FF之间十六进制（先低后高）。最大78H（120s）。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '燃气漏泄切断报警时间                        1              hex          s                                每个字节表示00~FF之间十六进制（先低后高）。最大0AH（10s）。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '长期未与服务器通讯报警时间            1              hex          天                                每个字节表示00~FF之间十六进制（先低后高）。最大1E（30天）。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Par1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '报警控制开关标记参数共16个字符，从左至右，每个字符含义如下：
第0 长期未与服务器通讯报警             0：关闭    1：开启
第1 燃气漏气切断报警                 0：关闭    1：开启
第2 流量过载切断报警                 0：关闭    1：开启
第3 异常大流量切断报警                     0：关闭    1：开启
第4 异常微小流量切断报警                 0：关闭    1：开启
第5 持续流量超时切断报警                 0：关闭    1：开启
第6 燃气压力过低切断报警                 0：关闭    1：开启
第7 长期未使用切断报警                     0：关闭    1：开启
第8 移动报警/地址震感器动作切断报警0：关闭    1：开启
第9~第15 备用      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'SwitchTag';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '报警参数设置状态
状态：0 申请 1 撤销  2 完成 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '总户数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Total';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '区域，设置涉及表的范围', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'Scope';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '申请时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'RegisterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '设置报警参数索引号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '设置报警参数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAlarm';


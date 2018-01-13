CREATE TABLE [dbo].[IoT_SystemPar] (
    [CompanyID]  CHAR (4)     NOT NULL,
    [ServerType] CHAR (1)     NULL,
    [NetAddr]    VARCHAR (25) NULL,
    [NetPort]    VARCHAR (5)  NULL,
    [GSM]        VARCHAR (15) NULL,
    [APN]        VARCHAR (20) NULL,
    [UID]        VARCHAR (10) NULL,
    [PWD]        VARCHAR (50) NULL,
    [AutoKey]    BIT          DEFAULT ((1)) NULL,
    [MKey]       CHAR (16)    NULL,
    CONSTRAINT [PK_IoT_SystemPar] PRIMARY KEY CLUSTERED ([CompanyID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户指定密钥', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'MKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '表通讯密钥是否自动生成 1 自动生成 
默认为系统自动生成。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'AutoKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'APN用户密码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'PWD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'APN接入点用户帐号，可以为空', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'UID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'APN接入点', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'APN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'GSM网络地址（手机号）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'GSM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '网络端口', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'NetPort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '网络地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'NetAddr';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '服务器参数类型：0 手机 1 IP地址 2 域名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'ServerType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '对应公司ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '服务器通讯参数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SystemPar';


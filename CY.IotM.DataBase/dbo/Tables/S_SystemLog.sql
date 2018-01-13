CREATE TABLE [dbo].[S_SystemLog] (
    [LogID]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [LogType]      SMALLINT       NOT NULL,
    [OperID]       VARCHAR (10)   NOT NULL,
    [OperName]     VARCHAR (20)   NOT NULL,
    [LogTime]      DATETIME       DEFAULT (getdate()) NOT NULL,
    [LoginIP]      VARCHAR (17)   NOT NULL,
    [LoginBrowser] VARCHAR (50)   DEFAULT ('') NOT NULL,
    [LoginSystem]  VARCHAR (50)   DEFAULT ('') NOT NULL,
    [Context]      VARCHAR (2000) NULL,
    [CompanyID]    CHAR (4)       NOT NULL,
    CONSTRAINT [PK_SystemLog] PRIMARY KEY CLUSTERED ([LogID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'Context';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登陆者操作系统', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'LoginSystem';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登陆者浏览器类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'LoginBrowser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'登陆ip', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'LoginIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日志时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'LogTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作员姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'OperName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作员编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'OperID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日志类别：登陆 = 0,注销 = 1,增加权限组=2,
修改权限组=3,删除权限组=4,分配权限组=5, 其他=99', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'LogType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'日志ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog', @level2type = N'COLUMN', @level2name = N'LogID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统操作日志', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_SystemLog';


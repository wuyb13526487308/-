CREATE TABLE [dbo].[IoT_User] (
    [CompanyID] CHAR (4)      NOT NULL,
    [UserID]    CHAR (10)     NOT NULL,
    [UserName]  VARCHAR (50)  NULL,
    [Phone]     VARCHAR (50)  NULL,
    [Street]    VARCHAR (50)  NULL,
    [Community] VARCHAR (50)  NULL,
    [Door]      VARCHAR (50)  NULL,
    [Address]   VARCHAR (100) NULL,
    [State]     CHAR (1)      NULL,
    [UserType] VARCHAR(2) NULL, 
    [SFZH] VARCHAR(50) NULL, 
    [BZRQ] NVARCHAR(50) NULL, 
    [BZFY] MONEY NULL, 
    [YJBZFY] BIT NULL DEFAULT 0, 
    [LD] NVARCHAR(50) NULL, 
    [DY] NVARCHAR(50) NULL, 
    [BGL] BIT NULL, 
    [ZS] BIT NULL, 
    [YGBX] BIT NULL, 
    [BXGMRQ] DATE NULL, 
    [BXYXQ] INT NULL, 
    [BXGMRSFZ] NVARCHAR(50) NULL, 
    [YQHTQD] BIT NULL, 
    [YQHTQDRQ] DATE NULL, 
    [YQHTBH] NVARCHAR(50) NULL, 
    [FYQHTR] NVARCHAR(50) NULL, 
    [QYQHTR] NVARCHAR(50) NULL, 
    [BZCZYBH] NVARCHAR(50) NULL, 
    [SYBWG] BIT NULL, 
    [BWGCD] INT NULL, 
    [ZCZJE] MONEY NULL, 
    [ZYQL] MONEY NULL, 
    [ZQF] MONEY NULL, 
    [SF_UserId] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_IoT_User] PRIMARY KEY CLUSTERED ([CompanyID] ASC, [UserID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '门牌号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'Door';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '小区', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'Community';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '街道', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'Street';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'UserName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'UserID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户信息表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_User';


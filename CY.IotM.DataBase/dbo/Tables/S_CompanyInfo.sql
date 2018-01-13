CREATE TABLE [dbo].[S_CompanyInfo] (
    [CompanyID]   CHAR (4)      NOT NULL,
    [CompanyName] VARCHAR (50)  NULL,
    [SimpleName]  VARCHAR (50)  NULL,
    [Provinces]   VARCHAR (50)  NULL,
    [City]        VARCHAR (50)  NULL,
    [Address]     VARCHAR (50)  NULL,
    [Linkman]     VARCHAR (50)  NULL,
    [Phone]       VARCHAR (50)  NULL,
    [URL]         VARCHAR (50)  NULL,
    [Status]      SMALLINT      NULL,
    [CreateDate]  DATETIME      NULL,
    [Context]     VARCHAR (500) NULL,
    CONSTRAINT [PK_CompanyInfo] PRIMARY KEY CLUSTERED ([CompanyID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'Context';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业信息创建日期', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'CreateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业状态，0 正常 1 停用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '公司主页', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系人', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'Linkman';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'Address';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '城市', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'City';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '省份', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'Provinces';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '公司简称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'SimpleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'CompanyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业编号（4位数字组成）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业信息表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyInfo';


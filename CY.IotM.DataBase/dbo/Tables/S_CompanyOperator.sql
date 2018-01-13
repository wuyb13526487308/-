CREATE TABLE [dbo].[S_CompanyOperator] (
    [OperID]     VARCHAR (50) NOT NULL,
    [CompanyID]  CHAR (4)     NOT NULL,
    [Pwd]        VARCHAR (50) NULL,
    [Name]       VARCHAR (50) NULL,
    [Sex]        CHAR (1)     NULL,
    [Phone]      CHAR (11)    NULL,
    [PhoneLogin] BIT          NULL,
    [Mail]       VARCHAR (50) NULL,
    [State]      CHAR (1)     DEFAULT ((0)) NULL,
    [OperType]   SMALLINT     NULL,
    CONSTRAINT [PK_CompanyOperator] PRIMARY KEY CLUSTERED ([OperID] ASC, [CompanyID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员类型，0 一般操作员 1 企业主帐号，不能被禁用和删除。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'OperType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员帐号状态：0 正常 1 禁用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员邮箱', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'Mail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否启用手机号码登陆系统', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'PhoneLogin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员手机号码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员性别：0 男 1 女 2 禁用', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户密码（MD5加密）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'Pwd';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '所属企业编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator', @level2type = N'COLUMN', @level2name = N'OperID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业用户操作员表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyOperator';


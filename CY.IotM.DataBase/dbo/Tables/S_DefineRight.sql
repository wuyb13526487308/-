CREATE TABLE [dbo].[S_DefineRight] (
    [RightCode] CHAR (10)     NOT NULL,
    [CompanyID] CHAR (4)      NOT NULL,
    [RightName] CHAR (20)     NOT NULL,
    [Context]   VARCHAR (200) NULL,
    CONSTRAINT [PK_DefineRight] PRIMARY KEY CLUSTERED ([RightCode] ASC, [CompanyID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '一百字以内的备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRight', @level2type = N'COLUMN', @level2name = N'Context';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '权限组名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRight', @level2type = N'COLUMN', @level2name = N'RightName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位编码（不为空则企业独享该权限组）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRight', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '权限组代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRight', @level2type = N'COLUMN', @level2name = N'RightCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '权限组', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRight';


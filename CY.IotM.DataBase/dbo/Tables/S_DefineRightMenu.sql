CREATE TABLE [dbo].[S_DefineRightMenu] (
    [CompanyID] CHAR (4)  NOT NULL,
    [RightCode] CHAR (10) NOT NULL,
    [MenuCode]  CHAR (10) NOT NULL,
    CONSTRAINT [PK_DefineRightMenu] PRIMARY KEY CLUSTERED ([CompanyID] ASC, [RightCode] ASC, [MenuCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '权限菜单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRightMenu', @level2type = N'COLUMN', @level2name = N'MenuCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '权限组代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRightMenu', @level2type = N'COLUMN', @level2name = N'RightCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位编码（为空则企业共享）', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRightMenu', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '权限组菜单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineRightMenu';


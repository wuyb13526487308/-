CREATE TABLE [dbo].[S_CompanyMenu] (
    [CompanyID] CHAR (4)  NOT NULL,
    [MenuCode]  CHAR (10) NOT NULL,
    CONSTRAINT [PK_S_CompanyMenu] PRIMARY KEY CLUSTERED ([CompanyID] ASC, [MenuCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '菜单代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyMenu', @level2type = N'COLUMN', @level2name = N'MenuCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '公司编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyMenu', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '公司对应菜单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_CompanyMenu';


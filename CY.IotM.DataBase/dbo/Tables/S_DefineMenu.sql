CREATE TABLE [dbo].[S_DefineMenu] (
    [MenuCode]   CHAR (10)     NOT NULL,
    [Name]       VARCHAR (20)  NOT NULL,
    [Type]       CHAR (2)      NOT NULL,
    [UrlClass]   VARCHAR (100) NULL,
    [ImageUrl]   VARCHAR (100)  NULL,
    [OrderNum]   SMALLINT      NOT NULL,
    [FatherCode] CHAR (10)     NULL,
    [RID]        INT           NULL,
    CONSTRAINT [PK_DefineMenu] PRIMARY KEY CLUSTERED ([MenuCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模板ID  当type为04 报表时才有值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu', @level2type = N'COLUMN', @level2name = N'RID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '父菜单编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu', @level2type = N'COLUMN', @level2name = N'FatherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '排序编号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu', @level2type = N'COLUMN', @level2name = N'OrderNum';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '图标样式地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu', @level2type = N'COLUMN', @level2name = N'ImageUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '链接页面或类入口。', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu', @level2type = N'COLUMN', @level2name = N'UrlClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '00：主菜单，01子菜单，02按钮类菜单，04报表菜单。（web）
10：主菜单，11子菜单，12按钮类菜单。（windows)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '菜单名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '菜单代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu', @level2type = N'COLUMN', @level2name = N'MenuCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '权限菜单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineMenu';


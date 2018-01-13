CREATE TABLE [dbo].[S_DefineOperRight] (
    [CompanyID] CHAR (4)     NOT NULL,
    [OperID]    VARCHAR (50) NOT NULL,
    [RightCode] CHAR (10)    NOT NULL,
    CONSTRAINT [PK_DefineOperRight] PRIMARY KEY CLUSTERED ([CompanyID] ASC, [OperID] ASC, [RightCode] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineOperRight', @level2type = N'COLUMN', @level2name = N'OperID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineOperRight', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作员权限组', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'S_DefineOperRight';


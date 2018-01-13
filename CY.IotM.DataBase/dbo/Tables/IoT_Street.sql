CREATE TABLE [dbo].[IoT_Street] (
    [ID]        BIGINT       IDENTITY (1, 1) NOT NULL,
    [CompanyID] CHAR (4)     NULL,
    [Ser]       VARCHAR (4)  NULL,
    [Name]      VARCHAR (50) NULL,
    CONSTRAINT [PK_IoT_Street] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '街道名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_Street', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '小区编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_Street', @level2type = N'COLUMN', @level2name = N'Ser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '企业代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_Street', @level2type = N'COLUMN', @level2name = N'CompanyID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '街道参数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_Street';


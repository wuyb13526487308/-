CREATE TABLE [dbo].[IoT_Community] (
    [ID]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [StreetID] BIGINT       NULL,
    [Ser]      VARCHAR (4)  NULL,
    [Name]     VARCHAR (50) NULL,
    CONSTRAINT [PK_IoT_Community] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '小区名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_Community', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '小区编码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_Community', @level2type = N'COLUMN', @level2name = N'Ser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '小区表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_Community';


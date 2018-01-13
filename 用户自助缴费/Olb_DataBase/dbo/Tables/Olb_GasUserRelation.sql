CREATE TABLE [dbo].[Olb_GasUserRelation] (
    [Account]   VARCHAR (50) NOT NULL,
    [GasUserID] VARCHAR (50) NOT NULL,
    [CompanyID] VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_Olb_GasUserRelation] PRIMARY KEY CLUSTERED ([Account] ASC, [GasUserID] ASC, [CompanyID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户燃气用户关联表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Olb_GasUserRelation';


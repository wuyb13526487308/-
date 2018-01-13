CREATE TABLE [dbo].[Olb_ChargeOrder] (
    [ID]         VARCHAR (50)    NOT NULL,
    [Account]    VARCHAR (50)    NULL,
    [GasUserID]  VARCHAR (50)    NULL,
    [OrderMoney] DECIMAL (10, 2) NULL,
    [OrderTime]  DATETIME        NULL,
    [CompanyID]  VARCHAR (10)    NULL,
    [Status]     INT             NULL,
    CONSTRAINT [PK_Olb_ChargeOrder] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户充值订单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Olb_ChargeOrder';


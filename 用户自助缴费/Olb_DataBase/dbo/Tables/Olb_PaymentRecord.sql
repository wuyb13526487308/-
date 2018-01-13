CREATE TABLE [dbo].[Olb_PaymentRecord] (
    [Account]        VARCHAR (50)    NULL,
    [GasUserID]      VARCHAR (50)    NULL,
    [GasUserName]    VARCHAR (20)    NULL,
    [GasUserAddress] VARCHAR (200)   NULL,
    [PayMoney]       DECIMAL (10, 2) NULL,
    [PayTime]        DATETIME        NULL,
    [Balance]        DECIMAL (10, 2) NULL,
	[CompanyID]      VARCHAR (10)    NULL,
    [ID]             VARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Olb_PaymentRecord] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户充值缴费记录', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Olb_PaymentRecord';


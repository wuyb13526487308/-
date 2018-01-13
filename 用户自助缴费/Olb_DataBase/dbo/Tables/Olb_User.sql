CREATE TABLE [dbo].[Olb_User] (
    [Account]      VARCHAR (50)  NOT NULL,
    [PassWord]     VARCHAR (50)  NOT NULL,
    [Name]         VARCHAR (20)  NULL,
    [IdentityCard] VARCHAR (50)  NULL,
    [Phone]        VARCHAR (20)  NULL,
    [Address]      VARCHAR (200) NULL,
    [ID]           VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Olb_User] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '网上营业厅用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Olb_User';


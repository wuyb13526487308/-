CREATE TABLE [dbo].[IoT_Pricing](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[RegisterDate] [datetime] NULL,
	[Scope] [varchar](100) NULL,
	[Total] [int] NULL,
	[PriceType] [varchar](50) NULL,
	[PriceContext] [varchar](50) NULL,
	[UseDate] [datetime] NULL,
	[State] [char](1) NULL,
	[Oper] [varchar](50) NULL,
	[Context] [varchar](100) NULL,
	[CompanyID] [char](4) NULL,
	[Price1] [money] NULL,
	[Gas1] [decimal](10, 2) NULL,
	[Price2] [money] NULL,
	[Gas2] [decimal](10, 2) NULL,
	[Price3] [money] NULL,
	[Gas3] [decimal](10, 2) NULL,
	[Price4] [money] NULL,
	[Gas4] [decimal](10, 2) NULL,
	[Price5] [money] NULL,
	[IsUsed] [bit] NULL,
	[Ladder] [int] NULL,
	[SettlementType] [char](2) NULL,
	[MeterType] [char](2) NULL,
 CONSTRAINT [PK_IoT_Pricing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'索引号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'ID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'RegisterDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域，调价对应的范围，可以是多个小区，也可以是全部用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'Scope'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总户数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'Total'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'PriceType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格描述：
例：
启用3阶梯价，价格1：2.5 气量1：30 /价格2：2.9 气量2：20 /价格3：3.5' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'PriceContext'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'启用日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'UseDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态：0 申请 1 撤销  2 完成 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'State'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'Oper'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'Context'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'企业代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing', @level2type=N'COLUMN',@level2name=N'CompanyID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'调价计划表。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IoT_Pricing'
GO
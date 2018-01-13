CREATE TABLE ADItem ( 
	AI_ID bigint identity(1,1)  NOT NULL,    --  索引建 
	AC_ID bigint NULL,    --  外键（广告主题） 
	OrderID smallint NULL,    --  文件序号 
	FileName varchar(20) NULL,    --  文件名称 
	BDate datetime NULL,    --  开始时间 ，只保留年月日 
	EDate datetime NULL,    --  停止时间 ，只保留年月日 
	Length smallint DEFAULT 20 NULL,    --  显示时间长度，单位：秒 
	StoreName char(20) NOT NULL,    --  内容文件存储名称： 企业编码 + yyMMdd +5位序号 +扩展名（含扩展名前面的点） 
	IsDisplay bit DEFAULT 1 NULL,    --  该文件在序列中是否显示 true 显示 
	StorePath varchar(max) NULL,    --  文件存储路径，相对与系统配置路径 
	FileLength int NULL    --  文件长度 
);
GO
ALTER TABLE ADItem ADD CONSTRAINT PK_ADItem 
	PRIMARY KEY CLUSTERED (AI_ID);
GO
EXEC sp_addextendedproperty 'MS_Description', '广告条目表', 'Schema', dbo, 'table', ADItem;
GO
EXEC sp_addextendedproperty 'MS_Description', '索引建', 'Schema', dbo, 'table', ADItem, 'column', AI_ID;
GO
EXEC sp_addextendedproperty 'MS_Description', '外键（广告主题）', 'Schema', dbo, 'table', ADItem, 'column', AC_ID;
GO
EXEC sp_addextendedproperty 'MS_Description', '文件名称', 'Schema', dbo, 'table', ADItem, 'column', FileName;
GO
EXEC sp_addextendedproperty 'MS_Description', '开始时间 ，只保留年月日', 'Schema', dbo, 'table', ADItem, 'column', BDate;
GO
EXEC sp_addextendedproperty 'MS_Description', '停止时间 ，只保留年月日', 'Schema', dbo, 'table', ADItem, 'column', EDate;
GO
EXEC sp_addextendedproperty 'MS_Description', '显示时间长度，单位：秒', 'Schema', dbo, 'table', ADItem, 'column', Length;
GO
EXEC sp_addextendedproperty 'MS_Description', '内容文件存储名称：
企业编码 + yyMMdd +5位序号 +扩展名（含扩展名前面的点）', 'Schema', dbo, 'table', ADItem, 'column', StoreName;
GO
EXEC sp_addextendedproperty 'MS_Description', '该文件在序列中是否显示 true 显示', 'Schema', dbo, 'table', ADItem, 'column', IsDisplay;
GO
EXEC sp_addextendedproperty 'MS_Description', '文件存储路径，相对与系统配置路径', 'Schema', dbo, 'table', ADItem, 'column', StorePath;
GO
--  Create Indexes 
--  Create Indexes 
ALTER TABLE ADItem
	ADD CONSTRAINT UQ_ADItem_StoreName UNIQUE (StoreName);
GO
EXEC sp_addextendedproperty 'MS_Description', '文件序号', 'Schema', dbo, 'table', ADItem, 'column', OrderID;
GO
EXEC sp_addextendedproperty 'MS_Description', '文件长度', 'Schema', dbo, 'table', ADItem, 'column', FileLength;
CREATE TABLE ADUser ( 
	UserID char(10) NOT NULL,    -- 广告用户ID 
	CompanyID char(4) NOT NULL,    -- 企业编码 
	AP_ID bigint NULL,    -- 最后发布的发布ID 
	AC_ID bigint NULL,
	PublishDate datetime NULL,    -- 最后发一次发布的时间 
	Street varchar(50) NULL,    -- 街道（冗余字段） 
	Community varchar(50) NULL,    -- 小区(冗余字段) 
	Adress varchar(100) NULL,    -- 地址 
	Ver char(4) NULL,    -- LCD设备版本 默认：1.00 
	AddTime datetime NULL
);
GO
ALTER TABLE ADUser ADD CONSTRAINT PK_ADUser 
	PRIMARY KEY CLUSTERED (UserID, CompanyID);
GO
EXEC sp_addextendedproperty 'MS_Description', '广告用户表', 'Schema', dbo, 'table', ADUser;
GO
EXEC sp_addextendedproperty 'MS_Description', '广告用户ID', 'Schema', dbo, 'table', ADUser, 'column', UserID;
GO
EXEC sp_addextendedproperty 'MS_Description', '企业编码', 'Schema', dbo, 'table', ADUser, 'column', CompanyID;
GO
EXEC sp_addextendedproperty 'MS_Description', '最后发布的发布ID', 'Schema', dbo, 'table', ADUser, 'column', AP_ID;
GO
EXEC sp_addextendedproperty 'MS_Description', '最后发一次发布的时间', 'Schema', dbo, 'table', ADUser, 'column', PublishDate;
GO
EXEC sp_addextendedproperty 'MS_Description', '街道（冗余字段）', 'Schema', dbo, 'table', ADUser, 'column', Street;
GO
EXEC sp_addextendedproperty 'MS_Description', '小区(冗余字段)', 'Schema', dbo, 'table', ADUser, 'column', Community;
GO
EXEC sp_addextendedproperty 'MS_Description', '地址', 'Schema', dbo, 'table', ADUser, 'column', Adress;
GO
EXEC sp_addextendedproperty 'MS_Description', 'LCD设备版本 默认：1.00', 'Schema', dbo, 'table', ADUser, 'column', Ver;
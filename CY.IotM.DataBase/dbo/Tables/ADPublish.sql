CREATE TABLE ADPublish ( 
	AP_ID bigint identity(1,1)  NOT NULL,    --  广告发布ID（索引号） 
	CompanyID char(4) NULL,    --  企业编码 
	AC_ID bigint NULL,    --  广告主题 
	AreaContext varchar(200) NULL,    --  区域描述 
	PublishDate datetime NULL,    --  发布时间 
	UserCount int NULL,    --  总户数 
	State smallint DEFAULT 0 NULL    --  发布状态 0 未发布 1 已发布  2 需重新发布 
);
GO
ALTER TABLE ADPublish ADD CONSTRAINT PK_ADPublish 
	PRIMARY KEY CLUSTERED (AP_ID);
GO
EXEC sp_addextendedproperty 'MS_Description', '广告发布记录表', 'Schema', dbo, 'table', ADPublish;
GO
EXEC sp_addextendedproperty 'MS_Description', '广告发布ID（索引号）', 'Schema', dbo, 'table', ADPublish, 'column', AP_ID;
GO
EXEC sp_addextendedproperty 'MS_Description', '企业编码', 'Schema', dbo, 'table', ADPublish, 'column', CompanyID;
GO
EXEC sp_addextendedproperty 'MS_Description', '广告主题', 'Schema', dbo, 'table', ADPublish, 'column', AC_ID;
GO
EXEC sp_addextendedproperty 'MS_Description', '区域描述', 'Schema', dbo, 'table', ADPublish, 'column', AreaContext;
GO
EXEC sp_addextendedproperty 'MS_Description', '发布时间', 'Schema', dbo, 'table', ADPublish, 'column', PublishDate;
GO
EXEC sp_addextendedproperty 'MS_Description', '总户数', 'Schema', dbo, 'table', ADPublish, 'column', UserCount;
GO
EXEC sp_addextendedproperty 'MS_Description', '发布状态 0 未发布 1 已发布  2 需重新发布', 'Schema', dbo, 'table', ADPublish, 'column', State;
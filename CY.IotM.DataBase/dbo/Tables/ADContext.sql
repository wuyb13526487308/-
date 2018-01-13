--  Create Tables 
--  Create Tables 
--  Create Tables 
CREATE TABLE ADContext ( 
	AC_ID bigint identity(1,1)  NOT NULL,    --  索引号 
	CompanyID char(4) NULL,    --  企业编码 
	Context varchar(200) NULL,    --  广告主题 
	State smallint DEFAULT 0 NULL,    --  主题状态 0 草稿 1 可发布 2 已发布 
	CreateDate datetime NULL    --  创建日期 
);
GO
--  Create Primary Key Constraints 
--  Create Primary Key Constraints 
--  Create Primary Key Constraints 
ALTER TABLE ADContext ADD CONSTRAINT PK_ADContext 
	PRIMARY KEY CLUSTERED (AC_ID);
GO
EXEC sp_addextendedproperty 'MS_Description', '广告主题表', 'Schema', dbo, 'table', ADContext;
GO
EXEC sp_addextendedproperty 'MS_Description', '索引号', 'Schema', dbo, 'table', ADContext, 'column', AC_ID;
GO
EXEC sp_addextendedproperty 'MS_Description', '企业编码', 'Schema', dbo, 'table', ADContext, 'column', CompanyID;
GO
EXEC sp_addextendedproperty 'MS_Description', '广告主题', 'Schema', dbo, 'table', ADContext, 'column', Context;
GO
EXEC sp_addextendedproperty 'MS_Description', '主题状态 0 草稿 1 可发布 2 已发布', 'Schema', dbo, 'table', ADContext, 'column', State;
GO
EXEC sp_addextendedproperty 'MS_Description', '创建日期', 'Schema', dbo, 'table', ADContext, 'column', CreateDate;
CREATE TABLE ADPublishUser ( 
	ID bigint identity(1,1)  NOT NULL,
	AP_ID bigint NOT NULL,    --  发布ID 
	UserID char(10) NOT NULL,    --  用户ID 
	CompanyID char(4) NULL,
	State smallint DEFAULT 0 NULL,    --  发布状态 0 等待调度  1 发布成功 2 发布失败 
	FinishedDate datetime NULL,    --  发布完成时间 
	Context varchar(max) NULL    --  备注（记录发布失败原因或其他描述） 
);
GO
ALTER TABLE ADPublishUser ADD CONSTRAINT PK_ADPublishUser 
	PRIMARY KEY CLUSTERED (AP_ID, UserID);
GO
EXEC sp_addextendedproperty 'MS_Description', '用户发布列表', 'Schema', dbo, 'table', ADPublishUser;
GO
EXEC sp_addextendedproperty 'MS_Description', '发布ID', 'Schema', dbo, 'table', ADPublishUser, 'column', AP_ID;
GO
EXEC sp_addextendedproperty 'MS_Description', '用户ID', 'Schema', dbo, 'table', ADPublishUser, 'column', UserID;
GO
EXEC sp_addextendedproperty 'MS_Description', '发布状态 0 等待调度  1 发布成功 2 发布失败', 'Schema', dbo, 'table', ADPublishUser, 'column', State;
GO
EXEC sp_addextendedproperty 'MS_Description', '发布完成时间', 'Schema', dbo, 'table', ADPublishUser, 'column', FinishedDate;
GO
EXEC sp_addextendedproperty 'MS_Description', '备注（记录发布失败原因或其他描述）', 'Schema', dbo, 'table', ADPublishUser, 'column', Context;
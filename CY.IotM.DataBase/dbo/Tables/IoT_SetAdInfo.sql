CREATE TABLE [dbo].[IoT_SetAdInfo] (
    [ID]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [FileIndex]     INT           NOT NULL,
    [FileName]      VARCHAR (50)  NOT NULL,
    [SetType]       INT           NOT NULL,
    [StartDate]     VARCHAR (50)  NULL,
    [EndDate]       VARCHAR (50)  NULL,
    [CycleTime]     INT           NULL,
    [PublishStatus] INT           NULL,
    [ShowStatus]    INT           NULL,
    [DeleteStatus]  INT           NULL,
    [CompanyID]     CHAR (4)      NULL,
    [Context]       VARCHAR (100) NULL,
	[SendTime] [datetime] NULL,
    CONSTRAINT [PK_IoT_SetAdInfo] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设置广告信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_SetAdInfo';


CREATE TABLE [dbo].[IoT_AdInfo] (
    [ID]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [FileIndex]     INT             NOT NULL,
    [FileName]      VARCHAR (50)    NOT NULL,
    [FileSize]      INT             NULL,
    [StartDate]     VARCHAR (50)    NULL,
    [EndDate]       VARCHAR (50)    NULL,
    [CycleTime]     INT             NULL,
    [PublishStatus] INT             NULL,
    [ShowStatus]    INT             NULL,
    [FileData]      VARBINARY (MAX) NULL,
    [CompanyID]     CHAR (4)        NULL,
    CONSTRAINT [PK_IoT_AdInfo] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [U_FileIndex] UNIQUE NONCLUSTERED ([FileIndex] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '广告信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IoT_AdInfo';


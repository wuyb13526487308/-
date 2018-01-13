CREATE TABLE [dbo].[ReportTemplate] (
    [RID]            INT          IDENTITY (1, 1) NOT NULL,
    [ReportName]     VARCHAR (50) NOT NULL,
    [ReportType]     SMALLINT     DEFAULT ((0)) NULL,
    [RD_ID]          INT          NULL,
    [ReportTemplate] IMAGE        NULL,
    [MenuName]       VARCHAR (50) NULL,
    [IsUse]          BIT          NULL,
    CONSTRAINT [PK_ReportTemplate] PRIMARY KEY CLUSTERED ([RID] ASC)
);


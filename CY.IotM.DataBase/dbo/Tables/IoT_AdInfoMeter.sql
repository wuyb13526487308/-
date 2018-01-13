CREATE TABLE [dbo].[IoT_AdInfoMeter] (
    [ID]           BIGINT       NOT NULL,
    [MeterID]      BIGINT       NOT NULL,
    [MeterNo]      VARCHAR (20) NULL,
    [State]        CHAR (1)     NULL,
    [FinishedDate] DATETIME     NULL,
    [Context]      VARCHAR (50) NULL,
    [TaskID]       VARCHAR (50) NULL,
    CONSTRAINT [PK_IoT_AdInfoMeter] PRIMARY KEY CLUSTERED ([ID] ASC, [MeterID] ASC)
);


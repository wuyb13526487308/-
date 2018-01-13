CREATE TABLE [dbo].[IoT_UserTemp] (
    [CompanyID] CHAR (4)        NOT NULL,
    [UserID]    CHAR (10)       NOT NULL,
    [UserName]  VARCHAR (50)    NULL,
    [Phone]     VARCHAR (50)    NULL,
    [Street]    VARCHAR (50)    NULL,
    [Community] VARCHAR (50)    NULL,
    [Door]      VARCHAR (50)    NULL,
    [Address]   VARCHAR (100)   NULL,
    [MeterNo]   CHAR (20)       NOT NULL,
    [MeterNum]  DECIMAL (10, 2) NULL,
    CONSTRAINT [PK_IoT_UserTemp] PRIMARY KEY CLUSTERED ([MeterNo] ASC)
);


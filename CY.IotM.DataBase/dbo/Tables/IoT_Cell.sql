﻿CREATE TABLE [dbo].[IoT_Cell]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyId] NCHAR(4) NULL, 
    [CellName] NVARCHAR(50) NULL
)

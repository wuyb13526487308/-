CREATE TABLE [dbo].[IoT_Building]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyId] NCHAR(4) NULL, 
    [Building] NVARCHAR(50) NULL
)

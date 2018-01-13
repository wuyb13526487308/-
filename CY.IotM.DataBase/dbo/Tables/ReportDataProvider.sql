CREATE TABLE ReportDataProvider ( 
	RD_ID int identity(1,1)  NOT NULL,
	DS_Name varchar(50),
	ConnectionType smallint,
	ConnectionString varchar(50),
	QuerySql varchar(7800),
	SqlType smallint
)



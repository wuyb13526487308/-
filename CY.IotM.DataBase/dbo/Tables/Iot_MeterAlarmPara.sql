CREATE TABLE Iot_MeterAlarmPara ( 
	MeterNo varchar(20) NOT NULL,    -- 表号 
	SwitchTag char(16) NULL,
	Par1 int NULL,
	Par2 int NULL,
	Par3 int NULL,
	Par4 char(4) NULL,
	Par5 int NULL,
	Par6 int NULL,
	Par7 int NULL,
	Par8 int NULL,
	Par9 char(2) NULL    -- 燃气表公称流量                    1              BCD        m?h       放大10倍        每个字节表示00~99之间BCD码(先低后高) 
);
GO
EXEC sp_addextendedproperty 'MS_Description', '表报警参数', 'Schema', dbo, 'table', Iot_MeterAlarmPara;
GO
EXEC sp_addextendedproperty 'MS_Description', '表号', 'Schema', dbo, 'table', Iot_MeterAlarmPara, 'column', MeterNo;
GO
EXEC sp_addextendedproperty 'MS_Description', '燃气表公称流量                    1              BCD        m?h       放大10倍        每个字节表示00~99之间BCD码(先低后高)', 'Schema', dbo, 'table', Iot_MeterAlarmPara, 'column', Par9;
GO
ALTER TABLE Iot_MeterAlarmPara ADD CONSTRAINT PK_Iot_MeterAlarmPara 
	PRIMARY KEY CLUSTERED (MeterNo);
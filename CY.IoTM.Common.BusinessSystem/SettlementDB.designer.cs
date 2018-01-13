﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CY.IoTM.Common.Business
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="IotMeter")]
	public partial class SettlementDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertIoT_SettlementDayMeter(IoT_SettlementDayMeter instance);
    partial void UpdateIoT_SettlementDayMeter(IoT_SettlementDayMeter instance);
    partial void DeleteIoT_SettlementDayMeter(IoT_SettlementDayMeter instance);
    partial void InsertIoT_SetSettlementDay(IoT_SetSettlementDay instance);
    partial void UpdateIoT_SetSettlementDay(IoT_SetSettlementDay instance);
    partial void DeleteIoT_SetSettlementDay(IoT_SetSettlementDay instance);
    #endregion
		
		public SettlementDBDataContext() : 
				base(global::CY.IoTM.Common.Business.Properties.Settings.Default.IotMeterConnectionString3, mappingSource)
		{
			OnCreated();
		}
		
		public SettlementDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SettlementDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SettlementDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SettlementDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<IoT_SettlementDayMeter> IoT_SettlementDayMeter
		{
			get
			{
				return this.GetTable<IoT_SettlementDayMeter>();
			}
		}
		
		public System.Data.Linq.Table<View_SettlementDayMeter> View_SettlementDayMeter
		{
			get
			{
				return this.GetTable<View_SettlementDayMeter>();
			}
		}
		
		public System.Data.Linq.Table<IoT_SetSettlementDay> IoT_SetSettlementDay
		{
			get
			{
				return this.GetTable<IoT_SetSettlementDay>();
			}
		}
		
		public System.Data.Linq.Table<View_SettlementDayMeterView> View_SettlementDayMeterView
		{
			get
			{
				return this.GetTable<View_SettlementDayMeterView>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.IoT_SettlementDayMeter")]
	public partial class IoT_SettlementDayMeter : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID;
		
		private long _MeterID;
		
		private string _MeterNo;
		
		private System.Nullable<char> _State;
		
		private System.Nullable<System.DateTime> _FinishedDate;
		
		private string _Context;
		
		private string _TaskID;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(long value);
    partial void OnIDChanged();
    partial void OnMeterIDChanging(long value);
    partial void OnMeterIDChanged();
    partial void OnMeterNoChanging(string value);
    partial void OnMeterNoChanged();
    partial void OnStateChanging(System.Nullable<char> value);
    partial void OnStateChanged();
    partial void OnFinishedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnFinishedDateChanged();
    partial void OnContextChanging(string value);
    partial void OnContextChanged();
    partial void OnTaskIDChanging(string value);
    partial void OnTaskIDChanged();
    #endregion
		
		public IoT_SettlementDayMeter()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="BigInt NOT NULL", IsPrimaryKey=true)]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MeterID", DbType="BigInt NOT NULL", IsPrimaryKey=true)]
		public long MeterID
		{
			get
			{
				return this._MeterID;
			}
			set
			{
				if ((this._MeterID != value))
				{
					this.OnMeterIDChanging(value);
					this.SendPropertyChanging();
					this._MeterID = value;
					this.SendPropertyChanged("MeterID");
					this.OnMeterIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MeterNo", DbType="VarChar(20)")]
		public string MeterNo
		{
			get
			{
				return this._MeterNo;
			}
			set
			{
				if ((this._MeterNo != value))
				{
					this.OnMeterNoChanging(value);
					this.SendPropertyChanging();
					this._MeterNo = value;
					this.SendPropertyChanged("MeterNo");
					this.OnMeterNoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="Char(1)")]
		public System.Nullable<char> State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FinishedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> FinishedDate
		{
			get
			{
				return this._FinishedDate;
			}
			set
			{
				if ((this._FinishedDate != value))
				{
					this.OnFinishedDateChanging(value);
					this.SendPropertyChanging();
					this._FinishedDate = value;
					this.SendPropertyChanged("FinishedDate");
					this.OnFinishedDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarChar(100)")]
		public string Context
		{
			get
			{
				return this._Context;
			}
			set
			{
				if ((this._Context != value))
				{
					this.OnContextChanging(value);
					this.SendPropertyChanging();
					this._Context = value;
					this.SendPropertyChanged("Context");
					this.OnContextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskID", DbType="VarChar(50)")]
		public string TaskID
		{
			get
			{
				return this._TaskID;
			}
			set
			{
				if ((this._TaskID != value))
				{
					this.OnTaskIDChanging(value);
					this.SendPropertyChanging();
					this._TaskID = value;
					this.SendPropertyChanged("TaskID");
					this.OnTaskIDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.View_SettlementDayMeter")]
	public partial class View_SettlementDayMeter
	{
		
		private string _CompanyID;
		
		private string _UserID;
		
		private string _UserName;
		
		private string _Address;
		
		private string _MeterNo;
		
		private System.Nullable<char> _State;
		
		private System.Nullable<System.DateTime> _FinishedDate;
		
		private string _Context;
		
		private long _MeterID;
		
		public View_SettlementDayMeter()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyID", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string CompanyID
		{
			get
			{
				return this._CompanyID;
			}
			set
			{
				if ((this._CompanyID != value))
				{
					this._CompanyID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Char(10) NOT NULL", CanBeNull=false)]
		public string UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserName", DbType="VarChar(50)")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if ((this._UserName != value))
				{
					this._UserName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="VarChar(100)")]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this._Address = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MeterNo", DbType="Char(20) NOT NULL", CanBeNull=false)]
		public string MeterNo
		{
			get
			{
				return this._MeterNo;
			}
			set
			{
				if ((this._MeterNo != value))
				{
					this._MeterNo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="Char(1)")]
		public System.Nullable<char> State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this._State = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FinishedDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> FinishedDate
		{
			get
			{
				return this._FinishedDate;
			}
			set
			{
				if ((this._FinishedDate != value))
				{
					this._FinishedDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarChar(100)")]
		public string Context
		{
			get
			{
				return this._Context;
			}
			set
			{
				if ((this._Context != value))
				{
					this._Context = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MeterID", DbType="BigInt NOT NULL")]
		public long MeterID
		{
			get
			{
				return this._MeterID;
			}
			set
			{
				if ((this._MeterID != value))
				{
					this._MeterID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.IoT_SetSettlementDay")]
	public partial class IoT_SetSettlementDay : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID;
		
		private System.Nullable<System.DateTime> _RegisterDate;
		
		private string _Scope;
		
		private System.Nullable<int> _Total;
		
		private System.Nullable<int> _SettlementDay;
		
		private System.Nullable<int> _SettlementMonth;
		
		private System.Nullable<char> _State;
		
		private string _Oper;
		
		private string _Context;
		
		private string _CompanyID;
		
		private string _TaskID;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(long value);
    partial void OnIDChanged();
    partial void OnRegisterDateChanging(System.Nullable<System.DateTime> value);
    partial void OnRegisterDateChanged();
    partial void OnScopeChanging(string value);
    partial void OnScopeChanged();
    partial void OnTotalChanging(System.Nullable<int> value);
    partial void OnTotalChanged();
    partial void OnSettlementDayChanging(System.Nullable<int> value);
    partial void OnSettlementDayChanged();
    partial void OnSettlementMonthChanging(System.Nullable<int> value);
    partial void OnSettlementMonthChanged();
    partial void OnStateChanging(System.Nullable<char> value);
    partial void OnStateChanged();
    partial void OnOperChanging(string value);
    partial void OnOperChanged();
    partial void OnContextChanging(string value);
    partial void OnContextChanged();
    partial void OnCompanyIDChanging(string value);
    partial void OnCompanyIDChanged();
    partial void OnTaskIDChanging(string value);
    partial void OnTaskIDChanged();
    #endregion
		
		public IoT_SetSettlementDay()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RegisterDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> RegisterDate
		{
			get
			{
				return this._RegisterDate;
			}
			set
			{
				if ((this._RegisterDate != value))
				{
					this.OnRegisterDateChanging(value);
					this.SendPropertyChanging();
					this._RegisterDate = value;
					this.SendPropertyChanged("RegisterDate");
					this.OnRegisterDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Scope", DbType="VarChar(100)")]
		public string Scope
		{
			get
			{
				return this._Scope;
			}
			set
			{
				if ((this._Scope != value))
				{
					this.OnScopeChanging(value);
					this.SendPropertyChanging();
					this._Scope = value;
					this.SendPropertyChanged("Scope");
					this.OnScopeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Total", DbType="Int")]
		public System.Nullable<int> Total
		{
			get
			{
				return this._Total;
			}
			set
			{
				if ((this._Total != value))
				{
					this.OnTotalChanging(value);
					this.SendPropertyChanging();
					this._Total = value;
					this.SendPropertyChanged("Total");
					this.OnTotalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SettlementDay", DbType="Int")]
		public System.Nullable<int> SettlementDay
		{
			get
			{
				return this._SettlementDay;
			}
			set
			{
				if ((this._SettlementDay != value))
				{
					this.OnSettlementDayChanging(value);
					this.SendPropertyChanging();
					this._SettlementDay = value;
					this.SendPropertyChanged("SettlementDay");
					this.OnSettlementDayChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SettlementMonth", DbType="Int")]
		public System.Nullable<int> SettlementMonth
		{
			get
			{
				return this._SettlementMonth;
			}
			set
			{
				if ((this._SettlementMonth != value))
				{
					this.OnSettlementMonthChanging(value);
					this.SendPropertyChanging();
					this._SettlementMonth = value;
					this.SendPropertyChanged("SettlementMonth");
					this.OnSettlementMonthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="Char(1)")]
		public System.Nullable<char> State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Oper", DbType="VarChar(50)")]
		public string Oper
		{
			get
			{
				return this._Oper;
			}
			set
			{
				if ((this._Oper != value))
				{
					this.OnOperChanging(value);
					this.SendPropertyChanging();
					this._Oper = value;
					this.SendPropertyChanged("Oper");
					this.OnOperChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarChar(100)")]
		public string Context
		{
			get
			{
				return this._Context;
			}
			set
			{
				if ((this._Context != value))
				{
					this.OnContextChanging(value);
					this.SendPropertyChanging();
					this._Context = value;
					this.SendPropertyChanged("Context");
					this.OnContextChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyID", DbType="Char(4)")]
		public string CompanyID
		{
			get
			{
				return this._CompanyID;
			}
			set
			{
				if ((this._CompanyID != value))
				{
					this.OnCompanyIDChanging(value);
					this.SendPropertyChanging();
					this._CompanyID = value;
					this.SendPropertyChanged("CompanyID");
					this.OnCompanyIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskID", DbType="VarChar(50)")]
		public string TaskID
		{
			get
			{
				return this._TaskID;
			}
			set
			{
				if ((this._TaskID != value))
				{
					this.OnTaskIDChanging(value);
					this.SendPropertyChanging();
					this._TaskID = value;
					this.SendPropertyChanged("TaskID");
					this.OnTaskIDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.View_SettlementDayMeterView")]
	public partial class View_SettlementDayMeterView
	{
		
		private long _DayID;
		
		private System.Nullable<System.DateTime> _RegisterDate;
		
		private string _Scope;
		
		private System.Nullable<int> _Total;
		
		private System.Nullable<int> _SettlementDay;
		
		private System.Nullable<int> _SettlementMonth;
		
		private System.Nullable<char> _State;
		
		private string _Oper;
		
		private string _Context;
		
		private string _CompanyID;
		
		private string _TaskID;
		
		private System.Nullable<int> _FailCount;
		
		public View_SettlementDayMeterView()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DayID", AutoSync=AutoSync.Always, DbType="BigInt NOT NULL IDENTITY", IsDbGenerated=true)]
		public long DayID
		{
			get
			{
				return this._DayID;
			}
			set
			{
				if ((this._DayID != value))
				{
					this._DayID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RegisterDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> RegisterDate
		{
			get
			{
				return this._RegisterDate;
			}
			set
			{
				if ((this._RegisterDate != value))
				{
					this._RegisterDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Scope", DbType="VarChar(100)")]
		public string Scope
		{
			get
			{
				return this._Scope;
			}
			set
			{
				if ((this._Scope != value))
				{
					this._Scope = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Total", DbType="Int")]
		public System.Nullable<int> Total
		{
			get
			{
				return this._Total;
			}
			set
			{
				if ((this._Total != value))
				{
					this._Total = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SettlementDay", DbType="Int")]
		public System.Nullable<int> SettlementDay
		{
			get
			{
				return this._SettlementDay;
			}
			set
			{
				if ((this._SettlementDay != value))
				{
					this._SettlementDay = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SettlementMonth", DbType="Int")]
		public System.Nullable<int> SettlementMonth
		{
			get
			{
				return this._SettlementMonth;
			}
			set
			{
				if ((this._SettlementMonth != value))
				{
					this._SettlementMonth = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="Char(1)")]
		public System.Nullable<char> State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this._State = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Oper", DbType="VarChar(50)")]
		public string Oper
		{
			get
			{
				return this._Oper;
			}
			set
			{
				if ((this._Oper != value))
				{
					this._Oper = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarChar(100)")]
		public string Context
		{
			get
			{
				return this._Context;
			}
			set
			{
				if ((this._Context != value))
				{
					this._Context = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyID", DbType="Char(4)")]
		public string CompanyID
		{
			get
			{
				return this._CompanyID;
			}
			set
			{
				if ((this._CompanyID != value))
				{
					this._CompanyID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskID", DbType="VarChar(50)")]
		public string TaskID
		{
			get
			{
				return this._TaskID;
			}
			set
			{
				if ((this._TaskID != value))
				{
					this._TaskID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FailCount", DbType="Int")]
		public System.Nullable<int> FailCount
		{
			get
			{
				return this._FailCount;
			}
			set
			{
				if ((this._FailCount != value))
				{
					this._FailCount = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
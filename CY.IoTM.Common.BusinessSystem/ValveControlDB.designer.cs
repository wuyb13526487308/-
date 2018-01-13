﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
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
	public partial class ValveControlDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertIoT_ValveControl(IoT_ValveControl instance);
    partial void UpdateIoT_ValveControl(IoT_ValveControl instance);
    partial void DeleteIoT_ValveControl(IoT_ValveControl instance);
    #endregion
		
		public ValveControlDBDataContext() : 
				base(global::CY.IoTM.Common.Business.Properties.Settings.Default.IotMeterConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ValveControlDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ValveControlDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ValveControlDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ValveControlDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<IoT_ValveControl> IoT_ValveControl
		{
			get
			{
				return this.GetTable<IoT_ValveControl>();
			}
		}
		
		public System.Data.Linq.Table<View_ValveControl> View_ValveControl
		{
			get
			{
				return this.GetTable<View_ValveControl>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.IoT_ValveControl")]
	public partial class IoT_ValveControl : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _ID;
		
		private string _UserID;
		
		private System.Nullable<long> _MeterID;
		
		private string _MeterNo;
		
		private System.Nullable<System.DateTime> _RegisterDate;
		
		private string _Reason;
		
		private System.Nullable<char> _ControlType;
		
		private System.Nullable<char> _State;
		
		private System.Nullable<System.DateTime> _FinishedDate;
		
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
    partial void OnUserIDChanging(string value);
    partial void OnUserIDChanged();
    partial void OnMeterIDChanging(System.Nullable<long> value);
    partial void OnMeterIDChanged();
    partial void OnMeterNoChanging(string value);
    partial void OnMeterNoChanged();
    partial void OnRegisterDateChanging(System.Nullable<System.DateTime> value);
    partial void OnRegisterDateChanged();
    partial void OnReasonChanging(string value);
    partial void OnReasonChanged();
    partial void OnControlTypeChanging(System.Nullable<char> value);
    partial void OnControlTypeChanged();
    partial void OnStateChanging(System.Nullable<char> value);
    partial void OnStateChanged();
    partial void OnFinishedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnFinishedDateChanged();
    partial void OnOperChanging(string value);
    partial void OnOperChanged();
    partial void OnContextChanging(string value);
    partial void OnContextChanged();
    partial void OnCompanyIDChanging(string value);
    partial void OnCompanyIDChanged();
    partial void OnTaskIDChanging(string value);
    partial void OnTaskIDChanged();
    #endregion
		
		public IoT_ValveControl()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.Always, DbType="BigInt NOT NULL IDENTITY", IsDbGenerated=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Char(10)")]
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
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MeterID", DbType="BigInt")]
		public System.Nullable<long> MeterID
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Reason", DbType="VarChar(500)")]
		public string Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				if ((this._Reason != value))
				{
					this.OnReasonChanging(value);
					this.SendPropertyChanging();
					this._Reason = value;
					this.SendPropertyChanged("Reason");
					this.OnReasonChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ControlType", DbType="Char(1)")]
		public System.Nullable<char> ControlType
		{
			get
			{
				return this._ControlType;
			}
			set
			{
				if ((this._ControlType != value))
				{
					this.OnControlTypeChanging(value);
					this.SendPropertyChanging();
					this._ControlType = value;
					this.SendPropertyChanged("ControlType");
					this.OnControlTypeChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarChar(50)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskID", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.View_ValveControl")]
	public partial class View_ValveControl
	{
		
		private string _CompanyID;
		
		private string _UserID;
		
		private string _UserName;
		
		private string _Address;
		
		private string _MeterNo;
		
		private System.Nullable<System.DateTime> _RegisterDate;
		
		private System.Nullable<char> _State;
		
		private System.Nullable<char> _ControlType;
		
		private string _Oper;
		
		private string _Reason;
		
		private System.Nullable<System.DateTime> _FinishedDate;
		
		private string _Context;
		
		private string _TaskID;
		
		public View_ValveControl()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ControlType", DbType="Char(1)")]
		public System.Nullable<char> ControlType
		{
			get
			{
				return this._ControlType;
			}
			set
			{
				if ((this._ControlType != value))
				{
					this._ControlType = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Reason", DbType="VarChar(500)")]
		public string Reason
		{
			get
			{
				return this._Reason;
			}
			set
			{
				if ((this._Reason != value))
				{
					this._Reason = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarChar(50)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TaskID", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
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
	}
}
#pragma warning restore 1591

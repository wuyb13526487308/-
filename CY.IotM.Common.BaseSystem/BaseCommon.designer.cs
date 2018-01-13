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

namespace CY.IotM.Common
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
	public partial class BaseCommonDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertCompanyInfo(CompanyInfo instance);
    partial void UpdateCompanyInfo(CompanyInfo instance);
    partial void DeleteCompanyInfo(CompanyInfo instance);
    partial void InsertCompanyMenu(CompanyMenu instance);
    partial void UpdateCompanyMenu(CompanyMenu instance);
    partial void DeleteCompanyMenu(CompanyMenu instance);
    partial void InsertCompanyOperator(CompanyOperator instance);
    partial void UpdateCompanyOperator(CompanyOperator instance);
    partial void DeleteCompanyOperator(CompanyOperator instance);
    partial void InsertMenuInfo(MenuInfo instance);
    partial void UpdateMenuInfo(MenuInfo instance);
    partial void DeleteMenuInfo(MenuInfo instance);
    partial void InsertSystemLog(SystemLog instance);
    partial void UpdateSystemLog(SystemLog instance);
    partial void DeleteSystemLog(SystemLog instance);
    partial void InsertReportTemplate(ReportTemplate instance);
    partial void UpdateReportTemplate(ReportTemplate instance);
    partial void DeleteReportTemplate(ReportTemplate instance);
    #endregion
		
		public BaseCommonDataContext() : 
				base(global::CY.IotM.Common.Properties.Settings.Default.IotMeterConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public BaseCommonDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BaseCommonDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BaseCommonDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public BaseCommonDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CompanyInfo> CompanyInfo
		{
			get
			{
				return this.GetTable<CompanyInfo>();
			}
		}
		
		public System.Data.Linq.Table<CompanyMenu> CompanyMenu
		{
			get
			{
				return this.GetTable<CompanyMenu>();
			}
		}
		
		public System.Data.Linq.Table<CompanyOperator> CompanyOperator
		{
			get
			{
				return this.GetTable<CompanyOperator>();
			}
		}
		
		public System.Data.Linq.Table<MenuInfo> MenuInfo
		{
			get
			{
				return this.GetTable<MenuInfo>();
			}
		}
		
		public System.Data.Linq.Table<SystemLog> SystemLog
		{
			get
			{
				return this.GetTable<SystemLog>();
			}
		}
		
		public System.Data.Linq.Table<ReportTemplate> ReportTemplate
		{
			get
			{
				return this.GetTable<ReportTemplate>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.S_CompanyInfo")]
	public partial class CompanyInfo : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _CompanyID;
		
		private string _CompanyName;
		
		private string _SimpleName;
		
		private string _Provinces;
		
		private string _City;
		
		private string _Address;
		
		private string _Linkman;
		
		private string _Phone;
		
		private string _URL;
		
		private System.Nullable<short> _Status;
		
		private System.Nullable<System.DateTime> _CreateDate;
		
		private string _Context;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCompanyIDChanging(string value);
    partial void OnCompanyIDChanged();
    partial void OnCompanyNameChanging(string value);
    partial void OnCompanyNameChanged();
    partial void OnSimpleNameChanging(string value);
    partial void OnSimpleNameChanged();
    partial void OnProvincesChanging(string value);
    partial void OnProvincesChanged();
    partial void OnCityChanging(string value);
    partial void OnCityChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnLinkmanChanging(string value);
    partial void OnLinkmanChanged();
    partial void OnPhoneChanging(string value);
    partial void OnPhoneChanged();
    partial void OnURLChanging(string value);
    partial void OnURLChanged();
    partial void OnStatusChanging(System.Nullable<short> value);
    partial void OnStatusChanged();
    partial void OnCreateDateChanging(System.Nullable<System.DateTime> value);
    partial void OnCreateDateChanged();
    partial void OnContextChanging(string value);
    partial void OnContextChanged();
    #endregion
		
		public CompanyInfo()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyID", DbType="Char(4) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyName", DbType="VarChar(50)")]
		public string CompanyName
		{
			get
			{
				return this._CompanyName;
			}
			set
			{
				if ((this._CompanyName != value))
				{
					this.OnCompanyNameChanging(value);
					this.SendPropertyChanging();
					this._CompanyName = value;
					this.SendPropertyChanged("CompanyName");
					this.OnCompanyNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SimpleName", DbType="VarChar(50)")]
		public string SimpleName
		{
			get
			{
				return this._SimpleName;
			}
			set
			{
				if ((this._SimpleName != value))
				{
					this.OnSimpleNameChanging(value);
					this.SendPropertyChanging();
					this._SimpleName = value;
					this.SendPropertyChanged("SimpleName");
					this.OnSimpleNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Provinces", DbType="VarChar(50)")]
		public string Provinces
		{
			get
			{
				return this._Provinces;
			}
			set
			{
				if ((this._Provinces != value))
				{
					this.OnProvincesChanging(value);
					this.SendPropertyChanging();
					this._Provinces = value;
					this.SendPropertyChanged("Provinces");
					this.OnProvincesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_City", DbType="VarChar(50)")]
		public string City
		{
			get
			{
				return this._City;
			}
			set
			{
				if ((this._City != value))
				{
					this.OnCityChanging(value);
					this.SendPropertyChanging();
					this._City = value;
					this.SendPropertyChanged("City");
					this.OnCityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="VarChar(50)")]
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
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Linkman", DbType="VarChar(50)")]
		public string Linkman
		{
			get
			{
				return this._Linkman;
			}
			set
			{
				if ((this._Linkman != value))
				{
					this.OnLinkmanChanging(value);
					this.SendPropertyChanging();
					this._Linkman = value;
					this.SendPropertyChanged("Linkman");
					this.OnLinkmanChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Phone", DbType="VarChar(50)")]
		public string Phone
		{
			get
			{
				return this._Phone;
			}
			set
			{
				if ((this._Phone != value))
				{
					this.OnPhoneChanging(value);
					this.SendPropertyChanging();
					this._Phone = value;
					this.SendPropertyChanged("Phone");
					this.OnPhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_URL", DbType="VarChar(50)")]
		public string URL
		{
			get
			{
				return this._URL;
			}
			set
			{
				if ((this._URL != value))
				{
					this.OnURLChanging(value);
					this.SendPropertyChanging();
					this._URL = value;
					this.SendPropertyChanged("URL");
					this.OnURLChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="SmallInt")]
		public System.Nullable<short> Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				if ((this._CreateDate != value))
				{
					this.OnCreateDateChanging(value);
					this.SendPropertyChanging();
					this._CreateDate = value;
					this.SendPropertyChanged("CreateDate");
					this.OnCreateDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarChar(500)")]
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.S_CompanyMenu")]
	public partial class CompanyMenu : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _CompanyID;
		
		private string _MenuCode;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCompanyIDChanging(string value);
    partial void OnCompanyIDChanged();
    partial void OnMenuCodeChanging(string value);
    partial void OnMenuCodeChanged();
    #endregion
		
		public CompanyMenu()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyID", DbType="Char(4) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MenuCode", DbType="Char(10) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string MenuCode
		{
			get
			{
				return this._MenuCode;
			}
			set
			{
				if ((this._MenuCode != value))
				{
					this.OnMenuCodeChanging(value);
					this.SendPropertyChanging();
					this._MenuCode = value;
					this.SendPropertyChanged("MenuCode");
					this.OnMenuCodeChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.S_CompanyOperator")]
	public partial class CompanyOperator : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _OperID;
		
		private string _CompanyID;
		
		private string _Pwd;
		
		private string _Name;
		
		private System.Nullable<char> _Sex;
		
		private string _Phone;
		
		private System.Nullable<bool> _PhoneLogin;
		
		private string _Mail;
		
		private System.Nullable<char> _State;
		
		private System.Nullable<short> _OperType;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOperIDChanging(string value);
    partial void OnOperIDChanged();
    partial void OnCompanyIDChanging(string value);
    partial void OnCompanyIDChanged();
    partial void OnPwdChanging(string value);
    partial void OnPwdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnSexChanging(System.Nullable<char> value);
    partial void OnSexChanged();
    partial void OnPhoneChanging(string value);
    partial void OnPhoneChanged();
    partial void OnPhoneLoginChanging(System.Nullable<bool> value);
    partial void OnPhoneLoginChanged();
    partial void OnMailChanging(string value);
    partial void OnMailChanged();
    partial void OnStateChanging(System.Nullable<char> value);
    partial void OnStateChanged();
    partial void OnOperTypeChanging(System.Nullable<short> value);
    partial void OnOperTypeChanged();
    #endregion
		
		public CompanyOperator()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OperID", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string OperID
		{
			get
			{
				return this._OperID;
			}
			set
			{
				if ((this._OperID != value))
				{
					this.OnOperIDChanging(value);
					this.SendPropertyChanging();
					this._OperID = value;
					this.SendPropertyChanged("OperID");
					this.OnOperIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyID", DbType="Char(4) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Pwd", DbType="VarChar(50)")]
		public string Pwd
		{
			get
			{
				return this._Pwd;
			}
			set
			{
				if ((this._Pwd != value))
				{
					this.OnPwdChanging(value);
					this.SendPropertyChanging();
					this._Pwd = value;
					this.SendPropertyChanged("Pwd");
					this.OnPwdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50)")]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sex", DbType="Char(1)")]
		public System.Nullable<char> Sex
		{
			get
			{
				return this._Sex;
			}
			set
			{
				if ((this._Sex != value))
				{
					this.OnSexChanging(value);
					this.SendPropertyChanging();
					this._Sex = value;
					this.SendPropertyChanged("Sex");
					this.OnSexChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Phone", DbType="Char(11)")]
		public string Phone
		{
			get
			{
				return this._Phone;
			}
			set
			{
				if ((this._Phone != value))
				{
					this.OnPhoneChanging(value);
					this.SendPropertyChanging();
					this._Phone = value;
					this.SendPropertyChanged("Phone");
					this.OnPhoneChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhoneLogin", DbType="Bit")]
		public System.Nullable<bool> PhoneLogin
		{
			get
			{
				return this._PhoneLogin;
			}
			set
			{
				if ((this._PhoneLogin != value))
				{
					this.OnPhoneLoginChanging(value);
					this.SendPropertyChanging();
					this._PhoneLogin = value;
					this.SendPropertyChanged("PhoneLogin");
					this.OnPhoneLoginChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Mail", DbType="VarChar(50)")]
		public string Mail
		{
			get
			{
				return this._Mail;
			}
			set
			{
				if ((this._Mail != value))
				{
					this.OnMailChanging(value);
					this.SendPropertyChanging();
					this._Mail = value;
					this.SendPropertyChanged("Mail");
					this.OnMailChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OperType", DbType="SmallInt")]
		public System.Nullable<short> OperType
		{
			get
			{
				return this._OperType;
			}
			set
			{
				if ((this._OperType != value))
				{
					this.OnOperTypeChanging(value);
					this.SendPropertyChanging();
					this._OperType = value;
					this.SendPropertyChanged("OperType");
					this.OnOperTypeChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.S_DefineMenu")]
	public partial class MenuInfo : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _MenuCode;
		
		private string _Name;
		
		private string _Type;
		
		private string _UrlClass;
		
		private string _ImageUrl;
		
		private short _OrderNum;
		
		private string _FatherCode;
		
		private System.Nullable<int> _RID;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMenuCodeChanging(string value);
    partial void OnMenuCodeChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnUrlClassChanging(string value);
    partial void OnUrlClassChanged();
    partial void OnImageUrlChanging(string value);
    partial void OnImageUrlChanged();
    partial void OnOrderNumChanging(short value);
    partial void OnOrderNumChanged();
    partial void OnFatherCodeChanging(string value);
    partial void OnFatherCodeChanged();
    partial void OnRIDChanging(System.Nullable<int> value);
    partial void OnRIDChanged();
    #endregion
		
		public MenuInfo()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MenuCode", DbType="Char(10) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string MenuCode
		{
			get
			{
				return this._MenuCode;
			}
			set
			{
				if ((this._MenuCode != value))
				{
					this.OnMenuCodeChanging(value);
					this.SendPropertyChanging();
					this._MenuCode = value;
					this.SendPropertyChanged("MenuCode");
					this.OnMenuCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="Char(2) NOT NULL", CanBeNull=false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UrlClass", DbType="VarChar(100)")]
		public string UrlClass
		{
			get
			{
				return this._UrlClass;
			}
			set
			{
				if ((this._UrlClass != value))
				{
					this.OnUrlClassChanging(value);
					this.SendPropertyChanging();
					this._UrlClass = value;
					this.SendPropertyChanged("UrlClass");
					this.OnUrlClassChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ImageUrl", DbType="VarChar(20)")]
		public string ImageUrl
		{
			get
			{
				return this._ImageUrl;
			}
			set
			{
				if ((this._ImageUrl != value))
				{
					this.OnImageUrlChanging(value);
					this.SendPropertyChanging();
					this._ImageUrl = value;
					this.SendPropertyChanged("ImageUrl");
					this.OnImageUrlChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OrderNum", DbType="SmallInt NOT NULL")]
		public short OrderNum
		{
			get
			{
				return this._OrderNum;
			}
			set
			{
				if ((this._OrderNum != value))
				{
					this.OnOrderNumChanging(value);
					this.SendPropertyChanging();
					this._OrderNum = value;
					this.SendPropertyChanged("OrderNum");
					this.OnOrderNumChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FatherCode", DbType="Char(10)")]
		public string FatherCode
		{
			get
			{
				return this._FatherCode;
			}
			set
			{
				if ((this._FatherCode != value))
				{
					this.OnFatherCodeChanging(value);
					this.SendPropertyChanging();
					this._FatherCode = value;
					this.SendPropertyChanged("FatherCode");
					this.OnFatherCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RID", DbType="Int")]
		public System.Nullable<int> RID
		{
			get
			{
				return this._RID;
			}
			set
			{
				if ((this._RID != value))
				{
					this.OnRIDChanging(value);
					this.SendPropertyChanging();
					this._RID = value;
					this.SendPropertyChanged("RID");
					this.OnRIDChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.S_SystemLog")]
	public partial class SystemLog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _LogID;
		
		private short _LogType;
		
		private string _OperID;
		
		private string _OperName;
		
		private System.DateTime _LogTime;
		
		private string _LoginIP;
		
		private string _LoginBrowser;
		
		private string _LoginSystem;
		
		private string _Context;
		
		private string _CompanyID;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnLogIDChanging(long value);
    partial void OnLogIDChanged();
    partial void OnLogTypeChanging(short value);
    partial void OnLogTypeChanged();
    partial void OnOperIDChanging(string value);
    partial void OnOperIDChanged();
    partial void OnOperNameChanging(string value);
    partial void OnOperNameChanged();
    partial void OnLogTimeChanging(System.DateTime value);
    partial void OnLogTimeChanged();
    partial void OnLoginIPChanging(string value);
    partial void OnLoginIPChanged();
    partial void OnLoginBrowserChanging(string value);
    partial void OnLoginBrowserChanged();
    partial void OnLoginSystemChanging(string value);
    partial void OnLoginSystemChanged();
    partial void OnContextChanging(string value);
    partial void OnContextChanged();
    partial void OnCompanyIDChanging(string value);
    partial void OnCompanyIDChanged();
    #endregion
		
		public SystemLog()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LogID", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public long LogID
		{
			get
			{
				return this._LogID;
			}
			set
			{
				if ((this._LogID != value))
				{
					this.OnLogIDChanging(value);
					this.SendPropertyChanging();
					this._LogID = value;
					this.SendPropertyChanged("LogID");
					this.OnLogIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LogType", DbType="SmallInt NOT NULL")]
		public short LogType
		{
			get
			{
				return this._LogType;
			}
			set
			{
				if ((this._LogType != value))
				{
					this.OnLogTypeChanging(value);
					this.SendPropertyChanging();
					this._LogType = value;
					this.SendPropertyChanged("LogType");
					this.OnLogTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OperID", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string OperID
		{
			get
			{
				return this._OperID;
			}
			set
			{
				if ((this._OperID != value))
				{
					this.OnOperIDChanging(value);
					this.SendPropertyChanging();
					this._OperID = value;
					this.SendPropertyChanged("OperID");
					this.OnOperIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OperName", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string OperName
		{
			get
			{
				return this._OperName;
			}
			set
			{
				if ((this._OperName != value))
				{
					this.OnOperNameChanging(value);
					this.SendPropertyChanging();
					this._OperName = value;
					this.SendPropertyChanged("OperName");
					this.OnOperNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LogTime", DbType="DateTime NOT NULL")]
		public System.DateTime LogTime
		{
			get
			{
				return this._LogTime;
			}
			set
			{
				if ((this._LogTime != value))
				{
					this.OnLogTimeChanging(value);
					this.SendPropertyChanging();
					this._LogTime = value;
					this.SendPropertyChanged("LogTime");
					this.OnLogTimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LoginIP", DbType="VarChar(17) NOT NULL", CanBeNull=false)]
		public string LoginIP
		{
			get
			{
				return this._LoginIP;
			}
			set
			{
				if ((this._LoginIP != value))
				{
					this.OnLoginIPChanging(value);
					this.SendPropertyChanging();
					this._LoginIP = value;
					this.SendPropertyChanged("LoginIP");
					this.OnLoginIPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LoginBrowser", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string LoginBrowser
		{
			get
			{
				return this._LoginBrowser;
			}
			set
			{
				if ((this._LoginBrowser != value))
				{
					this.OnLoginBrowserChanging(value);
					this.SendPropertyChanging();
					this._LoginBrowser = value;
					this.SendPropertyChanged("LoginBrowser");
					this.OnLoginBrowserChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LoginSystem", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string LoginSystem
		{
			get
			{
				return this._LoginSystem;
			}
			set
			{
				if ((this._LoginSystem != value))
				{
					this.OnLoginSystemChanging(value);
					this.SendPropertyChanging();
					this._LoginSystem = value;
					this.SendPropertyChanged("LoginSystem");
					this.OnLoginSystemChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Context", DbType="VarChar(2000)")]
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
					this.OnCompanyIDChanging(value);
					this.SendPropertyChanging();
					this._CompanyID = value;
					this.SendPropertyChanged("CompanyID");
					this.OnCompanyIDChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ReportTemplate")]
	public partial class ReportTemplate : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _RID;
		
		private string _ReportName;
		
		private System.Nullable<short> _ReportType;
		
		private System.Nullable<int> _RD_ID;
		
		private System.Data.Linq.Binary _ReportTemplate1;
		
		private string _MenuName;
		
		private System.Nullable<bool> _IsUse;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnRIDChanging(int value);
    partial void OnRIDChanged();
    partial void OnReportNameChanging(string value);
    partial void OnReportNameChanged();
    partial void OnReportTypeChanging(System.Nullable<short> value);
    partial void OnReportTypeChanged();
    partial void OnRD_IDChanging(System.Nullable<int> value);
    partial void OnRD_IDChanged();
    partial void OnReportTemplate1Changing(System.Data.Linq.Binary value);
    partial void OnReportTemplate1Changed();
    partial void OnMenuNameChanging(string value);
    partial void OnMenuNameChanged();
    partial void OnIsUseChanging(System.Nullable<bool> value);
    partial void OnIsUseChanged();
    #endregion
		
		public ReportTemplate()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int RID
		{
			get
			{
				return this._RID;
			}
			set
			{
				if ((this._RID != value))
				{
					this.OnRIDChanging(value);
					this.SendPropertyChanging();
					this._RID = value;
					this.SendPropertyChanged("RID");
					this.OnRIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReportName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ReportName
		{
			get
			{
				return this._ReportName;
			}
			set
			{
				if ((this._ReportName != value))
				{
					this.OnReportNameChanging(value);
					this.SendPropertyChanging();
					this._ReportName = value;
					this.SendPropertyChanged("ReportName");
					this.OnReportNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ReportType", DbType="SmallInt")]
		public System.Nullable<short> ReportType
		{
			get
			{
				return this._ReportType;
			}
			set
			{
				if ((this._ReportType != value))
				{
					this.OnReportTypeChanging(value);
					this.SendPropertyChanging();
					this._ReportType = value;
					this.SendPropertyChanged("ReportType");
					this.OnReportTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RD_ID", DbType="Int")]
		public System.Nullable<int> RD_ID
		{
			get
			{
				return this._RD_ID;
			}
			set
			{
				if ((this._RD_ID != value))
				{
					this.OnRD_IDChanging(value);
					this.SendPropertyChanging();
					this._RD_ID = value;
					this.SendPropertyChanged("RD_ID");
					this.OnRD_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="ReportTemplate", Storage="_ReportTemplate1", DbType="Image", UpdateCheck=UpdateCheck.Never)]
		public System.Data.Linq.Binary ReportTemplate1
		{
			get
			{
				return this._ReportTemplate1;
			}
			set
			{
				if ((this._ReportTemplate1 != value))
				{
					this.OnReportTemplate1Changing(value);
					this.SendPropertyChanging();
					this._ReportTemplate1 = value;
					this.SendPropertyChanged("ReportTemplate1");
					this.OnReportTemplate1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MenuName", DbType="VarChar(50)")]
		public string MenuName
		{
			get
			{
				return this._MenuName;
			}
			set
			{
				if ((this._MenuName != value))
				{
					this.OnMenuNameChanging(value);
					this.SendPropertyChanging();
					this._MenuName = value;
					this.SendPropertyChanged("MenuName");
					this.OnMenuNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsUse", DbType="Bit")]
		public System.Nullable<bool> IsUse
		{
			get
			{
				return this._IsUse;
			}
			set
			{
				if ((this._IsUse != value))
				{
					this.OnIsUseChanging(value);
					this.SendPropertyChanging();
					this._IsUse = value;
					this.SendPropertyChanged("IsUse");
					this.OnIsUseChanged();
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
}
#pragma warning restore 1591

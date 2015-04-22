﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34014
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilesContext
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="filInfo")]
	public partial class FolderModelDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertFiles(FilesEntities.Files instance);
    partial void UpdateFiles(FilesEntities.Files instance);
    partial void DeleteFiles(FilesEntities.Files instance);
    #endregion
		
		public FolderModelDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["filInfoConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public FolderModelDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FolderModelDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FolderModelDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public FolderModelDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<FilesEntities.Files> Files
		{
			get
			{
				return this.GetTable<FilesEntities.Files>();
			}
		}
	}
}
namespace FilesEntities
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.[Table]")]
	public partial class Files : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _pkid;
		
		private System.Nullable<int> _Id;
		
		private System.Nullable<int> _PID;
		
		private string _Name;
		
		private long _Size;
		
		private string _Type;
		
		private System.DateTime _CreateTime;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnpkidChanging(int value);
    partial void OnpkidChanged();
    partial void OnIdChanging(System.Nullable<int> value);
    partial void OnIdChanged();
    partial void OnPIDChanging(System.Nullable<int> value);
    partial void OnPIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnSizeChanging(long value);
    partial void OnSizeChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnCreateTimeChanging(System.DateTime value);
    partial void OnCreateTimeChanged();
    #endregion
		
		public Files()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pkid", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int pkid
		{
			get
			{
				return this._pkid;
			}
			set
			{
				if ((this._pkid != value))
				{
					this.OnpkidChanging(value);
					this.SendPropertyChanging();
					this._pkid = value;
					this.SendPropertyChanged("pkid");
					this.OnpkidChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", DbType="Int")]
		public System.Nullable<int> Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PID", DbType="Int")]
		public System.Nullable<int> PID
		{
			get
			{
				return this._PID;
			}
			set
			{
				if ((this._PID != value))
				{
					this.OnPIDChanging(value);
					this.SendPropertyChanging();
					this._PID = value;
					this.SendPropertyChanged("PID");
					this.OnPIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Size", DbType="BigInt NOT NULL")]
		public long Size
		{
			get
			{
				return this._Size;
			}
			set
			{
				if ((this._Size != value))
				{
					this.OnSizeChanging(value);
					this.SendPropertyChanging();
					this._Size = value;
					this.SendPropertyChanged("Size");
					this.OnSizeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="NChar(10) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateTime", DbType="DateTime NOT NULL")]
		public System.DateTime CreateTime
		{
			get
			{
				return this._CreateTime;
			}
			set
			{
				if ((this._CreateTime != value))
				{
					this.OnCreateTimeChanging(value);
					this.SendPropertyChanging();
					this._CreateTime = value;
					this.SendPropertyChanged("CreateTime");
					this.OnCreateTimeChanged();
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
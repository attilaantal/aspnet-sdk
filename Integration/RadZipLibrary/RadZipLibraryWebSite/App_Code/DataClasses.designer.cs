﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DataContext
{

    [global::System.Data.Linq.Mapping.DatabaseAttribute(Name = "Database")]
    public partial class DataClassesDataContext : System.Data.Linq.DataContext
    {

        private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();

        #region Extensibility Method Definitions
        partial void OnCreated();
        partial void InsertAlbum(Album instance);
        partial void UpdateAlbum(Album instance);
        partial void DeleteAlbum(Album instance);
        partial void InsertImage(Image instance);
        partial void UpdateImage(Image instance);
        partial void DeleteImage(Image instance);
        #endregion

        public DataClassesDataContext() :
            base(global::System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString, mappingSource)
        {
            OnCreated();
        }

        public DataClassesDataContext(string connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataClassesDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            OnCreated();
        }

        public System.Data.Linq.Table<Album> Albums
        {
            get
            {
                return this.GetTable<Album>();
            }
        }

        public System.Data.Linq.Table<Image> Images
        {
            get
            {
                return this.GetTable<Image>();
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Albums")]
    public partial class Album : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _ID;

        private string _Name;

        private byte[] _Thumbnail;

        private string _Description;

        private EntitySet<Image> _Images;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        partial void OnThumbnailChanging(byte[] value);
        partial void OnThumbnailChanged();
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
        #endregion

        public Album()
        {
            this._Images = new EntitySet<Image>(new Action<Image>(this.attach_Images), new Action<Image>(this.detach_Images));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Name", DbType = "NVarChar(400) NOT NULL", CanBeNull = false)]
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thumbnail", DbType = "Image", UpdateCheck = UpdateCheck.Never)]
        public byte[] Thumbnail
        {
            get
            {
                return this._Thumbnail;
            }
            set
            {
                if ((this._Thumbnail != value))
                {
                    this.OnThumbnailChanging(value);
                    this.SendPropertyChanging();
                    this._Thumbnail = value;
                    this.SendPropertyChanged("Thumbnail");
                    this.OnThumbnailChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Description", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                if ((this._Description != value))
                {
                    this.OnDescriptionChanging(value);
                    this.SendPropertyChanging();
                    this._Description = value;
                    this.SendPropertyChanged("Description");
                    this.OnDescriptionChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Album_Image", Storage = "_Images", ThisKey = "ID", OtherKey = "AlbumID")]
        public EntitySet<Image> Images
        {
            get
            {
                return this._Images;
            }
            set
            {
                this._Images.Assign(value);
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

        private void attach_Images(Image entity)
        {
            this.SendPropertyChanging();
            entity.Album = this;
        }

        private void detach_Images(Image entity)
        {
            this.SendPropertyChanging();
            entity.Album = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "dbo.Images")]
    public partial class Image : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _ID;

        private int _AlbumID;

        private string _FileName;

        private byte[] _Data;

        private EntityRef<Album> _Album;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
        partial void OnAlbumIDChanging(int value);
        partial void OnAlbumIDChanged();
        partial void OnFileNameChanging(string value);
        partial void OnFileNameChanged();
        partial void OnDataChanging(byte[] value);
        partial void OnDataChanged();
        #endregion

        public Image()
        {
            this._Album = default(EntityRef<Album>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID
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

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AlbumID", DbType = "Int NOT NULL")]
        public int AlbumID
        {
            get
            {
                return this._AlbumID;
            }
            set
            {
                if ((this._AlbumID != value))
                {
                    if (this._Album.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnAlbumIDChanging(value);
                    this.SendPropertyChanging();
                    this._AlbumID = value;
                    this.SendPropertyChanged("AlbumID");
                    this.OnAlbumIDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FileName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                if ((this._FileName != value))
                {
                    this.OnFileNameChanging(value);
                    this.SendPropertyChanging();
                    this._FileName = value;
                    this.SendPropertyChanged("FileName");
                    this.OnFileNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Data", DbType = "Image NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
        public byte[] Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                if ((this._Data != value))
                {
                    this.OnDataChanging(value);
                    this.SendPropertyChanging();
                    this._Data = value;
                    this.SendPropertyChanged("Data");
                    this.OnDataChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "Album_Image", Storage = "_Album", ThisKey = "AlbumID", OtherKey = "ID", IsForeignKey = true)]
        public Album Album
        {
            get
            {
                return this._Album.Entity;
            }
            set
            {
                Album previousValue = this._Album.Entity;
                if (((previousValue != value)
                            || (this._Album.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._Album.Entity = null;
                        previousValue.Images.Remove(this);
                    }
                    this._Album.Entity = value;
                    if ((value != null))
                    {
                        value.Images.Add(this);
                        this._AlbumID = value.ID;
                    }
                    else
                    {
                        this._AlbumID = default(int);
                    }
                    this.SendPropertyChanged("Album");
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
#pragma warning restore 1591

}
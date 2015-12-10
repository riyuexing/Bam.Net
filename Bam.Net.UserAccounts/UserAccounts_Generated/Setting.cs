/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.UserAccounts.Data
{
	// schema = UserAccounts
	// connection Name = UserAccounts
	[Serializable]
	[Bam.Net.Data.Table("Setting", "UserAccounts")]
	public partial class Setting: Dao
	{
		public Setting():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Setting(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Setting(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public Setting(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator Setting(DataRow data)
		{
			return new Setting(data);
		}

		private void SetChildren()
		{
						
		}

﻿	// property:Id, columnName:Id	
	[Exclude]
	[Bam.Net.Data.KeyColumn(Name="Id", DbDataType="BigInt", MaxLength="19")]
	public long? Id
	{
		get
		{
			return GetLongValue("Id");
		}
		set
		{
			SetValue("Id", value);
		}
	}

﻿	// property:Uuid, columnName:Uuid	
	[Bam.Net.Data.Column(Name="Uuid", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Uuid
	{
		get
		{
			return GetStringValue("Uuid");
		}
		set
		{
			SetValue("Uuid", value);
		}
	}

﻿	// property:Key, columnName:Key	
	[Bam.Net.Data.Column(Name="Key", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string Key
	{
		get
		{
			return GetStringValue("Key");
		}
		set
		{
			SetValue("Key", value);
		}
	}

﻿	// property:ValueType, columnName:ValueType	
	[Bam.Net.Data.Column(Name="ValueType", DbDataType="VarChar", MaxLength="4000", AllowNull=false)]
	public string ValueType
	{
		get
		{
			return GetStringValue("ValueType");
		}
		set
		{
			SetValue("ValueType", value);
		}
	}

﻿	// property:Value, columnName:Value	
	[Bam.Net.Data.Column(Name="Value", DbDataType="VarBinary", MaxLength="8000", AllowNull=false)]
	public byte[] Value
	{
		get
		{
			return GetByteArrayValue("Value");
		}
		set
		{
			SetValue("Value", value);
		}
	}



﻿	// start UserId -> UserId
	[Bam.Net.Data.ForeignKey(
        Table="Setting",
		Name="UserId", 
		DbDataType="BigInt", 
		MaxLength="",
		AllowNull=true, 
		ReferencedKey="Id",
		ReferencedTable="User",
		Suffix="1")]
	public long? UserId
	{
		get
		{
			return GetLongValue("UserId");
		}
		set
		{
			SetValue("UserId", value);
		}
	}

	User _userOfUserId;
	public User UserOfUserId
	{
		get
		{
			if(_userOfUserId == null)
			{
				_userOfUserId = Bam.Net.UserAccounts.Data.User.OneWhere(c => c.KeyColumn == this.UserId, this.Database);
			}
			return _userOfUserId;
		}
	}
	
				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance's.
		/// </summary> 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new SettingColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the Setting table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static SettingCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<Setting>();
			Database db = database ?? Db.For<Setting>();
			var results = new SettingCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static Setting GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static Setting GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static Setting GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static Setting GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static SettingCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static SettingCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<SettingColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a SettingColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SettingCollection Where(Func<SettingColumns, QueryFilter<SettingColumns>> where, OrderBy<SettingColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<Setting>();
			return new SettingCollection(database.GetQuery<SettingColumns, Setting>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static SettingCollection Where(WhereDelegate<SettingColumns> where, Database database = null)
		{		
			database = database ?? Db.For<Setting>();
			var results = new SettingCollection(database, database.GetQuery<SettingColumns, Setting>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static SettingCollection Where(WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<Setting>();
			var results = new SettingCollection(database, database.GetQuery<SettingColumns, Setting>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SettingColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static SettingCollection Where(QiQuery where, Database database = null)
		{
			var results = new SettingCollection(database, Select<SettingColumns>.From<Setting>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static Setting GetOneWhere(QueryFilter where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				result = CreateFromFilter(where, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Setting OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<SettingColumns> whereDelegate = (c) => where;
			var result = Top(1, whereDelegate, database);
			return OneOrThrow(result);
		}

		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Setting GetOneWhere(WhereDelegate<SettingColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				SettingColumns c = new SettingColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single Setting instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Setting OneWhere(WhereDelegate<SettingColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<SettingColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static Setting OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Setting FirstOneWhere(WhereDelegate<SettingColumns> where, Database database = null)
		{
			var results = Top(1, where, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}
		
		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Setting FirstOneWhere(WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy, Database database = null)
		{
			var results = Top(1, where, orderBy, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Shortcut for Top(1, where, orderBy, database)
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static Setting FirstOneWhere(QueryFilter where, OrderBy<SettingColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<SettingColumns> whereDelegate = (c) => where;
			var results = Top(1, whereDelegate, orderBy, database);
			if(results.Count > 0)
			{
				return results[0];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Execute a query and return the specified number
		/// of values. This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static SettingCollection Top(int count, WhereDelegate<SettingColumns> where, Database database = null)
		{
			return Top(count, where, null, database);
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static SettingCollection Top(int count, WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy, Database database = null)
		{
			SettingColumns c = new SettingColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<Setting>();
			QuerySet query = GetQuerySet(db); 
			query.Top<Setting>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<SettingColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SettingCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SettingCollection Top(int count, QueryFilter where, OrderBy<SettingColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<Setting>();
			QuerySet query = GetQuerySet(db);
			query.Top<Setting>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<SettingColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<SettingCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// of values
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="db"></param>
		public static SettingCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<Setting>();
			QuerySet query = GetQuerySet(db);
			query.Top<Setting>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<SettingCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a SettingColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between SettingColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<SettingColumns> where, Database database = null)
		{
			SettingColumns c = new SettingColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<Setting>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<Setting>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static Setting CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<Setting>();			
			var dao = new Setting();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static Setting OneOrThrow(SettingCollection c)
		{
			if(c.Count == 1)
			{
				return c[0];
			}
			else if(c.Count > 1)
			{
				throw new MultipleEntriesFoundException();
			}

			return null;
		}

	}
}																								

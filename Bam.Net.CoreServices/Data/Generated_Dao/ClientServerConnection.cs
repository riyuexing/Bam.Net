/*
	This file was generated and should not be modified directly
*/
// Model is Table
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.CoreServices.Data.Dao
{
	// schema = CoreRegistry
	// connection Name = CoreRegistry
	[Serializable]
	[Bam.Net.Data.Table("ClientServerConnection", "CoreRegistry")]
	public partial class ClientServerConnection: Bam.Net.Data.Dao
	{
		public ClientServerConnection():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ClientServerConnection(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ClientServerConnection(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public ClientServerConnection(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		[Bam.Net.Exclude]
		public static implicit operator ClientServerConnection(DataRow data)
		{
			return new ClientServerConnection(data);
		}

		private void SetChildren()
		{
						
		}

	// property:Id, columnName:Id	
	[Bam.Net.Exclude]
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

	// property:Uuid, columnName:Uuid	
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

	// property:Cuid, columnName:Cuid	
	[Bam.Net.Data.Column(Name="Cuid", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Cuid
	{
		get
		{
			return GetStringValue("Cuid");
		}
		set
		{
			SetValue("Cuid", value);
		}
	}

	// property:ClientId, columnName:ClientId	
	[Bam.Net.Data.Column(Name="ClientId", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public long? ClientId
	{
		get
		{
			return GetLongValue("ClientId");
		}
		set
		{
			SetValue("ClientId", value);
		}
	}

	// property:ServerId, columnName:ServerId	
	[Bam.Net.Data.Column(Name="ServerId", DbDataType="BigInt", MaxLength="19", AllowNull=true)]
	public long? ServerId
	{
		get
		{
			return GetLongValue("ServerId");
		}
		set
		{
			SetValue("ServerId", value);
		}
	}

	// property:Created, columnName:Created	
	[Bam.Net.Data.Column(Name="Created", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Created
	{
		get
		{
			return GetDateTimeValue("Created");
		}
		set
		{
			SetValue("Created", value);
		}
	}

	// property:CreatedBy, columnName:CreatedBy	
	[Bam.Net.Data.Column(Name="CreatedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string CreatedBy
	{
		get
		{
			return GetStringValue("CreatedBy");
		}
		set
		{
			SetValue("CreatedBy", value);
		}
	}

	// property:ModifiedBy, columnName:ModifiedBy	
	[Bam.Net.Data.Column(Name="ModifiedBy", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string ModifiedBy
	{
		get
		{
			return GetStringValue("ModifiedBy");
		}
		set
		{
			SetValue("ModifiedBy", value);
		}
	}

	// property:Modified, columnName:Modified	
	[Bam.Net.Data.Column(Name="Modified", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Modified
	{
		get
		{
			return GetDateTimeValue("Modified");
		}
		set
		{
			SetValue("Modified", value);
		}
	}

	// property:Deleted, columnName:Deleted	
	[Bam.Net.Data.Column(Name="Deleted", DbDataType="DateTime", MaxLength="8", AllowNull=true)]
	public DateTime? Deleted
	{
		get
		{
			return GetDateTimeValue("Deleted");
		}
		set
		{
			SetValue("Deleted", value);
		}
	}



				
		

		/// <summary>
		/// Gets a query filter that should uniquely identify
		/// the current instance.  The default implementation
		/// compares the Id/key field to the current instance's.
		/// </summary>
		[Bam.Net.Exclude] 
		public override IQueryFilter GetUniqueFilter()
		{
			if(UniqueFilterProvider != null)
			{
				return UniqueFilterProvider();
			}
			else
			{
				var colFilter = new ClientServerConnectionColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the ClientServerConnection table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static ClientServerConnectionCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<ClientServerConnection>();
			Database db = database ?? Db.For<ClientServerConnection>();
			var results = new ClientServerConnectionCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Process all records in batches of the specified size
		/// </summary>
		[Bam.Net.Exclude]
		public static async Task BatchAll(int batchSize, Action<IEnumerable<ClientServerConnection>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ClientServerConnectionColumns columns = new ClientServerConnectionColumns();
				var orderBy = Bam.Net.Data.Order.By<ClientServerConnectionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, (c) => c.KeyColumn > 0, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (c) => c.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>			 
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<ClientServerConnection>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		/// <summary>
		/// Process results of a query in batches of the specified size
		/// </summary>	
		[Bam.Net.Exclude]
		public static async Task BatchQuery(int batchSize, WhereDelegate<ClientServerConnectionColumns> where, Action<IEnumerable<ClientServerConnection>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				ClientServerConnectionColumns columns = new ClientServerConnectionColumns();
				var orderBy = Bam.Net.Data.Order.By<ClientServerConnectionColumns>(c => c.KeyColumn, Bam.Net.Data.SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (ClientServerConnectionColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static ClientServerConnection GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static ClientServerConnection GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static ClientServerConnection GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static ClientServerConnection GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		[Bam.Net.Exclude]
		public static ClientServerConnectionCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}

		[Bam.Net.Exclude]		
		public static ClientServerConnectionCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<ClientServerConnectionColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a ClientServerConnectionColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ClientServerConnectionCollection Where(Func<ClientServerConnectionColumns, QueryFilter<ClientServerConnectionColumns>> where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<ClientServerConnection>();
			return new ClientServerConnectionCollection(database.GetQuery<ClientServerConnectionColumns, ClientServerConnection>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="db"></param>
		[Bam.Net.Exclude]
		public static ClientServerConnectionCollection Where(WhereDelegate<ClientServerConnectionColumns> where, Database database = null)
		{		
			database = database ?? Db.For<ClientServerConnection>();
			var results = new ClientServerConnectionCollection(database, database.GetQuery<ClientServerConnectionColumns, ClientServerConnection>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ClientServerConnectionCollection Where(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<ClientServerConnection>();
			var results = new ClientServerConnectionCollection(database, database.GetQuery<ClientServerConnectionColumns, ClientServerConnection>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;ClientServerConnectionColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ClientServerConnectionCollection Where(QiQuery where, Database database = null)
		{
			var results = new ClientServerConnectionCollection(database, Select<ClientServerConnectionColumns>.From<ClientServerConnection>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		[Bam.Net.Exclude]
		public static ClientServerConnection GetOneWhere(QueryFilter where, Database database = null)
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
		[Bam.Net.Exclude]
		public static ClientServerConnection OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<ClientServerConnectionColumns> whereDelegate = (c) => where;
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
		[Bam.Net.Exclude]
		public static ClientServerConnection GetOneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				ClientServerConnectionColumns c = new ClientServerConnectionColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single ClientServerConnection instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ClientServerConnection OneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<ClientServerConnectionColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static ClientServerConnection OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ClientServerConnection FirstOneWhere(WhereDelegate<ClientServerConnectionColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ClientServerConnection FirstOneWhere(WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ClientServerConnection FirstOneWhere(QueryFilter where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<ClientServerConnectionColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="database"></param>
		[Bam.Net.Exclude]
		public static ClientServerConnectionCollection Top(int count, WhereDelegate<ClientServerConnectionColumns> where, Database database = null)
		{
			return Top(count, where, null, database);
		}

		/// <summary>
		/// Execute a query and return the specified number of values.  This method
		/// will issue a sql TOP clause so only the specified number of values
		/// will be returned.
		/// </summary>
		/// <param name="count">The number of values to return.
		/// This value is used in the sql query so no more than this 
		/// number of values will be returned by the database.
		/// </param>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ClientServerConnectionCollection Top(int count, WhereDelegate<ClientServerConnectionColumns> where, OrderBy<ClientServerConnectionColumns> orderBy, Database database = null)
		{
			ClientServerConnectionColumns c = new ClientServerConnectionColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<ClientServerConnection>();
			QuerySet query = GetQuerySet(db); 
			query.Top<ClientServerConnection>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<ClientServerConnectionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ClientServerConnectionCollection>(0);
			results.Database = db;
			return results;
		}

		[Bam.Net.Exclude]
		public static ClientServerConnectionCollection Top(int count, QueryFilter where, Database database)
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
		/// <param name="where">A QueryFilter used to filter the 
		/// results
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static ClientServerConnectionCollection Top(int count, QueryFilter where, OrderBy<ClientServerConnectionColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<ClientServerConnection>();
			QuerySet query = GetQuerySet(db);
			query.Top<ClientServerConnection>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<ClientServerConnectionColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<ClientServerConnectionCollection>(0);
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
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static ClientServerConnectionCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<ClientServerConnection>();
			QuerySet query = GetQuerySet(db);
			query.Top<ClientServerConnection>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<ClientServerConnectionCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of ClientServerConnections
		/// </summary>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<ClientServerConnection>();
            QuerySet query = GetQuerySet(db);
            query.Count<ClientServerConnection>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a ClientServerConnectionColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between ClientServerConnectionColumns and other values
		/// </param>
		/// <param name="database">
		/// Which database to query or null to use the default
		/// </param>
		[Bam.Net.Exclude]
		public static long Count(WhereDelegate<ClientServerConnectionColumns> where, Database database = null)
		{
			ClientServerConnectionColumns c = new ClientServerConnectionColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<ClientServerConnection>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ClientServerConnection>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}
		 
		public static long Count(QiQuery where, Database database = null)
		{
		    Database db = database ?? Db.For<ClientServerConnection>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<ClientServerConnection>();
			query.Where(where);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		} 		

		private static ClientServerConnection CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<ClientServerConnection>();			
			var dao = new ClientServerConnection();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static ClientServerConnection OneOrThrow(ClientServerConnectionCollection c)
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

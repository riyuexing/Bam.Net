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

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
	// schema = TestDaoSchema
	// connection Name = TestDaoSchema
	[Serializable]
	[Bam.Net.Data.Table("House", "TestDaoSchema")]
	public partial class House: Dao
	{
		public House():base()
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public House(DataRow data)
			: base(data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public House(Database db)
			: base(db)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public House(Database db, DataRow data)
			: base(db, data)
		{
			this.SetKeyColumnName();
			this.SetChildren();
		}

		public static implicit operator House(DataRow data)
		{
			return new House(data);
		}

		private void SetChildren()
		{

            this.ChildCollections.Add("HouseParent_HouseId", new HouseParentCollection(Database.GetQuery<HouseParentColumns, HouseParent>((c) => c.HouseId == GetLongValue("Id")), this, "HouseId"));				
            this.ChildCollections.Add("House_HouseParent_Parent",  new XrefDaoCollection<HouseParent, Parent>(this, false));
							
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

	// property:Name, columnName:Name	
	[Bam.Net.Data.Column(Name="Name", DbDataType="VarChar", MaxLength="4000", AllowNull=true)]
	public string Name
	{
		get
		{
			return GetStringValue("Name");
		}
		set
		{
			SetValue("Name", value);
		}
	}



				

	[Exclude]	
	public HouseParentCollection HouseParentsByHouseId
	{
		get
		{
			if (this.IsNew)
			{
				throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
			}

			if(!this.ChildCollections.ContainsKey("HouseParent_HouseId"))
			{
				SetChildren();
			}

			var c = (HouseParentCollection)this.ChildCollections["HouseParent_HouseId"];
			if(!c.Loaded)
			{
				c.Load(Database);
			}
			return c;
		}
	}
			

		// Xref       
        public XrefDaoCollection<HouseParent, Parent> Parents
        {
            get
            {			
				if (this.IsNew)
				{
					throw new InvalidOperationException("The current instance of type({0}) hasn't been saved and will have no child collections, call Save() or Save(Database) first."._Format(this.GetType().Name));
				}

				if(!this.ChildCollections.ContainsKey("House_HouseParent_Parent"))
				{
					SetChildren();
				}

				var xref = (XrefDaoCollection<HouseParent, Parent>)this.ChildCollections["House_HouseParent_Parent"];
				if(!xref.Loaded)
				{
					xref.Load(Database);
				}

				return xref;
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
				return UniqueFilterProvider(this);
			}
			else
			{
				var colFilter = new HouseColumns();
				return (colFilter.KeyColumn == IdValue);
			}			
		}

		/// <summary>
		/// Return every record in the House table.
		/// </summary>
		/// <param name="database">
		/// The database to load from or null
		/// </param>
		public static HouseCollection LoadAll(Database database = null)
		{
			SqlStringBuilder sql = new SqlStringBuilder();
			sql.Select<House>();
			Database db = database ?? Db.For<House>();
			var results = new HouseCollection(sql.GetDataTable(db));
			results.Database = db;
			return results;
		}

		public static async Task BatchAll(int batchSize, Action<IEnumerable<House>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				HouseColumns columns = new HouseColumns();
				var orderBy = Order.By<HouseColumns>(c => c.KeyColumn, SortOrder.Ascending);
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

		public static async Task BatchQuery(int batchSize, QueryFilter filter, Action<IEnumerable<House>> batchProcessor, Database database = null)
		{
			await BatchQuery(batchSize, (c) => filter, batchProcessor, database);			
		}

		public static async Task BatchQuery(int batchSize, WhereDelegate<HouseColumns> where, Action<IEnumerable<House>> batchProcessor, Database database = null)
		{
			await Task.Run(async ()=>
			{
				HouseColumns columns = new HouseColumns();
				var orderBy = Order.By<HouseColumns>(c => c.KeyColumn, SortOrder.Ascending);
				var results = Top(batchSize, where, orderBy, database);
				while(results.Count > 0)
				{
					await Task.Run(()=>
					{ 
						batchProcessor(results);
					});
					long topId = results.Select(d => d.Property<long>(columns.KeyColumn.ToString())).ToArray().Largest();
					results = Top(batchSize, (HouseColumns)where(columns) && columns.KeyColumn > topId, orderBy, database);
				}
			});			
		}

		public static House GetById(int id, Database database = null)
		{
			return GetById((long)id, database);
		}

		public static House GetById(long id, Database database = null)
		{
			return OneWhere(c => c.KeyColumn == id, database);
		}

		public static House GetByUuid(string uuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Uuid") == uuid, database);
		}

		public static House GetByCuid(string cuid, Database database = null)
		{
			return OneWhere(c => Bam.Net.Data.Query.Where("Cuid") == cuid, database);
		}

		public static HouseCollection Query(QueryFilter filter, Database database = null)
		{
			return Where(filter, database);
		}
				
		public static HouseCollection Where(QueryFilter filter, Database database = null)
		{
			WhereDelegate<HouseColumns> whereDelegate = (c) => filter;
			return Where(whereDelegate, database);
		}

		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A Func delegate that recieves a HouseColumns 
		/// and returns a QueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static HouseCollection Where(Func<HouseColumns, QueryFilter<HouseColumns>> where, OrderBy<HouseColumns> orderBy = null, Database database = null)
		{
			database = database ?? Db.For<House>();
			return new HouseCollection(database.GetQuery<HouseColumns, House>(where, orderBy), true);
		}
		
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static HouseCollection Where(WhereDelegate<HouseColumns> where, Database database = null)
		{		
			database = database ?? Db.For<House>();
			var results = new HouseCollection(database, database.GetQuery<HouseColumns, House>(where), true);
			return results;
		}
		   
		/// <summary>
		/// Execute a query and return the results. 
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static HouseCollection Where(WhereDelegate<HouseColumns> where, OrderBy<HouseColumns> orderBy = null, Database database = null)
		{		
			database = database ?? Db.For<House>();
			var results = new HouseCollection(database, database.GetQuery<HouseColumns, House>(where, orderBy), true);
			return results;
		}

		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate&lt;HouseColumns&gt;.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static HouseCollection Where(QiQuery where, Database database = null)
		{
			var results = new HouseCollection(database, Select<HouseColumns>.From<House>().Where(where, database));
			return results;
		}
				
		/// <summary>
		/// Get one entry matching the specified filter.  If none exists 
		/// one will be created; success will depend on the nullability
		/// of the specified columns.
		/// </summary>
		public static House GetOneWhere(QueryFilter where, Database database = null)
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
		public static House OneWhere(QueryFilter where, Database database = null)
		{
			WhereDelegate<HouseColumns> whereDelegate = (c) => where;
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
		public static House GetOneWhere(WhereDelegate<HouseColumns> where, Database database = null)
		{
			var result = OneWhere(where, database);
			if(result == null)
			{
				HouseColumns c = new HouseColumns();
				IQueryFilter filter = where(c); 
				result = CreateFromFilter(filter, database);
			}

			return result;
		}

		/// <summary>
		/// Execute a query that should return only one result.  If more
		/// than one result is returned a MultipleEntriesFoundException will 
		/// be thrown.  This method is most commonly used to retrieve a
		/// single House instance by its Id/Key value
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static House OneWhere(WhereDelegate<HouseColumns> where, Database database = null)
		{
			var result = Top(1, where, database);
			return OneOrThrow(result);
		}
					 
		/// <summary>
		/// This method is intended to respond to client side Qi queries.
		/// Use of this method from .Net should be avoided in favor of 
		/// one of the methods that take a delegate of type
		/// WhereDelegate<HouseColumns>.
		/// </summary>
		/// <param name="where"></param>
		/// <param name="database"></param>
		public static House OneWhere(QiQuery where, Database database = null)
		{
			var results = Top(1, where, database);
			return OneOrThrow(results);
		}

		/// <summary>
		/// Execute a query and return the first result.  This method will issue a sql TOP clause so only the 
		/// specified number of values will be returned.
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static House FirstOneWhere(WhereDelegate<HouseColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static House FirstOneWhere(WhereDelegate<HouseColumns> where, OrderBy<HouseColumns> orderBy, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static House FirstOneWhere(QueryFilter where, OrderBy<HouseColumns> orderBy = null, Database database = null)
		{
			WhereDelegate<HouseColumns> whereDelegate = (c) => where;
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
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="database"></param>
		public static HouseCollection Top(int count, WhereDelegate<HouseColumns> where, Database database = null)
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
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="orderBy">
		/// Specifies what column and direction to order the results by
		/// </param>
		/// <param name="database"></param>
		public static HouseCollection Top(int count, WhereDelegate<HouseColumns> where, OrderBy<HouseColumns> orderBy, Database database = null)
		{
			HouseColumns c = new HouseColumns();
			IQueryFilter filter = where(c);         
			
			Database db = database ?? Db.For<House>();
			QuerySet query = GetQuerySet(db); 
			query.Top<House>(count);
			query.Where(filter);

			if(orderBy != null)
			{
				query.OrderBy<HouseColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HouseCollection>(0);
			results.Database = db;
			return results;
		}

		public static HouseCollection Top(int count, QueryFilter where, Database database)
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
		/// <param name="db"></param>
		public static HouseCollection Top(int count, QueryFilter where, OrderBy<HouseColumns> orderBy = null, Database database = null)
		{
			Database db = database ?? Db.For<House>();
			QuerySet query = GetQuerySet(db);
			query.Top<House>(count);
			query.Where(where);

			if(orderBy != null)
			{
				query.OrderBy<HouseColumns>(orderBy);
			}

			query.Execute(db);
			var results = query.Results.As<HouseCollection>(0);
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
		public static HouseCollection Top(int count, QiQuery where, Database database = null)
		{
			Database db = database ?? Db.For<House>();
			QuerySet query = GetQuerySet(db);
			query.Top<House>(count);
			query.Where(where);
			query.Execute(db);
			var results = query.Results.As<HouseCollection>(0);
			results.Database = db;
			return results;
		}

		/// <summary>
		/// Return the count of Houses
		/// </summary>
		public static long Count(Database database = null)
        {
			Database db = database ?? Db.For<House>();
            QuerySet query = GetQuerySet(db);
            query.Count<House>();
            query.Execute(db);
            return (long)query.Results[0].DataRow[0];
        }

		/// <summary>
		/// Execute a query and return the number of results
		/// </summary>
		/// <param name="where">A WhereDelegate that recieves a HouseColumns 
		/// and returns a IQueryFilter which is the result of any comparisons
		/// between HouseColumns and other values
		/// </param>
		/// <param name="db"></param>
		public static long Count(WhereDelegate<HouseColumns> where, Database database = null)
		{
			HouseColumns c = new HouseColumns();
			IQueryFilter filter = where(c) ;

			Database db = database ?? Db.For<House>();
			QuerySet query = GetQuerySet(db);	 
			query.Count<House>();
			query.Where(filter);	  
			query.Execute(db);
			return query.Results.As<CountResult>(0).Value;
		}

		private static House CreateFromFilter(IQueryFilter filter, Database database = null)
		{
			Database db = database ?? Db.For<House>();			
			var dao = new House();
			filter.Parameters.Each(p=>
			{
				dao.Property(p.ColumnName, p.Value);
			});
			dao.Save(db);
			return dao;
		}
		
		private static House OneOrThrow(HouseCollection c)
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

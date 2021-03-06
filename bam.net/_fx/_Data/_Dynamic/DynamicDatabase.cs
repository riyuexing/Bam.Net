﻿namespace Bam.Net.Data.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Bam.Net;
    using Bam.Net.Data;
    using Bam.Net.Data.Schema;
    using Bam.Net.Data.MsSql;
    using Bam.Net.ExceptionHandling;
    using System.Data.Common;
    using System.Reflection;
    using System.Data;

    /// <summary>
    /// A dynamic crud interface to a database
    /// </summary>
    /// <typeparam name="Db"></typeparam>
    public partial class DynamicDatabase
    {
        /// <summary>
        /// Execute a query using the current sql buffered in CurrentSql
        /// and returning the results as a representation of
        /// the specified tableName
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Retrieve(string tableName)
        {
            if (CurrentSql != null)
            {
                DataTable table = CurrentSql.GetDataTable(Database);
                table = MapDataTable(tableName, table);
                IEnumerable<dynamic> results = table.ToDynamicList();
                CurrentSql = null;
                return results;
            }
            return new List<dynamic>();
        }

        /// <summary>
        /// Execute the specified sql using the specified parameters
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Query(string sql, Dictionary<string, object> parameters)
        {
            try
            {
                Database db = Database;
                IEnumerable<DbParameter> dbParameters = parameters.ToDbParameters(db);
                return Database.GetDataTable(sql, System.Data.CommandType.Text, dbParameters.ToArray()).ToDynamicList();
            }
            catch (Exception ex)
            {
                ExceptionArbiter.Catch(this, ex);
            }

            return new List<dynamic>();
        }
    }
}

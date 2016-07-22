﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using Bam.Net.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Bam.Net.Data.MsSql
{
    public class MsSqlSchemaExtractor : SchemaExtractor
    {
        // TODO: update this to retrieve all meta data using fewer queries along the lines of  
        //SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH FROM {GetSchemaName()}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TestTable'

        Database _daoDatabase;

        public MsSqlSchemaExtractor(string connectionName)
            : base()
        {
            _daoDatabase = Db.For(connectionName);
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            ConnectionString = connectionString;
        }

        public MsSqlSchemaExtractor(MsSqlDatabase database)
            : base()
        {
            _daoDatabase = database;
            ConnectionString = database.ConnectionString;
        }

        public MsSqlSchemaExtractor(MsSqlDatabase database, INameFormatter nameFormatter): this(database)
        {
            this.NameFormatter = nameFormatter;
        }
        public override string ConnectionString
        {
            get
            {
                return base.ConnectionString;
            }
            set
            {
                base.ConnectionString = value;
                SetConnection(value);
            }
        }
        public override DataTypes GetColumnDataType(string tableName, string columnName)
        {
            return TranslateDataType(GetColumnDbDataType(tableName, columnName).ToLowerInvariant());
        }

        public override string GetColumnDbDataType(string tableName, string columnName)
        {
            return GetColumnAttribute(tableName, columnName, "DATA_TYPE").ToString();            
        }

        public override string GetColumnMaxLength(string tableName, string columnName)
        {
            object value = GetColumnAttribute(tableName, columnName, "CHARACTER_MAXIMUM_LENGTH");
            return value == null ? "MAX" : value.ToString();
        }

        private object GetColumnAttribute(string tableName, string columnName, string attributeName)
        {
            string sql = $"SELECT {attributeName} FROM {GetSchemaName()}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName";
            return  _daoDatabase.GetFirstRowFromSql(sql, new { TableName = tableName, ColumnName = columnName }.ToDbParameters(_daoDatabase).ToArray())[0];
        }

        public override string[] GetColumnNames(string tableName)
        {
            string sql = $"SELECT COLUMN_NAME FROM {GetSchemaName()}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @TableName";
            return _daoDatabase.GetValues<string>(sql, new { TableName = tableName }.ToDbParameters(_daoDatabase).ToArray()).ToArray();
        }

        public override bool GetColumnNullable(string tableName, string columnName)
        {
            string sql = "SELECT COLUMNPROPERTY(OBJECT_ID(@TableName, 'U'), @ColumnName, 'AllowsNull')";
            return _daoDatabase.GetValue<int>(sql, new { TableName = tableName, ColumnName = columnName }.ToDbParameters(_daoDatabase).ToArray()) == 1;
        }

        public override ForeignKeyColumn[] GetForeignKeyColumns()
        {
            DataTable foreignKeyData = GetForeignKeyData(_daoDatabase);
            List<ForeignKeyColumn> results = new List<ForeignKeyColumn>();
            foreach (DataRow row in foreignKeyData.Rows)
            {
                ForeignKeyColumn fk = new ForeignKeyColumn();
                fk.TableName = row["ForeignKeyTable"].ToString();
                fk.ReferenceName = row["ForeignKeyName"].ToString();
                fk.Name = row["ForeignKeyColumn"].ToString();
                fk.ReferencedKey = row["PrimaryKeyColumn"].ToString();

                fk.ReferencedTable = row["PrimaryKeyTable"].ToString();
                results.Add(fk);
            }

            return results.ToArray();
        }

        public override string GetKeyColumnName(string tableName)
        {
            string sql = $@"SELECT COLUMN_NAME
FROM {GetSchemaName()}.INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + CONSTRAINT_NAME), 'IsPrimaryKey') = 1
AND TABLE_NAME = @TableName";            
            return _daoDatabase.GetValue<string>(sql, new { TableName = tableName }.ToDbParameters(_daoDatabase).ToArray());            
        }

        public override string GetSchemaName()
        {
            return _daoDatabase.ConnectionName;
        }

        public override string[] GetTableNames()
        {
            string sql = $"SELECT TABLE_NAME FROM {GetSchemaName()}.INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            DataTable results = _daoDatabase.GetDataTableFromSql(sql);
            List<string> tableNames = new List<string>();
            foreach(DataRow row in results.Rows)
            {
                tableNames.Add(row["TABLE_NAME"].ToString());
            }
            return tableNames.ToArray();
        }

        protected internal DataTypes TranslateDataType(string sqlDataType)
        {
            switch (sqlDataType)
            {
                case "bigint":
                    return DataTypes.Long;
                case "binary":
                    return DataTypes.ByteArray;
                case "bit":
                    return DataTypes.Boolean;
                case "char":
                    return DataTypes.String;
                case "datetime":
                    return DataTypes.DateTime;
                case "decimal":
                    return DataTypes.Decimal;
                case "float":
                    return DataTypes.String;
                case "image":
                    return DataTypes.ByteArray;
                case "int":
                    return DataTypes.Int;
                case "money":
                    return DataTypes.Decimal;
                case "nchar":
                    return DataTypes.String;
                case "ntext":
                    return DataTypes.String;
                case "nvarchar":
                    return DataTypes.String;
                case "nvarcharmax":
                    return DataTypes.String;
                case "none":
                    return DataTypes.String;
                case "numeric":
                    return DataTypes.Long;
                case "real":
                    return DataTypes.String;
                case "smalldatetime":
                    return DataTypes.DateTime;
                case "smallint":
                    return DataTypes.Int;
                case "smallmoney":
                    return DataTypes.Decimal;
                case "sysname":
                    return DataTypes.String;
                case "text":
                    return DataTypes.String;
                case "timestamp":
                    return DataTypes.DateTime;
                case "tinyint":
                    return DataTypes.Int;
                case "uniqueidentifier":
                    return DataTypes.String;
                case "userdefineddatatype":
                    return DataTypes.String;
                case "userdefinedtype":
                    return DataTypes.String;
                case "varbinary":
                    return DataTypes.ByteArray;
                case "varbinarymax":
                    return DataTypes.ByteArray;
                case "varchar":
                    return DataTypes.String;
                case "varcharmax":
                    return DataTypes.String;
                case "variant":
                    return DataTypes.String;
                case "xml":
                    return DataTypes.String;
                default:
                    return DataTypes.String;
            }
        }


        internal static DataTable GetForeignKeyData(Database db)
        {
            #region sql
            string sql = @"SELECT FK.constraint_name as ForeignKeyName, 
                FK.table_name as ForeignKeyTable, 
                FKU.column_name as ForeignKeyColumn,
                UK.constraint_name as PrimaryKeyName, 
                UK.table_name as PrimaryKeyTable, 
                UKU.column_name as PrimaryKeyColumn
                FROM Information_Schema.Table_Constraints AS FK
                INNER JOIN
                Information_Schema.Key_Column_Usage AS FKU
                ON FK.constraint_type = 'FOREIGN KEY' AND
                FKU.constraint_name = FK.constraint_name
                INNER JOIN
                Information_Schema.Referential_Constraints AS RC
                ON RC.constraint_name = FK.constraint_name
                INNER JOIN
                Information_Schema.Table_Constraints AS UK
                ON UK.constraint_name = RC.unique_constraint_name
                INNER JOIN
                Information_Schema.Key_Column_Usage AS UKU
                ON UKU.constraint_name = UK.constraint_name AND
                UKU.ordinal_position =FKU.ordinal_position";
            #endregion
            DataTable foreignKeyData = db.GetDataTableFromSql(sql, CommandType.Text);
            return foreignKeyData;
        }
        private void SetConnection(string connectionString)
        {
            SqlConnection _connection = new SqlConnection(ConnectionString);

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            string databaseName = connectionStringBuilder["Initial Catalog"] as string;
            if (string.IsNullOrWhiteSpace(databaseName))
            {
                databaseName = connectionStringBuilder["Database"] as string;
            }

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                throw new InvalidOperationException(
                    string.Format("Unable to determine database name from the specified connection string: {0}",
                    connectionString));
            }

            _daoDatabase.ConnectionName = databaseName;
        }
    }
}

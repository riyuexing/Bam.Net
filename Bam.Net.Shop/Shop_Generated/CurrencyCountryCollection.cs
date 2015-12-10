/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Shop
{
    public class CurrencyCountryCollection: DaoCollection<CurrencyCountryColumns, CurrencyCountry>
    { 
		public CurrencyCountryCollection(){}
		public CurrencyCountryCollection(Database db, DataTable table, Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public CurrencyCountryCollection(DataTable table, Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public CurrencyCountryCollection(Query<CurrencyCountryColumns, CurrencyCountry> q, Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public CurrencyCountryCollection(Database db, Query<CurrencyCountryColumns, CurrencyCountry> q, bool load) : base(db, q, load) { }
		public CurrencyCountryCollection(Query<CurrencyCountryColumns, CurrencyCountry> q, bool load) : base(q, load) { }
    }
}
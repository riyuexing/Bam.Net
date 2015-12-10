/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Analytics
{
    public class UserIdentifierQuery: Query<UserIdentifierColumns, UserIdentifier>
    { 
		public UserIdentifierQuery(){}
		public UserIdentifierQuery(WhereDelegate<UserIdentifierColumns> where, OrderBy<UserIdentifierColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public UserIdentifierQuery(Func<UserIdentifierColumns, QueryFilter<UserIdentifierColumns>> where, OrderBy<UserIdentifierColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public UserIdentifierQuery(Delegate where, Database db = null) : base(where, db) { }

		public UserIdentifierCollection Execute()
		{
			return new UserIdentifierCollection(this, true);
		}
    }
}
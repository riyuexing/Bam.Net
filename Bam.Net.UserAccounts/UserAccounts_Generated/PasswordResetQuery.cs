/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public class PasswordResetQuery: Query<PasswordResetColumns, PasswordReset>
    { 
		public PasswordResetQuery(){}
		public PasswordResetQuery(WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public PasswordResetQuery(Func<PasswordResetColumns, QueryFilter<PasswordResetColumns>> where, OrderBy<PasswordResetColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public PasswordResetQuery(Delegate where, Database db = null) : base(where, db) { }

		public PasswordResetCollection Execute()
		{
			return new PasswordResetCollection(this, true);
		}
    }
}
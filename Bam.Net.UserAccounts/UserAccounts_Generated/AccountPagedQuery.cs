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
    public class AccountPagedQuery: PagedQuery<AccountColumns, Account>
    { 
		public AccountPagedQuery(AccountColumns orderByColumn, AccountQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
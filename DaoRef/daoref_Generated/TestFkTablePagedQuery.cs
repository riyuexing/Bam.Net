/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class TestFkTablePagedQuery: PagedQuery<TestFkTableColumns, TestFkTable>
    { 
		public TestFkTablePagedQuery(TestFkTableColumns orderByColumn, TestFkTableQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
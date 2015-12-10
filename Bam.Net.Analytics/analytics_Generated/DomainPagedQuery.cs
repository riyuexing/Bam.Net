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
    public class DomainPagedQuery: PagedQuery<DomainColumns, Domain>
    { 
		public DomainPagedQuery(DomainColumns orderByColumn, DomainQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
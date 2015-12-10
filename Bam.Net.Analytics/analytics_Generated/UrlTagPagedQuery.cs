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
    public class UrlTagPagedQuery: PagedQuery<UrlTagColumns, UrlTag>
    { 
		public UrlTagPagedQuery(UrlTagColumns orderByColumn, UrlTagQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
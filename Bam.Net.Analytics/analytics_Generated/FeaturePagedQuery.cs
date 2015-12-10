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
    public class FeaturePagedQuery: PagedQuery<FeatureColumns, Feature>
    { 
		public FeaturePagedQuery(FeatureColumns orderByColumn, FeatureQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
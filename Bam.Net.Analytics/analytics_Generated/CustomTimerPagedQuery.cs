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
    public class CustomTimerPagedQuery: PagedQuery<CustomTimerColumns, CustomTimer>
    { 
		public CustomTimerPagedQuery(CustomTimerColumns orderByColumn, CustomTimerQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
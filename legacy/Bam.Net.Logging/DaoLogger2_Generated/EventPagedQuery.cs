/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class EventPagedQuery: PagedQuery<EventColumns, Event>
    { 
		public EventPagedQuery(EventColumns orderByColumn, EventQuery query, Database db = null) : base(orderByColumn, query, db) { }
    }
}
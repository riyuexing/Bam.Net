/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Distributed
{
    public class Keyspace
    {
        public Keyspace(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }

        public int Start { get; set; }
        public int End { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Schema
{
    public class RazorResourceDaoGenerator: DaoGenerator
    {
        public RazorResourceDaoGenerator(IDaoTargetStreamResolver targetStreamResolver) : base(new RazorParserDaoCodeWriter { DaoTargetStreamResolver = targetStreamResolver }, targetStreamResolver) { }
    }
}

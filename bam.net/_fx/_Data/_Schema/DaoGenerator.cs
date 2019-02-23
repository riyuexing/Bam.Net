using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public partial class DaoGenerator
    {
        public DaoGenerator(IDaoCodeWriter codeWriter = null, IDaoTargetStreamResolver targetStreamResolver = null)
        {
            DisposeOnComplete = true;
            SubscribeToEvents();

            Namespace = "DaoGenerated";
            TargetStreamResolver = targetStreamResolver ?? new DaoTargetStreamResolver();
            DaoCodeWriter = codeWriter ?? new RazorParserDaoCodeWriter
            {
                DaoTargetStreamResolver = TargetStreamResolver
            };
        }
    }
}

using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic.Tests.Data
{
    public class TestData: RepoData
    {
        public string Value { get; set; }
        public long LongValue { get; set; }
        public ulong ULongValue { get; set; }
    }
}

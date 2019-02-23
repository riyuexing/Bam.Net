using Bam.Net.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public partial class Table
    {

        public string RenderContextMethod()
        {
            RazorParser<TableTemplate> razorParser = new RazorParser<TableTemplate>();
            return razorParser.ExecuteResource("ContextMethods.tmpl", new { Model = this });
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public partial class KeyColumn
    {
        protected internal override string RenderClassProperty()
        {
            return Render<KeyColumn>("KeyProperty.tmpl");
        }
    }
}

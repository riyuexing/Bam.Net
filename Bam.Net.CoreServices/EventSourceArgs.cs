﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class EventSourceArgs: EventArgs, IJsonable
    {
        public EventMessage EventMessage { get; set; }

        public string ToJson()
        {
            return EventMessage.ToJson();
        }
    }
}

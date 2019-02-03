using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Presentation.Components
{
    public class Component
    {
        public Component(ComponentOptions options)
        {
            Options = options;
        }
        public ComponentOptions Options { get; set; }
        public string Name { get; set; }
        public List<Component> Children { get; set; }
    }
}

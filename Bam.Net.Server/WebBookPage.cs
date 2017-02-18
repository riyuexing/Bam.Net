/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server
{
    public class WebBookPage
    {
        public WebBookPage()
        {
            this.Layout = "default";
        }
        public string Name { get; set; }
        public string Layout { get; set; }

        public override bool Equals(object obj)
        {
            WebBookPage page = obj as WebBookPage;
            if (page != null)
            {
                return page.Name.Equals(this.Name) && page.Layout.Equals(this.Layout);
            }
            else
            {
                return base.Equals(obj);
            }
        }
        public override int GetHashCode()
        {
            return this.GetHashCode(Name, Layout);
        }
    }

}

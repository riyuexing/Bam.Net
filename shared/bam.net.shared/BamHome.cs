using Bam.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net
{
    public class BamHome
    {
        public static string[] PathSegments
        {
            get
            {
                if(OSInfo.Current == OSNames.Windows)
                {
                    return new string[] { "C:", "bam" };
                }
                else
                {
                    return new string[] { "~/", ".bam" };
                }
            }
        }

        public static string[] ToolkitSegments
        {
            get
            {
                List<string> segments = new List<string>(PathSegments)
                {
                    "toolkit"
                };
                return segments.ToArray();
            }
        }
    }
}

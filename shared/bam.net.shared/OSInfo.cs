using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Bam.Net
{
    public class OSInfo
    {
        static OSNames _current;
        public static OSNames Current
        {
            get
            {
                if(_current == OSNames.Invalid)
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        _current = OSNames.Windows;
                    }else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        _current = OSNames.Linux;
                    }
                    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    {
                        _current = OSNames.OSX;
                    }
                }
                return _current;
            }
        }
    }
}

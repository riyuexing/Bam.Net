using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Presentation
{
    public interface IApplicationTemplateManager: ITemplateManager
    {
        string ApplicationName { get; }
    }
}

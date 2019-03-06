using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using Bam.Net.Server;
using Newtonsoft.Json;
using Bam.Net.Presentation;
using Bam.Net.Presentation.Html;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using Bam.Net.Services;

namespace Bam.Net.Server
{
    public partial class LayoutConf
    {
        protected internal void SetIncludes(AppConf conf, LayoutModel layoutModel)
        {
            Args.ThrowIfNull(conf, "AppConf");
            Args.ThrowIfNull(conf.BamConf, "BamConf");
            Args.ThrowIfNull(conf.BamConf.ContentRoot, "ContentRoot");
            ApplicationServiceRegistry reg = ApplicationServiceRegistry.ForApplication(conf.Name);
            Includes commonIncludes = reg.Get<IIncludesResolver>().ResolveCommonIncludes(conf.BamConf.ContentRoot);
            Includes appIncludes = reg.Get<IIncludesResolver>().ResolveApplicationIncludes(conf.Name, conf.BamConf.ContentRoot);
            Includes combined = commonIncludes.Combine(appIncludes);
            
            // TODO: test this
            // finish this
            foreach(Tag scriptTag in Tag.ForEach(combined.Scripts, path => Tag.Of("script", new { src = path })))
            {
                layoutModel.ScriptTags += scriptTag.Render();
            }
            // <link rel="stylesheet" href="/css/fullcalendar/fullcalendar.css" />
            foreach (Tag cssTag in Tag.ForEach(combined.Css, path => Tag.Of("link", new { rel = "stylesheet", href = path })))
            {
                layoutModel.StyleSheetLinkTags += cssTag.Render();
            }
        }
    }
}

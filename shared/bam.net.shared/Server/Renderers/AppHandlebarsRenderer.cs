using Bam.Net.Presentation;
using Bam.Net.Presentation.Handlebars;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bam.Net.Server.Renderers
{
    public class AppHandlebarsRenderer : CommonHandlebarsRenderer, IApplicationTemplateManager
    {
        public AppHandlebarsRenderer() : base(null)
        {
        }

        public AppHandlebarsRenderer(AppContentResponder appContent) : base(appContent.ContentResponder)
        {
            AppContentResponder = appContent;
        }

        public AppContentResponder AppContentResponder
        {
            get;
            set;
        }

        public string ApplicationName
        {
            get
            {
                return AppContentResponder.ApplicationName;
            }
        }

        protected override void SetHandlebarsDirectories()
        {
            base.SetHandlebarsDirectories();
            foreach(string templateDirectoryName in AppContentResponder.TemplateDirectoryNames)
            {
                string directoryPath = Path.Combine(AppContentResponder.AppRoot.Root, templateDirectoryName);
                HandlebarsDirectories.Add(new HandlebarsDirectory(directoryPath));
            }
        }
    }
}

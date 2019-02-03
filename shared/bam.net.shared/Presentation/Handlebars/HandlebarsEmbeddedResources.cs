using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bam.Net.Presentation.Handlebars
{
    public class HandlebarsEmbeddedResources
    {
        public HandlebarsEmbeddedResources(Assembly assembly)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; set; }

        Dictionary<string, Func<object, string>> _templates;
        public Dictionary<string, Func<object, string>> Templates
        {
            get
            {
                return _templates;
            }
        }

        readonly object _reloadLock = new object();
        bool _loaded = false;
        public string Render(string templateName, object data)
        {
            if (!_loaded)
            {
                Reload();
            }
            if (!Templates.ContainsKey(templateName))
            {
                Args.Throw<InvalidOperationException>("Specified template not found: {0}", templateName);
            }
            return Templates[templateName](data);
        }

        public void Reload()
        {
            lock (_reloadLock)
            {
                // register each before compiling individually so each is available as a partial
                ForEachEmbeddedTemplate(resourceName =>
                {
                    using (TextReader sr = new StreamReader(Assembly.GetManifestResourceStream(resourceName)))
                    {
                        string name = Path.GetFileNameWithoutExtension(resourceName);
                        HandlebarsDotNet.Handlebars.RegisterTemplate(name, sr.ReadToEnd());
                    }
                });

                ForEachEmbeddedTemplate(resourceName =>
                {
                    using (TextReader sr = new StreamReader(Assembly.GetManifestResourceStream(resourceName)))
                    {
                        string name = Path.GetFileNameWithoutExtension(resourceName);
                        _templates.AddMissing(name, HandlebarsDotNet.Handlebars.Compile(sr.ReadToEnd()));
                    }
                });

                _loaded = true;
            }
        }

        private void ForEachEmbeddedTemplate(Action<string> action)
        {
            string[] resourceNames = Assembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                if (resourceName.EndsWith(".hbs", StringComparison.InvariantCultureIgnoreCase))
                {
                    action(resourceName);
                }
            }
        }
    }
}

/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Javascript;
using Bam.Net.Presentation.Html;
using System.Web.Mvc;

namespace Bam.Net.Server
{
    public partial class Includes
    {        
        
        /// <summary>
        /// Renders the Scripts as a series of html script tags
        /// with the src attributes set to the value of each 
        /// Script string and the type attribute set to 
        /// "text/javascript"
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString GetScriptTags()
        {
            StringBuilder result = new StringBuilder();
            Scripts.Each(script =>
            {
                Tag scr = new Tag("script").Attr("src", script).Attr("type", "text/javascript");
                result.Append(scr.ToHtmlString(TagRenderMode.Normal));
            });

            return MvcHtmlString.Create(result.ToString());
        }

        /// <summary>
        /// Renders the Css as a series of link tags
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString GetStyleSheetLinkTags()
        {
            StringBuilder result = new StringBuilder();
            Css.Each(css =>
            {
                Tag link = new Tag("link").Attr("rel", "stylesheet").Attr("href", css);
                result.Append(link.ToHtmlString(TagRenderMode.SelfClosing));
            });

            return MvcHtmlString.Create(result.ToString());
        }

    }
}

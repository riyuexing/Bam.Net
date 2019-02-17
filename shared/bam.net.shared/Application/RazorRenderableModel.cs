using Bam.Net.Razor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Application
{
    /// <summary>
    /// A base class for models that may be templated, where the extender of this class
    /// RECIEVES the model that is tempalted.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RazorRenderableModel<T> : RazorRenderableModel
    {
        public RazorRenderableModel(T model)
        {
            TemplateRenderer = new RazorTemplateRenderer<T>()
            {
                Model = model
            };
        }
    }

    /// <summary>
    /// A base class for models that may be templated, where the extender of this class
    /// IS the model that is templated.
    /// </summary>
    public abstract class RazorRenderableModel : RenderableModel
    {
        public RazorRenderableModel()
        {
            TemplateRenderer = new RazorTemplateRenderer<object>()
            {
                Model = this
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public partial class ForeignKeyColumn
    {

        protected internal string RenderClassProperty(int i = -1, string ns = "")
        {
            if (string.IsNullOrEmpty(ReferencedClass.Trim()))
            {
                throw new InvalidOperationException("ReferencedClass cannot be null");
            }
            if (i > 0)
            {
                ReferenceNameSuffix = i.ToString();
            }

            return Render<ForeignKeyColumn>("ForeignKeyProperty.tmpl", ns);
        }

        protected internal string RenderListProperty()
        {
            return Render<ForeignKeyColumn>("DaoCollectionProperty.tmpl");
        }

        protected internal string RenderAddToChildDaoCollection()
        {
            return Render<ForeignKeyColumn>("ChildDaoCollectionAdd.tmpl");
        }
    }
}

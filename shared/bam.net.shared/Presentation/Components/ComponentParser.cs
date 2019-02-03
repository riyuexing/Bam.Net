using Markdig.Helpers;
using Markdig.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.NMet.Presentation.Components
{
    public class ComponentParser : InlineParser
    {
        public ComponentParser()
        {
            OpeningCharacters = new char[] { '`' };
        }

        public override bool Match(InlineProcessor processor, ref StringSlice slice)
        {
            throw new NotImplementedException();
        }
    }
}

using Bam.Net.Presentation.Html;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;

namespace Bam.Net.Presentation.Tests
{
    [Serializable]
    public class HtmlTagTests: CommandLineTestInterface
    {
        [UnitTest]
        public void TestDivOutput()
        {
            Tag tag = new Tag("div", new { id = "banana" });
            OutLine(tag.ToString());
        }
    }
}
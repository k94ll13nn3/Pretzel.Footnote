using System;
using DotLiquid;
using NUnit.Framework;

namespace Pretzel.Footnote.Tests
{
    [TestFixture]
    internal class FootnoteTests
    {
        [Test]
        public void TestFootnote()
        {
            Template.RegisterTag<FootnoteTag>("footnote");
            Template.RegisterTag<FootnoteRenderTag>("footnote_render");

            var templateOk1 = Template.Parse("{% footnote test %}");
            var templateOk1b = Template.Parse("{% footnote test %}");
            var templateOk2 = Template.Parse("{% footnote \"lorem ipsum\" %}");
            var templateOk3 = Template.Parse("{% footnote [Link](www.google.com) %}");

            var templateRendered = Template.Parse("{% footnote_render %}");

            Assert.AreEqual("test", FootnoteCore.Footnotes[1]);
            Assert.AreEqual("\"lorem ipsum\"", FootnoteCore.Footnotes[2]);
            Assert.AreEqual("[Link](www.google.com)", FootnoteCore.Footnotes[3]);
            Assert.AreEqual("<sup><a href=\"#fn:1\">1</a></sup>", templateOk1.Render());
            Assert.AreEqual("<sup><a href=\"#fn:1\">1</a></sup>", templateOk1b.Render());
            Assert.AreEqual("<sup><a href=\"#fn:2\">2</a></sup>", templateOk2.Render());
            Assert.AreEqual("<sup><a href=\"#fn:3\">3</a></sup>", templateOk3.Render());

            Assert.AreEqual("<div class=\"footnotes\"><hr /><ol><li id=\"fn:1\">\n\ntest\n\n</li><li id=\"fn:2\">\n\n\"lorem ipsum\"\n\n</li><li id=\"fn:3\">\n\n[Link](www.google.com)\n\n</li></ol></div>", templateRendered.Render());
        }

        [Test]
        public void TestMultipleRender()
        {
            Template.RegisterTag<FootnoteTag>("footnote");
            Template.RegisterTag<FootnoteRenderTag>("footnote_render");

            Template.Parse("{% footnote test %}");
            Template.Parse("{% footnote \"lorem ipsum\" %}");
            var templateRendered1 = Template.Parse("{% footnote_render %}");
            Assert.AreEqual("<div class=\"footnotes\"><hr /><ol><li id=\"fn:1\">\n\ntest\n\n</li><li id=\"fn:2\">\n\n\"lorem ipsum\"\n\n</li></ol></div>", templateRendered1.Render());

            Template.Parse("{% footnote [Link](www.google.com) %}");
            var templateRendered2 = Template.Parse("{% footnote_render %}");
            Assert.AreEqual("<div class=\"footnotes\"><hr /><ol><li id=\"fn:1\">\n\n[Link](www.google.com)\n\n</li></ol></div>", templateRendered2.Render());

            var templateRendered3 = Template.Parse("{% footnote_render %}");
            Assert.AreEqual(string.Empty, templateRendered3.Render());
        }

        [Test]
        public void TestEmptyFootnote()
        {
            Template.RegisterTag<FootnoteTag>("footnote");
            Template.RegisterTag<FootnoteRenderTag>("footnote_render");

            var templateRendered = Template.Parse("{% footnote_render %}");
            Assert.AreEqual(string.Empty, templateRendered.Render());
        }

        [Test]
        public void TestFootnoteId()
        {
            Template.RegisterTag<FootnoteTag>("footnote");
            Template.RegisterTag<FootnoteIdTag>("footnote_id");

            Template.Parse("{% footnote test %}");
            var templateOk1 = Template.Parse("{% footnote_id 1 %}");

            Assert.AreEqual("<sup><a href=\"#fn:1\">1</a></sup>", templateOk1.Render());

            Assert.Throws<ArgumentException>(() => Template.Parse("{% footnote_id 2 %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% footnote_id test %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% footnote_id %}"));
        }
    }
}
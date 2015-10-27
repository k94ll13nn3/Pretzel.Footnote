using DotLiquid;
using NUnit.Framework;
using System;

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

            Template templateOk1 = Template.Parse("{% footnote test %}");
            Template templateOk1b = Template.Parse("{% footnote test %}");
            Template templateOk2 = Template.Parse("{% footnote \"lorem ipsum\" %}");
            Template templateOk3 = Template.Parse("{% footnote [Link](www.google.com) %}");

            Template templateRendered = Template.Parse("{% footnote_render %}");

            Assert.AreEqual("test", FootnoteCore.Footnotes[1]);
            Assert.AreEqual("\"lorem ipsum\"", FootnoteCore.Footnotes[2]);
            Assert.AreEqual("[Link](www.google.com)", FootnoteCore.Footnotes[3]);
            Assert.AreEqual("<sup><a href=\"#fn:1\">1</a></sup>", templateOk1.Render());
            Assert.AreEqual("<sup><a href=\"#fn:1\">1</a></sup>", templateOk1b.Render());
            Assert.AreEqual("<sup><a href=\"#fn:2\">2</a></sup>", templateOk2.Render());
            Assert.AreEqual("<sup><a href=\"#fn:3\">3</a></sup>", templateOk3.Render());

            Assert.AreEqual("<div class=\"footnotes\"><hr /><ol><li id=\"fn:1\"><p>test</p></li><li id=\"fn:2\"><p>\"lorem ipsum\"</p></li><li id=\"fn:3\"><p>[Link](www.google.com)</p></li></ol></div>", templateRendered.Render());
        }

        [Test]
        public void TestMultipleRender()
        {
            Template.RegisterTag<FootnoteTag>("footnote");
            Template.RegisterTag<FootnoteRenderTag>("footnote_render");

            Template.Parse("{% footnote test %}");
            Template.Parse("{% footnote \"lorem ipsum\" %}");
            Template templateRendered1 = Template.Parse("{% footnote_render %}");
            Assert.AreEqual("<div class=\"footnotes\"><hr /><ol><li id=\"fn:1\"><p>test</p></li><li id=\"fn:2\"><p>\"lorem ipsum\"</p></li></ol></div>", templateRendered1.Render());

            Template.Parse("{% footnote [Link](www.google.com) %}");
            Template templateRendered2 = Template.Parse("{% footnote_render %}");
            Assert.AreEqual("<div class=\"footnotes\"><hr /><ol><li id=\"fn:1\"><p>[Link](www.google.com)</p></li></ol></div>", templateRendered2.Render());

            Template templateRendered3 = Template.Parse("{% footnote_render %}");
            Assert.AreEqual(string.Empty, templateRendered3.Render());
        }

        [Test]
        public void TestEmptyFootnote()
        {
            Template.RegisterTag<FootnoteTag>("footnote");
            Template.RegisterTag<FootnoteRenderTag>("footnote_render");

            Template templateRendered = Template.Parse("{% footnote_render %}");
            Assert.AreEqual(string.Empty, templateRendered.Render());
        }

        [Test]
        public void TestFootnoteId()
        {
            Template.RegisterTag<FootnoteTag>("footnote");
            Template.RegisterTag<FootnoteIdTag>("footnote_id");

            Template.Parse("{% footnote test %}");
            Template templateOk1 = Template.Parse("{% footnote_id 1 %}");

            Assert.AreEqual("<sup><a href=\"#fn:1\">1</a></sup>", templateOk1.Render());

            Assert.Throws<ArgumentException>(() => Template.Parse("{% footnote_id 2 %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% footnote_id test %}"));
            Assert.Throws<ArgumentException>(() => Template.Parse("{% footnote_id %}"));
        }
    }
}
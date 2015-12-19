// Pretzel.Footnote plugin
using System.Collections.Generic;
using System.ComponentModel.Composition;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace Pretzel.Footnote
{
    [Export(typeof(ITag))]
    public class FootnoteTag : Tag, ITag
    {
        private int id;

        public new string Name => "Footnote";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);

            this.id = FootnoteCore.AddFootnote(markup.Trim());
        }

        public override void Render(Context context, System.IO.TextWriter result)
        {
            result.Write($"<sup><a href=\"#fn:{this.id}\">{this.id}</a></sup>");
        }
    }
}
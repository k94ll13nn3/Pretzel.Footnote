using System;
using System.Collections.Generic;
using DotLiquid;
using Pretzel.Logic.Extensibility;
using System.ComponentModel.Composition;

namespace Pretzel.Footnote
{
    [Export(typeof(ITag))]
    public class FootnoteIdTag : Tag, ITag
    {
        private int id;

        public new string Name => "FootnoteId";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);

            if (int.TryParse(markup.Trim(), out this.id))
            {
                if (!FootnoteCore.Footnotes.ContainsKey(this.id))
                {
                    throw new ArgumentException("There is no footnote associated to the id.");
                }
            }
            else
            {
                throw new ArgumentException("Expected syntax: {% footnote_id id %} where id is an int.");
            }
        }

        public override void Render(Context context, System.IO.TextWriter result)
        {
            result.Write($"<sup><a href=\"#fn:{this.id}\">{this.id}</a></sup>");
        }
    }
}
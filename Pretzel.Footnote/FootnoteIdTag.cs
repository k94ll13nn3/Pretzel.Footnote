using System;
using System.Collections.Generic;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace Pretzel.Footnote
{
    /// <summary>
    /// The footnote id tag.
    /// </summary>
    public class FootnoteIdTag : Tag, ITag
    {
        /// <summary>
        /// The id of the footnote.
        /// </summary>
        private int id = 0;

        /// <summary>
        /// Overrides the tag name.
        /// </summary>
        public new string Name => "FootnoteId";

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override void Render(Context context, System.IO.TextWriter result)
        {
            result.Write($"<sup><a href=\"#fn:{this.id}\">{this.id}</a></sup>");
        }
    }
}
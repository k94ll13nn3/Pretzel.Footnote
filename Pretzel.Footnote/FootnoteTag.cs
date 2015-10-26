using System.Collections.Generic;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace Pretzel.Footnote
{
    /// <summary>
    /// The footnote tag.
    /// </summary>
    public class FootnoteTag : Tag, ITag
    {
        /// <summary>
        /// The id of the footnote.
        /// </summary>
        private int id = 0;

        /// <summary>
        /// Overrides the tag name.
        /// </summary>
        public new string Name => "Footnote";

        /// <inheritdoc/>
        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);

            this.id = FootnoteCore.AddFootnote(markup.Trim());
        }

        /// <inheritdoc/>
        public override void Render(Context context, System.IO.TextWriter result)
        {
            result.Write($"<sup><a href=\"#fn:{this.id}\">{this.id}</a></sup>");
        }
    }
}
using System.Collections.Generic;
using DotLiquid;
using Pretzel.Logic.Extensibility;

namespace Pretzel.Footnote
{
    /// <summary>
    /// The footnote tag.
    /// </summary>
    public class FootnoteRenderTag : Tag, ITag
    {
        /// <summary>
        /// Overrides the tag name.
        /// </summary>
        public new string Name => "FootnoteRender";

        /// <inheritdoc/>
        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);
        }

        /// <inheritdoc/>
        public override void Render(Context context, System.IO.TextWriter result)
        {
            if (FootnoteCore.Footnotes.Count != 0)
            {
                result.Write("<div class=\"footnotes\"><hr /><ol>");

                foreach (var footnote in FootnoteCore.Footnotes)
                {
                    result.Write($"<li id=\"fn:{footnote.Key}\"><p>{footnote.Value}</p></li>");
                }

                result.Write("</ol></div>");
                FootnoteCore.Clear();
            }
        }
    }
}
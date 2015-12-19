// Pretzel.Footnote plugin
using System.Collections.Generic;
using DotLiquid;
using Pretzel.Logic.Extensibility;
using System.ComponentModel.Composition;

namespace Pretzel.Footnote
{
    [Export(typeof(ITag))]
    public class FootnoteRenderTag : Tag, ITag
    {
        public new string Name => "FootnoteRender";

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            base.Initialize(tagName, markup, tokens);
        }

        public override void Render(Context context, System.IO.TextWriter result)
        {
            if (FootnoteCore.Footnotes.Count != 0)
            {
                result.Write("<div class=\"footnotes\"><hr /><ol>");

                foreach (var footnote in FootnoteCore.Footnotes)
                {
                    result.Write($"<li id=\"fn:{footnote.Key}\">\n\n{footnote.Value}\n\n</li>");
                }

                result.Write("</ol></div>");
                FootnoteCore.Clear();
            }
        }
    }
}
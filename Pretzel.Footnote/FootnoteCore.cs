using System.Collections.Generic;

namespace Pretzel.Footnote
{
    /// <summary>
    /// This class will store all the known footnotes during the generation process.
    /// </summary>
    internal static class FootnoteCore
    {
        private static readonly Dictionary<int, string> FootnotesValue = new Dictionary<int, string>();

        public static Dictionary<int, string> Footnotes => FootnotesValue;

        public static int AddFootnote(string fn)
        {
            if (!FootnotesValue.ContainsValue(fn))
            {
                FootnotesValue.Add(FootnotesValue.Count + 1, fn);
            }

            return FootnotesValue.Count;
        }

        public static void Clear()
        {
            FootnotesValue.Clear();
        }
    }
}
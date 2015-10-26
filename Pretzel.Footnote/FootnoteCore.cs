using System.Collections.Generic;

namespace Pretzel.Footnote
{
    /// <summary>
    /// The core class that will store the footnotes.
    /// </summary>
    internal static class FootnoteCore
    {
        /// <summary>
        /// The footnote list.
        /// </summary>
        private static Dictionary<int, string> footnotes = new Dictionary<int, string>();

        /// <summary>
        /// Gets the footnote list.
        /// </summary>
        public static Dictionary<int, string> Footnotes => footnotes;

        /// <summary>
        /// Adds a footnote.
        /// </summary>
        /// <param name="fn">The footnote.</param>
        /// <returns>The id of the footnote.</returns>
        public static int AddFootnote(string fn)
        {
            if (!footnotes.ContainsValue(fn))
            {
                footnotes.Add(footnotes.Count + 1, fn);
            }

            return footnotes.Count;
        }

        /// <summary>
        /// Clears the footnote list.
        /// </summary>
        public static void Clear()
        {
            footnotes.Clear();
        }
    }
}
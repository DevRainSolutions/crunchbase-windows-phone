using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DevRainSolutions.TechCompanies.Core
{
    public static class Helpers
    {
        private static readonly Dictionary<string, string> Replacements = new Dictionary<string, string>
                                                                       {
                                                                           {"&#8212;","-"},
                                                                            {"&#8220;","'"},
                                                                            {"&#8221;","'"},
                                                                           {"&nbsp;"," "},
                                                                           {"&#ldquo;",@"'"},
                                                                           {"&#rdquo;",@"'"},
                                                                           {"&#lsquo;",@"'"},
                                                                           {"&#rsquo;",@"'"},
                                                                           {"&ldquo;",@"'"},
                                                                           {"&rdquo;",@"'"},
                                                                           {"&raquo;",@"'"},
                                                                            {"&quot;",@"'"},
                                                                           {"&laquo;",@"'"},
                                                                           {"&lsquo;",@"'"},
                                                                           {"&rsquo;",@"'"},
                                                                           {"&amp;","&"},
                                                                           {"&lt;","<"},
                                                                           {"&lte;","<="},
                                                                           {"&gt;",">"},
                                                                           {"&gte;",">="},
                                                                           {"&ndash;","-"},
                                                                           {"&mdash;","-"},
                                                                           {"&middot;",""},
                                                                           //{"\n\n","\n"},
                                                                           {"&hellip;",""},
                                                                            {"\n\t","\n"},
                                                                           {"\t\n","\n"},
                                                                           {"\t\t \n","\n"},
                                                                           {"<p>",""},
                                                                           {"</p>","\n"}
                                                                           ,{"<br>","\n"},
                                                                           {"&#39;","'"},
                                                                            {"&bull;",""},
                                                                           //{"<p>",""},
                                                                           //{"</p>",". "}
                                                                        
                                                                       };

        private static readonly Dictionary<string, string> Removents = new Dictionary<string, string>
                                                                       {
                                                                           {"<style>", "</style>"},
                                                                            {"<xml>", "</xml>"}
                                                                       };

        public static string RemoveHtmlWithParagraphs(this string s)
        {
            //s = s.Replace("</p>", "\n\n");
            return Regex.Replace(s, @"<(.|\n)*?>", string.Empty);
        }


        public static string RemoveHtml(this string s)
        {
            return Regex.Replace(s, @"<(.|\n)*?>", string.Empty);
        }

        private static string RemoveTags(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            foreach (var startTag in Removents.Keys)
            {
                var endTag = Removents[startTag];
                int index = html.IndexOf(startTag, System.StringComparison.Ordinal);

                if (index == -1) continue;

                var endIndex = html.IndexOf(endTag, System.StringComparison.Ordinal);
                if (endIndex == -1) continue;


                html = html.Remove(index, endIndex - index + endTag.Length);
            }

            return html;
        }

        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            var r = Replacements.Keys.Aggregate(html, (current, mReplacement) => current.Replace(mReplacement, Replacements[mReplacement]));
            r = RemoveTags(r);
            return Regex.Replace(r, allowHarmlessTags ? "" : "<[^>]*>", string.Empty);
        }

        public static string Clear(string r, List<string> removes)
        {
            return string.IsNullOrEmpty(r) ? string.Empty : removes.Aggregate(r, (current, s) => current.Replace(s, string.Empty));
        }


        public static string ClearRssItemDescription(string vacancy)
        {
            return Clear(vacancy, new List<string> { "   " }).TrimEnd('\n');
        }

        
    }
}

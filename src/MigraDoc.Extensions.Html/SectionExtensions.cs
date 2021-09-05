using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace MigraDoc.Extensions.Html
{
    public static class SectionExtensions
    {
        public static Section AddHtml(this Section section, string html, bool isPdf = false)
        {
            return section.Add(Sanitize(html), new HtmlConverter(isPdf));
        }

        public static Cell AddHtml(this Cell cell, string html, bool isPdf = false)
        {
            return cell.Add(Sanitize(html), new HtmlConverter(isPdf));
        }

        public static Paragraph AddHtml(this Paragraph paragraph, string html, bool isPdf = false)
        {
            return paragraph.Add(Sanitize(html), new HtmlConverter(isPdf));
        }

        private static string Sanitize(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return html;
            }

            return html.Replace(@"\""", @"""");
        }
    }
}
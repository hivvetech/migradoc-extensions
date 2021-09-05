using MigraDoc.DocumentObjectModel;
using System;

namespace MigraDoc.Extensions
{
    public static class ParagraphExtensions
    {
        public static Paragraph SetStyle(this Paragraph paragraph, string style)
        {
            if (paragraph == null)
            {
                throw new ArgumentNullException(nameof(paragraph));
            }
            if (string.IsNullOrEmpty(style))
            {
                throw new ArgumentNullException(nameof(style));
            }

            paragraph.Style = style;
            return paragraph;
        }
    }
}
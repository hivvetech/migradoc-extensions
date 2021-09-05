using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;

namespace MigraDoc.Extensions
{
    public static class SectionExtensions
    {
        public static Section Add(this Section section, string contents, IConverter converter)
        {
            if (string.IsNullOrEmpty(contents))
            {
                throw new ArgumentNullException(nameof(contents));
            }
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            var addAction = converter.Convert(contents);
            addAction(section);
            return section;
        }

        public static Cell Add(this Cell cell, string contents, IConverter converter)
        {
            if (string.IsNullOrEmpty(contents))
            {
                throw new ArgumentNullException(nameof(contents));
            }
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }


            var addAction = converter.ConvertCell(contents);
            addAction(cell);
            return cell;
        }

        public static Paragraph Add(this Paragraph paragraph, string contents, IConverter converter)
        {
            if (string.IsNullOrEmpty(contents))
            {
                throw new ArgumentNullException(nameof(contents));
            }
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }


            var addAction = converter.ConvertParagraph(contents);
            addAction(paragraph);
            return paragraph;
        }
    }
}
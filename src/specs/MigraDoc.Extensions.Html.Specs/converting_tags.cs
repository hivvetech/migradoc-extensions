﻿//using MigraDoc.DocumentObjectModel;
//using NSpec;
//using System;

//namespace MigraDoc.Extensions.Html.Specs
//{
//    class ConvertingTagsSpec : nspec
//    {
//        Section pdf;

//        string html;

//        void before_each()
//        {
//            pdf = new Section();
//        }

//        void act_each()
//        {
//            pdf.AddHtml(html);
//        }

//        void plain_text()
//        {
//            before = () => html = "test";
//            it["adds a paragraph to the pdf containing the provided text"] = ()
//                => pdf.LastParagraph.should_not_be_null()
//                    .Elements[0].should_cast_to<Text>()
//                        .Content.should_be("test");
//        }

//        void html_encoded_text()
//        {
//            before = () => html = "&lt;Me &amp; You&gt;";

//            it["decodes the html"] = ()
//                => pdf.LastParagraph.Elements[0].should_cast_to<Text>()
//                    .Content.should_be("<Me & You>");
//        }

//        void paragraph_tag()
//        {
//            context["when the tag is empty"] = () =>
//            {
//                before = () => html = "<p></p>";
//                it["adds an empty paragraph to the pdf"] = ()
//                    => pdf.LastParagraph.Elements.Count.should_be(0);
//            };

//            context["when the tag contains plain text"] = () =>
//            {
//                before = () => html = "<p>test</p>";
//                it["adds a paragraph to the pdf containing the provided text"] = ()
//                    => pdf.LastParagraph.Elements[0].should_cast_to<Text>()
//                        .Content.should_be("test");
//            };
//        }

//        void heading_tags()
//        {
//            new[] { 1, 2, 3, 4, 5, 6 }.Do(x =>
//            {
//                context["heading{0} tag".With(x)] = () =>
//                {
//                    pdf = new Section();
//                    pdf.AddHtml("<h{0}></h{0}>".With(x));

//                    it["adds a paragraph with the style 'Heading{0}'".With(x)] = ()
//                        => pdf.LastParagraph.Style = "Heading{0}".With(x);
//                };
//            });
//        }

//        void strong_tag()
//        {
//            before = () => html = "<strong>test</strong>";
//            it["adds a paragraph with bold text"] = ()
//                => pdf.LastParagraph.should_not_be_null()
//                    .Elements[0].should_cast_to<FormattedText>()
//                        .Bold.should_be_true();
//        }

//        // edge case
//        void line_break_within_strong_tag()
//        {
//            before = () => html = "<strong>test<br/>test2</strong>";
//            it["adds bold text with a line break"] = ()
//                => pdf.LastParagraph.Elements[0].should_cast_to<FormattedText>()
//                    .Elements[1].should_cast_to<Character>()
//                        .SymbolName.should_be(SymbolName.LineBreak);
//        }

//        void italic_tag()
//        {
//            before = () => html = "<i>test</i>";
//            it["adds a paragraph with italic text"] = ()
//                => pdf.LastParagraph.should_not_be_null()
//                    .Elements[0].should_cast_to<FormattedText>()
//                        .Italic.should_be_true();
//        }

//        void emphasis_tag()
//        {
//            before = () => html = "<em>test</em>";
//            it["adds a paragraph with italic text"] = ()
//                => pdf.LastParagraph.should_not_be_null()
//                    .Elements[0].should_cast_to<FormattedText>()
//                        .Italic.should_be_true();
//        }

//        void underline_tag()
//        {
//            before = () => html = "<u>test</u>";
//            it["adds a paragraph with underlined text"] = ()
//                => pdf.LastParagraph.should_not_be_null()
//                    .Elements[0].should_cast_to<FormattedText>()
//                        .Underline.should_be(Underline.Single);
//        }

//        void nested_bold_italic_and_underline_tags()
//        {
//            FormattedText text = null;
//            before = () => html = "<strong><i><u>test</ul></i></strong>";

//            act = () =>
//            {
//                text = pdf.LastParagraph.Elements[0] as FormattedText;
//            };

//            it["adds a paragraph with formatted text"] = ()
//                => text.should_not_be_null();

//            it["the text is bold"] = () => text.Bold.should_be_true();
//            it["the text is italic"] = () => text.Italic.should_be_true();
//            it["the text is underlined"] = () => text.Underline.should_be(Underline.Single);
//        }

//        void anchor_tag()
//        {
//            Hyperlink link = null;

//            before = () => html = "<a href='http://www.google.com'>test</a>";

//            act = () => link = pdf.LastParagraph.Elements[0] as Hyperlink;

//            it["adds a hyperlink"] = () => link.should_not_be_null();
//            it["sets the link to the anchor's href"] = () => link.Name.should_be("http://www.google.com");
//            it["contains the anchor text"] = ()
//                => link.Elements[0].should_cast_to<Text>()
//                    .Content.should_be("test");
//        }

//        void div_tag()
//        {
//            before = () => html = "<div><p><strong>test</strong></p></div>";

//            it["processes the inner html as normal"] = ()
//                => pdf.LastParagraph.Elements[0].should_cast_to<FormattedText>()
//                    .Elements[0].should_cast_to<Text>()
//                        .Content.should_be("test");
//        }

//        void heading_followed_by_a_paragraph()
//        {
//            before = () => html = "<h1>Heading</h1><p>Content</p>";

//            it["adds a heading paragraph"] = ()
//                => pdf.Elements[0].should_cast_to<Paragraph>()
//                        .Style.should_be("Heading1");

//            it["adds a text paragraph"] = ()
//                => pdf.LastParagraph.Elements[0].should_cast_to<Text>()
//                    .Content.should_be("Content");
//        }

//        void whitespace_between_tags()
//        {
//            before = () => html = @"
//                <p>One</p>


//                <p>Two</p>
//            ";

//            it["removes the whitespace"] = ()
//                => pdf.Elements.Count.should_be(2);
//        }

//        void line_breaks_between_text_elements()
//        {
//            before = () => html = "this\nshould\nbe\ncollapsed";

//            it["removes the line breaks"] = ()
//                => pdf.LastParagraph.Elements.Count.should_be(1);
//        }

//        void unordered_list()
//        {
//            before = () => html = @"
//                <ul>
//                    <li>Item 1</li>
//                    <li>Item 2</li>
//                    <li>Item 3</li>
//                </ul>
//            ";

//            it["adds an opening paragraph to the start of the list with style 'ListStart'"] = ()
//                => pdf.Elements[0].should_cast_to<Paragraph>().Style.should_be("ListStart");
 
//            it["adds a paragraph for each list item with the style 'UnorderedList'"] = ()
//                =>
//            {
//                pdf.Elements.Count.should_be(5); // including the start/close paragraphs
//                for (int i = 1, j = pdf.Elements.Count - 1; i < j; i++)
//                {
//                    var li = pdf.Elements[i] as Paragraph;
//                    li.Style.should_be("UnorderedList");
//                }
//            };
            
//            it["adds a closing paragraph to the end of the list with style 'ListEnd'"] = ()
//                => pdf.LastParagraph.Style.should_be("ListEnd"); 
//        }

//        void ordered_list()
//        {
//            before = () => html = @"
//                <ol>
//                    <li>Item 1</li>
//                    <li>Item 2</li>
//                    <li>Item 3</li>
//                </ol>
//            ";

//            it["adds an opening paragraph to the start of the list with style 'ListStart'"] = ()
//                => pdf.Elements[0].should_cast_to<Paragraph>().Style.should_be("ListStart");

//            it["adds a paragraph for each list item with the style 'OrderedList'"] = ()
//            =>
//                {
//                    pdf.Elements.Count.should_be(5);
//                    for (int i = 1, j = pdf.Elements.Count - 1; i < j; i++)
//                    {
//                        var li = pdf.Elements[i] as Paragraph;
//                        li.Style.should_be("OrderedList");
//                    }
//                };

//            it["adds a closing paragraph to the end of the list with style 'ListEnd'"] = ()
//                => pdf.LastParagraph.Style.should_be("ListEnd"); 
//        }

//        void multiple_ordered_lists()
//        {
//            before = () => html = @"
//                <ol>
//                    <li>Item 1</li>
//                    <li>Item 2</li>
//                    <li>Item 3</li>
//                </ol>
//                <ol>
//                    <li>Item 1</li>
//                    <li>Item 2</li>
//                    <li>Item 3</li>
//                </ol>
//            ";
            
//            it["restarts numbering for each list"] = () =>
//            {
//                pdf.Elements[1].should_cast_to<Paragraph>() 
//                    .Format.ListInfo.ContinuePreviousList.should_be_false();

//                pdf.Elements[2].should_cast_to<Paragraph>()
//                    .Format.ListInfo.ContinuePreviousList.should_be_true();

//                // first item in the second list should not continue numbering
//                pdf.Elements[4].should_cast_to<Paragraph>()
//                    .Format.ListInfo.ContinuePreviousList.should_be_false();
//            };

//            it["still adds the 'ListEnd' paragraph"] = () 
//                => pdf.LastParagraph.Style.should_be("ListEnd");
//        }

//        void horizontal_rule_tag()
//        {
//            before = () => html = "<hr/>";

//            it["adds a paragraph with the style 'HorizontalRule'"] = ()
//                => pdf.LastParagraph.Style.should_be("HorizontalRule");
//        }

//        void line_break()
//        {
//            before = () => html = "<br/>";

//            it["adds a line break to the paragraph"] = ()
//                => pdf.LastParagraph.Elements[0].should_cast_to<Character>()
//                        .SymbolName.should_be(SymbolName.LineBreak);
//        }
//    }
//}

﻿using System;
using System.Drawing;
using System.IO;
using System.Text;
using Word;
using Word.Api.Interfaces;
using Word.Utils;
using Word.W2004;
using Word.W2004.Elements;
using Word.W2004.Elements.TableElements;
using Word.W2004.Style;
using Font = Word.W2004.Style.Font;
using Image = Word.W2004.Elements.Image;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IDocument myDoc = new Document2004();

            Properties prop = new Properties
                                  {
                                      //AppName = "Sharp2Word",
                                      Author = "Dublicator",
                                      LastSaved = new DateTime(2011, 11, 11)
                                  };
            myDoc.Head.Properties = prop;

            myDoc.addEle(BreakLine.times(1).create()); // this is one breakline

            //Headings
            myDoc.addEle(Heading2.with("===== Headings ======").create());
            myDoc.addEle(
                Paragraph.with(
                    "This doc has been generated by the unit test testJava2wordAllInOne() in the class DocumentTest2004Test.java.")
                    .create());
            myDoc.addEle(BreakLine.times(1).create());

            myDoc.addEle(Paragraph
                             .with("I will try to use a little bit of everything in the API Java2word. " +
                                   "I realised that is very dificult to keep the doucmentation updated " +
                                   "so this is where I will demostrate how to do some cool things with Java2word!")
                             .create());


            myDoc.addEle(Heading1.with("Heading01 without styling").create());
            myDoc.addEle(Heading2.with("Heading02 with styling").withStyle()
                             .setAlign(Align.CENTER).setItalic(true).create());
            myDoc.addEle(Heading3.with("Heading03").withStyle().setBold(true)
                             .setAlign(Align.RIGHT).create());

            //Paragraph and ParagrapPiece
            myDoc.addEle(Heading2.with("===== Paragraph and ParagrapPiece ======").create());
            myDoc.addEle(Paragraph.with("I am a very simple paragraph.").create());

            myDoc.addEle(BreakLine.times(1).create());
            ParagraphPiece myParPiece01 =
                ParagraphPiece.with(
                    "If you use the class 'Paragraph', you will have limited style. Maybe only paragraph aligment.");
            ParagraphPiece myParPiece02 =
                ParagraphPiece.with("In order to use more advanced style, you have to use ParagraphPiece");
            ParagraphPiece myParPiece03 =
                ParagraphPiece.with(
                    "One example of this is when you want to make ONLY one word BOLD or ITALIC. the way to to this is create many pieces, format them separetely and put all together in a Paragraph object. Example:");

            myDoc.addEle(Paragraph.withPieces(myParPiece01, myParPiece02, myParPiece03).create());

            ParagraphPiece myParPieceJava =
                ParagraphPiece.with("I like Java and ").withStyle().setFont(Font.COURIER).create();
            ParagraphPiece myParPieceRuby =
                ParagraphPiece.with("Ruby!!! ").withStyle().setBold(true).setItalic(true).create();
            ParagraphPiece myParPieceAgile =
                ParagraphPiece.with("I actually love Java, Ruby Agile, BDD, Cucumber, automation... ").withStyle().
                    setTextColor("008000").create();

            myDoc.addEle(Paragraph.withPieces(myParPieceJava, myParPieceRuby, myParPieceAgile).create());

            //font size
            myDoc.addEle(Paragraph.withPieces(ParagraphPiece.with("No size").create(),
                                              ParagraphPiece.with("I am size 50.").withStyle().setFontSize(50).setTextColor(Color.Cyan).create()));

            //Document Header and Footer
            myDoc.addEle(BreakLine.times(2).create());
            myDoc.addEle(Heading2.with("===== Document Header and Footer ======").create());
            myDoc.addEle(Paragraph.with("By default everything is added to the Body when you do 'myDoc.addEle(...)'." +
                                        " But you can add elements to the Header and/or Footer. Other cool thing is show page number or not.")
                             .create());

            myDoc.addEle(BreakLine.times(2).create());
            myDoc.addEle(
                Paragraph.with(
                    "Page number is displayed by default but you can disable: 'myDoc.getFooter().showPageNumber(false)' ")
                    .create());

            myDoc.addEle(BreakLine.times(2).create());
            myDoc.addEle(
                Paragraph.with(
                    "you can also hide Header and Footer in the first Page. This is useful for when you have a cover page.: 'myDoc.getHeader().setHideHeaderAndFooterFirstPage(true)' ")
                    .create());

            myDoc.Header.addEle(
                Paragraph.withPieces(ParagraphPiece.with("I am in the"),
                                     ParagraphPiece.with(" Header ").withStyle().setBold(true).create(),
                                     ParagraphPiece.with("of all pages")).create());

            myDoc.Footer.addEle(Paragraph.with("I am in the Footer of all pages").create());


            //Images
            myDoc.addEle(BreakLine.times(1).create());
            myDoc.addEle(Heading2.with("===== Images ======").create());
            myDoc.addEle(
                Paragraph.with(
                    "Images can be created from diferent locations. It can be from your local machine, from web URL or classpath.")
                    .create());

            myDoc.addEle(Paragraph.with("This one is coming from WEB, google web site: ").create());
            myDoc.addEle(Image.from_WEB_URL("http://www.google.com/images/logos/ps_logo2.png"));

            myDoc.addEle(BreakLine.times(2).create());
            myDoc.addEle(Paragraph.with("You can change the image dimensions:.").create());
            myDoc.addEle(
                Image.from_WEB_URL("http://www.google.com/images/logos/ps_logo2.png").setHeight("40").setWidth("80").
                    create());


            myDoc.addEle(BreakLine.times(2).create());
            myDoc.addEle(
                Paragraph.with(
                    "You can always be creative mixing up images inside other IElements. Eg.: Paragraphs, Tables, etc.")
                    .create());

            /*myDoc.addEle(
                            new Paragraph("This document inside the paragraph, coming from '/src/test/resources/dtpick.gif': "
                                            + Image.from_FULL_LOCAL_PATHL(Utils.getAppRoot() + "/src/test/resources/dtpick.gif").getContent()));
            */
            myDoc.addEle(BreakLine.times(1).create());


            //Table
            myDoc.addEle(Heading2.with("===== Table ======").create());
            myDoc.addEle(
                Paragraph.with("Table os soccer playerd and their number of gols - the best of the best of all times:").
                    create());
            myDoc.addEle(BreakLine.times(1).create());

            Table tbl = new Table();
            tbl.addTableEle(TableEle.TH, "Name", "Number of gols", "Country");

            tbl.addTableEle(TableEle.TD, "Arthur Friedenreich", "1329", "Brazil");
            tbl.addTableEle(TableEle.TD, "Pele", "1281", "Brazil");
            tbl.addTableEle(TableEle.TD, "Romario", "1002", "Brazil");
            tbl.addTableEle(TableEle.TD, "Tulio Maravilha", "956", "Brazil");
            tbl.addTableEle(TableEle.TD, "Zico", "815", "Brazil");
            tbl.addTableEle(TableEle.TD, "Roberto Dinamite", "748", "Brazil");
            tbl.addTableEle(TableEle.TD, "Di Stéfano", "715", "Argentina");
            tbl.addTableEle(TableEle.TD, "Puskas", "689", "Hungary");
            tbl.addTableEle(TableEle.TD, "Flávio", "591", "Brazil");
            tbl.addTableEle(TableEle.TD, "James McGory", "550", "Scotland");
            tbl.addTableEle(TableEle.TD, "Leonardo Correa", "299", "Brazil/Australia");

            tbl.addTableEle(TableEle.TF, "Total", "1,100,000.00", " ");

            myDoc.addEle(tbl);

            myDoc.addEle(BreakLine.times(1).create());

            myDoc.addEle(
                Paragraph.withPieces(
                    ParagraphPiece.with("* Zico was mid-fieldfer and managed to score all those fucking goals!").
                        withStyle().setItalic(true).create()).create());
            myDoc.addEle(
                Paragraph.withPieces(
                    ParagraphPiece.with(
                        "* Leonardo Correa's goals (me) include futsal, soccer, friendly games, training games, so on... (but not playstation)")
                        .withStyle().setItalic(true).create()).create());


            //PageBreaks
            myDoc.addEle(Heading2.with("===== PageBreak ======").create());
            myDoc.addEle(Paragraph.with("There is a PAGE BREAK after this line:").create());
            myDoc.addEle(PageBreak.create());
            myDoc.addEle(Paragraph.with("There is a PAGE BREAK before this line:").create());

            //myDoc.Save(@"C:\testWord.doc");
            string myWord = myDoc.Content;
           // string msg = "";
           // bool b=XmlValidator.ValidateXml(myWord, ref msg, Util.getAppRoot() + "\\Schemas\\wordml.xsd");
           // Console.WriteLine("Result -" + b);
            //Console.WriteLine(msg);
            //Console.ReadKey();

            using (FileStream fs = new FileStream(@"c:\mytest.doc", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.Write(Util.pretty(myWord));
                }
            }

        }

    }
}

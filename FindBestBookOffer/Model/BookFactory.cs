using System.Linq;
using FindBestBookOffer.Utils;
using HtmlAgilityPack;

namespace FindBestBookOffer.Model
{
    public static class BookFactory
    {
        public static Book BookFromHtmlNode(HtmlNode htmlNode)
        {
            var book = new Book();
            var isbn10 = htmlNode.Descendants().Single(childNode => childNode.IsIsbn10()).InnerText;
            book.ISBN10 = isbn10;

            var isbn13 = htmlNode.Descendants().Single(childNode => childNode.IsIsbn13()).InnerText;
            book.ISBN13 = isbn13;

            var author = htmlNode.Descendants().Single(childNode => childNode.IsAuthor()).InnerText;
            book.Author = author;

            var title = htmlNode.Descendants().Single(childNode => childNode.IsTitle()).ChildNodes.Single().InnerText;
            book.Title = title;

            return book;
        }
    }
}
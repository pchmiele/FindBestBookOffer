using FindBestBookOffer.Model;
using HtmlAgilityPack;

namespace FindBestBookOffer.ISBNSearch
{
    internal class ISBNSearchServiceResult
    {
        private readonly HtmlNode htmlNode;

        internal ISBNSearchServiceResult(HtmlNode htmlNode)
        {
            this.htmlNode = htmlNode;
        }

        public Book AsBook()
        {
            return BookFactory.BookFromHtmlNode(htmlNode);
        }
    }
}
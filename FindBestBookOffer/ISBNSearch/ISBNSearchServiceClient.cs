using HtmlAgilityPack;

namespace FindBestBookOffer.ISBNSearch
{
    public class ISBNSearchServiceClient
    {
        public ISBNSearchServiceResults SearchForBooks(string title)
        {
            var searchForTitleUri = ISBNSearchServiceUriTemplate.SearchByTitleUri(title);

            var searchResultsPage = new HtmlWeb();
            var searchResultsHtmlDocument = searchResultsPage.Load(searchForTitleUri.AbsoluteUri);

            return new ISBNSearchServiceResults(searchResultsHtmlDocument);
        }
    }
}
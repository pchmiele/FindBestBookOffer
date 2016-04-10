using HtmlAgilityPack;

namespace FindBestBookOffer.Apress
{
    public class ApressClient
    {
        public ApressSearchResult SearchForBookWithISBN13(string isbn13)
        {
            var apressSearchByISBNUri = ApressUriTemplate.SearchByISBNUri(isbn13);

            var searchResultsPage = new HtmlWeb();
            var searchResultsHtmlDocument = searchResultsPage.Load(apressSearchByISBNUri.AbsoluteUri);

            return new ApressSearchResult(searchResultsHtmlDocument, apressSearchByISBNUri);
        }
    }
}
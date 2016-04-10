using System.Linq;
using FindBestBookOffer.Model;

namespace FindBestBookOffer.ISBNSearch
{
    public class ISBNSearchService
    {
        private readonly ISBNSearchServiceClient isbnSearchServiceClient;

        public ISBNSearchService(ISBNSearchServiceClient isbnSearchServiceClient)
        {
            this.isbnSearchServiceClient = isbnSearchServiceClient;
        }

        public FoundBooks FindBooksByTitle(string title)
        {
            var foundBooks = new FoundBooks();
            var searchResults = isbnSearchServiceClient.SearchForBooks(title);
            searchResults.Results.ToList().ForEach(result => foundBooks.Add(result.AsBook()));
            return foundBooks;
        }
    }
}
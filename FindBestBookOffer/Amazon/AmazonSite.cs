using FindBestBookOffer.Interfaces;

namespace FindBestBookOffer.Amazon
{
    public class AmazonSite
    {
        private readonly AmazonClient amazonClient;

        public AmazonSite(AmazonClient apressClient)
        {
            amazonClient = apressClient;
        }

        public ISearchResult FindBookByISBN(string isbn13)
        {
            var result = amazonClient.SearchForBookWithISBN13(isbn13);
            return result.IsSuccessfull ? result : null;
        }
    }
}
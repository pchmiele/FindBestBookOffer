using FindBestBookOffer.Interfaces;

namespace FindBestBookOffer.Apress
{
    public class ApressSite
    {
        private readonly ApressClient apressClient;

        public ApressSite(ApressClient apressClient)
        {
            this.apressClient = apressClient;
        }

        public ISearchResult FindBookByISBN(string isbn13)
        {
            var result = apressClient.SearchForBookWithISBN13(isbn13);
            return result.IsSuccessfull ? result : null;
        }
    }
}
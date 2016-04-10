using System;

namespace FindBestBookOffer.Apress
{
    internal static class ApressUriTemplate
    {
        private const string BaseUri = "http://www.apress.com/";

        public static Uri SearchByISBNUri(string isbn13)
        {
            return new Uri(string.Format("{0}{1}?gtmf=s", BaseUri, isbn13));
        }
    }
}
using System;
using System.Web;

namespace FindBestBookOffer.ISBNSearch
{
    internal static class ISBNSearchServiceUriTemplate
    {
        private const string BaseUri = "http://www.isbnsearch.org/";

        public static Uri SearchByTitleUri(string title)
        {
            var titleEncoded = HttpUtility.UrlEncode(title);
            return new Uri(string.Format("{0}search?s={1}", BaseUri, titleEncoded));
        }
    }
}
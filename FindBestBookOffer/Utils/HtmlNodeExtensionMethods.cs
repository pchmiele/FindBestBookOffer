using HtmlAgilityPack;

namespace FindBestBookOffer.Utils
{
    public static class HtmlNodeExtenionMethods
    {
        private static bool IsH3(this HtmlNode htmlNode)
        {
            return htmlNode.Name == "h3";
        }

        private static bool IsStrong(this HtmlNode htmlNode)
        {
            return htmlNode.Name == "strong";
        }

        private static bool IsUl(this HtmlNode htmlNode)
        {
            return htmlNode.Name == "ul";
        }

        private static bool IsP(this HtmlNode htmlNode)
        {
            return htmlNode.Name == "p";
        }

        private static bool IsDiv(this HtmlNode htmlNode)
        {
            return htmlNode.Name == "div";
        }

        private static bool HasClassAttribute(this HtmlNode htmlNode)
        {
            return htmlNode.Attributes["class"] != null;
        }

        private static bool HasExpectedClass(this HtmlNode htmlNode, string expectedClass)
        {
            return htmlNode.HasClassAttribute() &&
                   htmlNode.Attributes["class"].Value.Equals(expectedClass);
        }

        public static bool IsBookInfo(this HtmlNode htmlNode)
        {
            return htmlNode.IsDiv() && htmlNode.HasExpectedClass("bookinfo");
        }

        public static bool IsIsbn13(this HtmlNode htmlNode)
        {
            return htmlNode.IsP() && htmlNode.HasExpectedClass("isbn isbnleft");
        }

        public static bool IsAuthor(this HtmlNode htmlNode)
        {
            return htmlNode.IsP() && !htmlNode.HasClassAttribute();
        }

        public static bool IsTitle(this HtmlNode htmlNode)
        {
            return htmlNode.IsH3();
        }

        public static bool IsPricesNode(this HtmlNode htmlNode)
        {
            return htmlNode.IsUl() && htmlNode.HasExpectedClass("prices");
        }

        public static bool IsEbookPrice(this HtmlNode htmlNode)
        {
            return htmlNode.IsStrong() && htmlNode.InnerText.Contains("eBook Price:");
        }

        public static bool IsIsbn10(this HtmlNode htmlNode)
        {
            return htmlNode.IsP() && htmlNode.HasExpectedClass("isbn");
        }

        public static bool IsDetailPageURL(this HtmlNode htmlNode)
        {
            return htmlNode.Name == "DetailPageURL";
        }
    }
}
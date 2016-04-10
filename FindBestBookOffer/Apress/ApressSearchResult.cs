using System;
using System.Linq;
using System.Text;
using FindBestBookOffer.Utils;
using FindBestBookOffer.Interfaces;
using FindBestBookOffer.Model;
using HtmlAgilityPack;

namespace FindBestBookOffer.Apress
{
    public class ApressSearchResult : ISearchResult
    {
        private readonly HtmlDocument htmlDocument;

        private readonly bool isSuccessfull;

        private Price bookPrice;

        internal ApressSearchResult(HtmlDocument htmlDocument, Uri bookUri)
        {
            this.htmlDocument = htmlDocument;
            BookUri = bookUri;

            isSuccessfull = DetermineIfSuccessful();
        }

        public Price BookPrice
        {
            get
            {
                if (bookPrice != null)
                {
                    return bookPrice;
                }

                var price = GetBookPrice();
                bookPrice = new Price(decimal.Parse(price), Currency.USD);
                return bookPrice;
            }
        }

        public Uri BookUri { get; private set; }

        public bool IsSuccessfull
        {
            get { return isSuccessfull; }
        }

        private HtmlNode GetPricesNode()
        {
            var pricesNode = htmlDocument.DocumentNode.Descendants().First(childNode => childNode.IsPricesNode());
            return pricesNode;
        }

        private HtmlNode GetEbookPriceNode()
        {
            var pricesNode = GetPricesNode();
            var ebookPriceNode = pricesNode.Descendants().Single(childNode => childNode.IsEbookPrice());
            return ebookPriceNode;
        }

        private string GetPriceWithDolarPrefix()
        {
            var ebookPriceNode = GetEbookPriceNode();
            var ebookPriceNodeWithoutPreviousPrice = Price.IsPreviousPriceRegex()
                .Replace(ebookPriceNode.InnerHtml, string.Empty);
            return Price.IsPriceInUSDRegex().Match(ebookPriceNodeWithoutPreviousPrice).Value;
        }

        private string GetBookPrice()
        {
            var bookPriceWithDolarPrefix = GetPriceWithDolarPrefix();
            return bookPriceWithDolarPrefix.Replace("$", string.Empty);
        }

        private bool DetermineIfSuccessful()
        {
            return
                !htmlDocument.DocumentNode.InnerText.Contains("Sorry, there’s nothing at that URL. - Apress IT eBooks");
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Book From Apress");
            stringBuilder.AppendLine(string.Format("Uri: {0}", BookUri.AbsoluteUri));
            stringBuilder.AppendLine(string.Format("Price: {0}", BookPrice));

            return stringBuilder.ToString();
        }
    }
}
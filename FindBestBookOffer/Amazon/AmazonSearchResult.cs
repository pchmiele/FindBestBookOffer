using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FindBestBookOffer.Interfaces;
using FindBestBookOffer.Model;

namespace FindBestBookOffer.Amazon
{
    public class AmazonSearchResult : ISearchResult
    {
        private readonly bool isSuccessfull;
        private readonly XDocument xmlDocument;

        private Price actualBookPrice;

        private Uri linkToBook;

        internal AmazonSearchResult(string response)
        {
            xmlDocument = XDocument.Parse(response);
            isSuccessfull = DetermineIfSuccessful(response);
        }

        public XNamespace AmazonWebServiceNameSpace { get; set; }

        public Price BookPrice
        {
            get
            {
                if (actualBookPrice != null)
                {
                    return actualBookPrice;
                }

                var lowestPrice = GetLowestPrice();
                actualBookPrice = new Price(decimal.Parse(lowestPrice), Currency.USD);
                return actualBookPrice;
            }
        }

        public Uri BookUri
        {
            get
            {
                if (linkToBook != null)
                {
                    return linkToBook;
                }

                linkToBook = new Uri(GetBookUrl());
                return linkToBook;
            }
        }

        public bool IsSuccessfull
        {
            get { return isSuccessfull; }
        }

        private bool DetermineIfSuccessful(string input)
        {
            return !input.Contains("<Errors>");
        }

        private XName FullTagName(string tag)
        {
            return AmazonWebServiceNameSpace == null ? tag : AmazonWebServiceNameSpace + tag;
        }

        private XElement GetLowestPriceNode()
        {
            var lowestNewPriceFullTagName = FullTagName("LowestNewPrice");
            return xmlDocument.Descendants(lowestNewPriceFullTagName).Single();
        }

        private string GetLowestPriceWithDolarPrefix()
        {
            var formattedPriceFullTagName = FullTagName("FormattedPrice");
            var lowestNewPriceNode = GetLowestPriceNode();
            return lowestNewPriceNode.Descendants(formattedPriceFullTagName).Single().Value;
        }

        private string GetLowestPrice()
        {
            var lowestNewPriceWithDolarPrefix = GetLowestPriceWithDolarPrefix();
            return lowestNewPriceWithDolarPrefix.Replace("$", string.Empty);
        }

        private string GetBookUrl()
        {
            var detailPageUrlFullTagName = FullTagName("DetailPageURL");
            return xmlDocument.Descendants(detailPageUrlFullTagName).Single().Value;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Book From Amazon");
            stringBuilder.AppendLine(string.Format("Uri: {0}", BookUri.AbsoluteUri));
            stringBuilder.AppendLine(string.Format("Price: {0}", BookPrice));

            return stringBuilder.ToString();
        }
    }
}
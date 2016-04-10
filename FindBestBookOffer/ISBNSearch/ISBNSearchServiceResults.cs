using System.Collections.Generic;
using System.Linq;
using FindBestBookOffer.Utils;
using HtmlAgilityPack;

namespace FindBestBookOffer.ISBNSearch
{
    public class ISBNSearchServiceResults
    {
        private readonly HtmlDocument htmlDocument;

        internal ISBNSearchServiceResults(HtmlDocument htmlDocument)
        {
            this.htmlDocument = htmlDocument;
        }

        internal IEnumerable<ISBNSearchServiceResult> Results
        {
            get
            {
                var results = new List<ISBNSearchServiceResult>();

                htmlDocument.DocumentNode.Descendants()
                    .Where(childNode => childNode.IsBookInfo())
                    .ToList()
                    .ForEach(singleResult => results.Add(new ISBNSearchServiceResult(singleResult)));

                return results;
            }
        }
    }
}
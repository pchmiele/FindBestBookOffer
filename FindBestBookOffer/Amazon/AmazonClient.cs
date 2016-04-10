using System.Collections.Generic;
using System.Xml.Linq;
using Nager.AmazonProductAdvertising;
using Nager.AmazonProductAdvertising.Model;

namespace FindBestBookOffer.Amazon
{
    public class AmazonClient
    {
        private readonly AmazonAuthentication AmazonAuthentication;
        private readonly XNamespace AmazonWebServiceNameSpace;
        private readonly AmazonWrapper AmazonWrapper;
        private readonly string AssociateTag;

        public AmazonClient(AppSettings appSettings)
        {
            AssociateTag = appSettings.AssociateTag;
            AmazonWebServiceNameSpace = appSettings.AmazonWebServiceNameSpace;

            AmazonAuthentication = new AmazonAuthentication
            {
                AccessKey = appSettings.AccessKey,
                SecretKey = appSettings.SecretKey
            };

            AmazonWrapper = new AmazonWrapper(AmazonAuthentication, AmazonEndpoint.US, appSettings.AssociateTag);
        }

        public AmazonSearchResult SearchForBookWithISBN13(string isbn13)
        {
            var request = new Dictionary<string, string>();
            request["Operation"] = "ItemLookup";
            request["ResponseGroup"] = "Large";
            request["SearchIndex"] = "Books";
            request["IdType"] = "EAN";
            request["ItemId"] = isbn13;
            request["AssociateTag"] = AssociateTag;

            var response = AmazonWrapper.Request(request);

            return new AmazonSearchResult(response) {AmazonWebServiceNameSpace = AmazonWebServiceNameSpace};
        }
    }
}
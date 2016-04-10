using System.Xml.Linq;

namespace FindBestBookOffer.Utils
{
    public static class XElementExtensionMethods
    {
        public static bool IsDetailPageURLTag(this XElement xElement)
        {
            return xElement.Name == "DetailPageURL";
        }

        public static bool IsLowestNewPriceTag(this XElement xElement)
        {
            return xElement.Name == "LowestNewPrice";
        }
    }
}
using System.ServiceModel.Syndication;
using System.Xml.Linq;

namespace Infrastructure.Utilities
{
    public class GetImageUrlFromItemHelper
    {
        public static string? GetImageUrlFromItem(SyndicationItem item)
        {
            // Extract image URL from an enclosure tag (often in RSS 2.0 feeds)
            var enclosure = item.Links.FirstOrDefault(link => link.RelationshipType == "enclosure" && link.MediaType?.StartsWith("image") == true);
            if (enclosure != null)
            {
                return enclosure.Uri.ToString();
            }

            // Alternative method: Extract image from media:content or media:thumbnail
            foreach (var extension in item.ElementExtensions)
            {
                if (extension.OuterName == "content" || extension.OuterName == "thumbnail")
                {
                    var xmlElement = extension.GetObject<XElement>();
                    var urlAttribute = xmlElement.Attribute("url");
                    if (urlAttribute != null)
                    {
                        return urlAttribute.Value;
                    }
                }
            }

            // Method for content:encoded tags
            var contentEncoded = item.ElementExtensions.FirstOrDefault(ext =>
                ext.OuterName == "encoded" && ext.OuterNamespace == "http://purl.org/rss/1.0/modules/content/");

            if (contentEncoded != null)
            {
                var htmlContent = contentEncoded.GetObject<XElement>().Value;

                // HTML parsing to find the image tag
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlContent);

                var imgTag = doc.DocumentNode.SelectSingleNode("//img");
                if (imgTag != null)
                {
                    var src = imgTag.GetAttributeValue("src", null);
                    return src;
                }
            }


            return null;
        }
    }
}
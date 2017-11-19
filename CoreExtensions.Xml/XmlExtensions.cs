using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CoreExtensions
{
    public static class XmlExtensions
    {
        private static XElement Sort(XElement element)
        {
            return new XElement(element.Name,
                    element.Attributes(),
                    from child in element.Nodes()
                    where child.NodeType != XmlNodeType.Element
                    select child,
                    from child in element.Elements()
                    orderby child.Name.ToString()
                    select Sort(child));
        }

        public static string Sort(this XmlDocument file)
        {
            var newFile = XDocument.Parse(file.OuterXml);
            var xDoc = new XDocument(
                    newFile.Declaration,
                    from child in newFile.Nodes()
                    where child.NodeType != XmlNodeType.Element
                    select child,
                    Sort(newFile.Root));
            return xDoc.ToString();
        }

        public static Stream ToMemoryStream(this XmlDocument doc)
        {
            var xmlStream = new MemoryStream();
            doc.Save(xmlStream);
            xmlStream.Flush(); //Adjust this if you want read your data
            xmlStream.Position = 0;
            return xmlStream;
        }

        public static XDocument ToXDocument(this XmlDocument doc)
        {
            return XDocument.Parse(doc.OuterXml);
        }

        public static XmlDocument ToXmlDocument(this XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }
    }
}

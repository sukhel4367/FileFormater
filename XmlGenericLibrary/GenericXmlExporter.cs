using System.Text;
using ExporterCore;
using System.Xml.Serialization;

namespace XmlGenericLibrary
{
    public class GenericXmlExporter<T> : IExporter<T>
    {
        public string Name => "XML";
        public string Extension => "xml";

        public void Export(List<T> data, string filePath)
        {
            var serializer = new XmlSerializer(typeof(List<T>));
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, data);
            }
        }
    }

}

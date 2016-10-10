using System;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.Text;

namespace Serialization
{
    public class Serializator
    {
        Catalog CatalogBinDeserialize { get; set; }
        Catalog CatalogXmlDeserialize { get; set; }
        public Catalog Catalog = new Catalog();
        Catalog catalogJavaScriptDeserialize;
        public const string FileNameBin = "Catalog.bin";
        public const string FileNameXml = "Catalog.xml";
        public const string FileNameJson = "Catalog.json";
        public void ToSerialize()
        {
            try
            {
                Catalog catalog = new Catalog();
                catalog.Fill($@"{Environment.CurrentDirectory}/Books.xml");

                // Use BinaryFormatter
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(FileNameBin, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, catalog);
                }
                

                // Use XmlSerializer           
                var serializerXml = new XmlSerializer(typeof(Catalog));
                using (var streamXml = new FileStream(FileNameXml, FileMode.Create))
                {
                    serializerXml.Serialize(streamXml, catalog);
                }

                // Use JavascriptSerializer
                var serializerJavaScript = new JavaScriptSerializer();
                using (var streamJson = new FileStream(FileNameJson, FileMode.Create))
                {
                    var str = serializerJavaScript.Serialize(catalog);
                    byte[] info = new UTF8Encoding(true).GetBytes(str);
                    streamJson.Write(info, 0, info.Length);
                }

                // Deserialize object
                using (var streamDeserialize = new FileStream(FileNameBin, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    CatalogBinDeserialize = (Catalog)formatter.Deserialize(streamDeserialize);
                }
                CatalogXmlDeserialize = serializerXml.Deserialize(new FileStream(FileNameXml, FileMode.Open)) as Catalog;
                catalogJavaScriptDeserialize = serializerJavaScript.Deserialize<Catalog>(File.ReadAllText(FileNameJson));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

    }
}

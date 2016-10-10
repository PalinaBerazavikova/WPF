using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Serializator serializator = new Serializator();
            serializator.ToSerialize();
            foreach (var item in serializator.Catalog.ListBook)
            {
                Console.WriteLine("{0} = {1}", item.Author, item.Title);
            }
            Console.ReadKey();
        }
    }
}

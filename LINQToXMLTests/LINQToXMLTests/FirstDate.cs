using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace LINQToXMLTests
{
    [TestClass]
    public class FirstDate
    {
        [TestMethod]
        public void GetFirstDate()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().Elements().Where(c => !c.Element("orders").IsEmpty).Select(x => new
            {
                name = x.Element("name").Value,
                date = x.Elements("orders").Elements().Elements("orderdate").Select(y => DateTime.Parse(y.Value)).Min(),
            }).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersFirstDate.txt"))
            {
                foreach (var b in a)

                {
                    file.WriteLine($"{b.name} { b.date:MM:yyyy}");
                }
            }
        }

    }
}


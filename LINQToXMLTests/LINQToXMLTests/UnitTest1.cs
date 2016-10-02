using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LINQToXMLTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().Elements().Where(c => !c.Element("orders").IsEmpty).Select(x => new
            {
                name = x.Element("name").Value,
                date = x.Elements("orders").Elements().Elements("orderdate").Select(y => DateTime.Parse(y.Value)).Min(),
            }).ToList();
            foreach (var b in a)
            {
                Console.WriteLine(b.name + " date " + b.date);
            }
        }
    }
}

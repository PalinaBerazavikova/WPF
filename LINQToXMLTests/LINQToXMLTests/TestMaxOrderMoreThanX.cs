using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.IO;
using System.Linq;

namespace LINQToXMLTests
{
    [TestClass]
    public class TestMaxOrderMoreThanX
    {
        [TestMethod]
        public void TestMaxOrdersX1()
        {
            WriteNamesWithMaxOrderMoreThanX(15000);
        }
        [TestMethod]
        public void TestMaxOrdersX2()
        {
            WriteNamesWithMaxOrderMoreThanX(10000);
        }
        [TestMethod]
        public void TestMaxOrdersX3()
        {
            WriteNamesWithMaxOrderMoreThanX(1000);
        }
        public void WriteNamesWithMaxOrderMoreThanX(int n)
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().Elements().Where(c => !c.Element("orders").IsEmpty).Select(x => new
            {
                name = x.Element("name").Value,
                maxOrder = x.Elements("orders").Elements().Elements("total").Select(y => double.Parse(y.Value.Replace(".", ","))).Max(),

            }).Where(x => x.maxOrder > n).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersWithMaxOrderMoreThan{n}.txt"))
            {
                foreach (var f in a)
                {
                    file.WriteLine(f.name);
                }
            }
        }
    }
}

using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.IO;

namespace LINQToXMLTests
{
    [TestClass]
    public class TestOrdersSumMoreThanX
    {
        [TestMethod]
        public void TestOrdersSumX1()
        {
            TestOrdersSum(150000);
        }
        [TestMethod]
        public void TestOrdersSumX2()
        {
            TestOrdersSum(10000);
        }
        [TestMethod]
        public void TestOrdersSumX3()
        {
            TestOrdersSum(100000);
        }


        public void TestOrdersSum(int n)
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().Elements().Select(x => new
            {
                name = x.Element("name").Value,
                sum = x.Elements("orders").Elements().Elements("total").Select(y => double.Parse(y.Value.Replace(".", ","))).Sum(),

            }).Where(x => x.sum > n).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersWithOrderMoreThan{n}.txt"))
            {
                foreach (var f in a)
                {
                    file.WriteLine(f.name);
                }
            }
        }
    }
}



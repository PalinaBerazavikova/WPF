using System;
using System.Xml.Linq;
using NUnit.Framework;
using System.Linq;
using System.IO;

namespace LINQToXMLTests
{
    [TestFixture]
    public class TestOrdersSumMoreThanX
    {
        public string FileName { get; set; } = string.Empty;
        public XDocument Document { get; set; }
        [SetUp]
        public void BeforeMethod()
        {
            this.FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            this.Document = XDocument.Load(this.FileName);
        }
        [TearDown]
        public void AfterMethod()
        {
            this.FileName = null;
            this.Document = null;
        }


        [TestCase(7000, 7000)]
        [TestCase(9000, 9000)]
        [TestCase(20000, 123)]
        public void TestMethod(double n, double exp)
        {
            var a = this.Document.Elements().Elements().Select(x => new
            {
                name = x.Element("name").Value,
                sum = x.Elements("orders").Elements().Elements("total").Select(y => double.Parse(y.Value.Replace(".", ","))).Sum(),

            }).Where(x => x.sum > n).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\Output.txt"))
            {
                file.WriteLine($"Customers with summary order more than{n}");
                foreach (var f in a)
                {
                    file.WriteLine(f.name);
                }
            }
            Assert.AreEqual(n, exp);
        }
    }
}



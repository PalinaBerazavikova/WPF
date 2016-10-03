using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace LINQToXMLTests
{
    [TestClass]
    public class TestSomethingWrong
    {
        [TestMethod]
        public void TestNoRegion()
        {
            NoRegion();
        }
        [TestMethod]
        public void TestPostalcodeWithLetters()
        {
            PostalcodeWithLetters();
        }
        public void PostalcodeWithLetters()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().
                Elements().
                Select(x => new
                {
                    region = x.Element("region"),
                    postalcode = x.Element("postalcode"),
                    phone = x.Element("phone").Value,
                }).ToList();
            int res;

            var result = a.Where(x =>  ((x.postalcode != null) && (Regex.IsMatch(x.postalcode.Value, "[^0-9-]"))) )))).ToList();
            // using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersFirstDate.txt"))
            // {
            foreach (var b in result)

            {
                Console.WriteLine($"{b}");
                //file.WriteLine($"{b.name} { b.date:MM:yyyy}");
            }
            // }
        }
        public void NoRegion()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().
                Elements().
                Select(x => new
                {
                    name = x.Element("name").Value,
                    region = x.Element("region"),
                    postalcode = x.Element("postalcode"),
                    phone = x.Element("phone").Value,
                }).ToList();

            var result = a.Where(x => (x.region == null)).ToList();
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersWithNoRegion.txt"))
            {
                foreach (var b in result)

                {
                    file.WriteLine($"{b.name} ");
                }
            }
        }
    }
}

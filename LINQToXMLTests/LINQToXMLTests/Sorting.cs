using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace LINQToXMLTests
{
    [TestClass]
    public class Sorting
    {
        [TestMethod]
        public void TestSortByYear()
        {
            SortByYear();
        }
        [TestMethod]
        public void TestSortByMonth()
        {
            SortByMonth();
        }
        [TestMethod]
        public void TestSortByOrdersSum()
        {
            SortByOrdersSum();
        }
        [TestMethod]
        public void TestSortByName()
        {
            SortByName();
        }

        public void SortByYear()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().Elements().Where(c => !c.Element("orders").IsEmpty).Select(x => new
            {
                name = x.Element("name").Value,
                date = x.Elements("orders").Elements().Elements("orderdate").Select(y => DateTime.Parse(y.Value)).Min(),
            }).ToList();
            var sortByYear = a.OrderBy(x => x.date.Year);
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersSortedByYear.txt"))
            {
                foreach (var b in sortByYear)

                {
                    file.WriteLine($"{b.name} { b.date:yyyy}");
                }
            }
        }
        public void SortByMonth()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().Elements().Where(c => !c.Element("orders").IsEmpty).Select(x => new
            {
                name = x.Element("name").Value,
                date = x.Elements("orders").Elements().Elements("orderdate").Select(y => DateTime.Parse(y.Value)).Min(),
            }).ToList();
            var sortByMonth = a.OrderBy(x => x.date.Month);
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersSortedByMonth.txt"))
            {
                foreach (var b in sortByMonth)

                {
                    file.WriteLine($"{b.name} { b.date:MM}");
                }
            }
        }

        public void SortByOrdersSum()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().Elements().Where(c => !c.Element("orders").IsEmpty).Select(x => new
            {
                name = x.Element("name").Value,
                sum = x.Elements("orders").Elements().Elements("total").Select(y => double.Parse(y.Value.Replace(".", ","))).Sum(),
            }).ToList();
            var sortByOrdersSum = a.OrderByDescending(x => x.sum);
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersSortedByOrdersSum.txt"))
            {
                foreach (var b in sortByOrdersSum)

                {
                    file.WriteLine($"{b.name} { b.sum}");
                }
            }
        }

        public void SortByName()
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            var a = Document.Elements().Elements().Where(c => !c.Element("orders").IsEmpty).Select(x => new
            {
                name = x.Element("name").Value,
            }).ToList();
            var sortByName = a.OrderBy(x => x.name);
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersSortedByName.txt"))
            {
                foreach (var b in sortByName)

                {
                    file.WriteLine($"{b.name} ");
                }
            }
        }
    }
}

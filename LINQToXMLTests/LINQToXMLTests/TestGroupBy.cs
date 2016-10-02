using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace LINQToXMLTests
{
    [TestClass]
    public class TestGroupBy
    {

        [TestMethod]
        public void TestGroupByCountry()
        {
            GroupByCountry("country");
        }
        public void GroupByCountry(string param)
        {
            string FileName = $@"{Environment.CurrentDirectory}\Customers.xml";
            XDocument Document = XDocument.Load(FileName);
            IEnumerable<IGrouping<string, string>> query =
       Document.Elements().
       Elements().GroupBy(x => x.Element(param).
       Value, x => x.Element("name").Value);
            using (StreamWriter file = new StreamWriter($@"{Environment.CurrentDirectory}\CustomersGroupedBy{param}.txt"))
            {
                foreach (IGrouping<string, string> varGroup in query)
                {
                    file.WriteLine(varGroup.Key);
                    foreach (string name in varGroup)
                    {
                        file.WriteLine($"  {name}");
                    }
                }
            }
        }
    }
}




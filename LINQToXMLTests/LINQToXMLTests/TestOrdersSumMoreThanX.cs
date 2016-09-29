using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace LINQToXMLTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod()
        {
            //задаем путь к нашему рабочему файлу XML
            string fileName = "RD. HW - AT Lab#. 05 - Customers.xml";
            //читаем данные из файла
            XDocument doc = XDocument.Load(fileName);
            //проходим по каждому элементу в найшей library
            //(этот элемент сразу доступен через свойство doc.Root)
            foreach (XElement el in doc.Root.Elements())
            {
                //Выводим имя элемента и значение аттрибута id
                Console.WriteLine("{0} {1}", el.Name, el.Attribute("id").Value);
                Console.WriteLine("  Attributes:");
                //выводим в цикле все аттрибуты, заодно смотрим как они себя преобразуют в строку
                foreach (XAttribute attr in el.Attributes())
                    Console.WriteLine("    {0}", attr);
                Console.WriteLine("  Elements:");
                //выводим в цикле названия всех дочерних элементов и их значения
                foreach (XElement element in el.Elements())
                    Console.WriteLine("    {0}: {1}", element.Name, element.Value);
            }
        }
    }
}

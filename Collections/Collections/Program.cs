using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            ListPerfomance.WriteToFile();
            LinkedListPerfomance.WriteToFile();
            DictionaryPerfomance.WriteToFile();
            QueuePerfomance.WriteToFile();
            SortedSetPerfomance.WriteToFile();
            SortedDictionaryPerfomance.WriteToFile();
            StackPerfomance.WriteToFile();
            Console.ReadKey();

        }
    }
}

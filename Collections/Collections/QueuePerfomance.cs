using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class QueuePerfomance
    {
        public static Queue<int> TestQueue { get; set; } = new Queue<int>();

        public static void WriteToFile()
        {
            string output = string.Empty;
            output = $"{output}LinkedList adding time (1 000 000 elements) {AddingTime()}{Environment.NewLine}";
            output = $"{output}LinkedList reading time (1 000 000 elements) {ReadingTime()}{Environment.NewLine}";
            output = $"{output}LinkedList searching time (1 000 000 elements) {SearchingTime()}{Environment.NewLine}";
            output = $"{output}LinkedList removing time (100 000 elements) {RemovingTime()}{Environment.NewLine}";
            using (StreamWriter file = File.AppendText("CollectionsPerfomance.txt"))
            {
                file.WriteLine(output);
            }
        }

        public static string AddingTime()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 1000000; i++)
            {
                TestQueue.Enqueue(i);
            }
            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            return elapsedTime;
        }

        public static string ReadingTime()
        {
            Stopwatch stopWatch = new Stopwatch();
            int y;
            stopWatch.Start();
            foreach (int i in TestQueue)
            {
                y = i;
            }
            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            return elapsedTime;
        }

        public static string SearchingTime()
        {
            Stopwatch stopWatch = new Stopwatch();
            int y;
            stopWatch.Start();
            foreach (int i in TestQueue)
            {
                if (i == 12345)
                {
                    y = i;
                }
            }
            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:0000}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            return elapsedTime;
        }

        public static string RemovingTime()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 0; i < 100000; i++)
            {
                TestQueue.Dequeue();
            }
            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00000}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            return elapsedTime;
        }
    }
}


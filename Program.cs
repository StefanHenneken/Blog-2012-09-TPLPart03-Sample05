using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sample05
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }
        public void Run()
        {
            Task<int> taskM01 = Task.Factory.StartNew<int>(M01, 1); // alternativ: ((a) => M01(a), 1)
            Task<int> taskM02 = Task.Factory.StartNew<int>(M02, 2);
            Task<int> taskM03 = taskM02.ContinueWith<int>(M03);
            Task<int> taskM04 = taskM02.ContinueWith<int>(M04);
            Task[] tasks = new Task[] { taskM01, taskM03, taskM04 };
            Task<int> taskM05 = Task.Factory.ContinueWhenAll<int>(tasks, M05);

            taskM05.Wait();
            Console.WriteLine("taskM05: {0}", taskM05.Result);
            Console.ReadLine();
        }
        private int M01(object x)
        {
            Thread.Sleep(800); // Rechenzeit simulieren
            return (int)x + 1;
        }
        private int M02(object y)
        {
            Thread.Sleep(600); // Rechenzeit simulieren
            return (int)y + 2;
        }
        private int M03(Task<int> task)
        {
            Thread.Sleep(500); // Rechenzeit simulieren
            return task.Result + 3;
        }
        private int M04(Task<int> task)
        {
            Thread.Sleep(300); // Rechenzeit simulieren
            return task.Result + 4;
        }
        private int M05(Task[] tasks)
        {
            int result = 0;
            foreach (Task<int> task in tasks)
                result += task.Result;
            return result;
        }
    }
}

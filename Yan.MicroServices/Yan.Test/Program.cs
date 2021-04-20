using System;
using Yan.DelayQueue;

namespace Yan.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IDelayQueue<TestModel> queue = new SecondDelayQueue<TestModel>(10);
            queue.EnQueue(1, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "1 !" });
            queue.EnQueue(2, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "2 !" });
            queue.EnQueue(3, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "3 !" });
            queue.EnQueue(4, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "4 !" });
            queue.EnQueue(5, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "5 !" });
            queue.EnQueue(6, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "6 !" });
            queue.EnQueue(7, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "7 !" });
            queue.EnQueue(8, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "8 !" });
            queue.EnQueue(9, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "9 !" });
            queue.EnQueue(10, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "10 !" });
            queue.EnQueue(11, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "11 !" });
            queue.EnQueue(12, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "12 !" });
            queue.EnQueue(13, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "13 !" });
            queue.EnQueue(14, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "14 !" });
            queue.EnQueue(15, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "15 !" });
            queue.EnQueue(16, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "16 !" });
            queue.EnQueue(17, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "17 !" });
            queue.EnQueue(18, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "18 !" });
            queue.EnQueue(19, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "19 !" });
            queue.EnQueue(20, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "20 !" });
            queue.EnQueue(21, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "21 !" });
            queue.EnQueue(22, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "22 !" });
            queue.EnQueue(23, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "23 !" });
            queue.EnQueue(24, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "24 !" });
            queue.EnQueue(25, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "25 !" });
            queue.EnQueue(26, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "26 !" });
            queue.EnQueue(27, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "27 !" });
            queue.EnQueue(28, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "28 !" });
            queue.EnQueue(29, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "29 !" });
            queue.EnQueue(30, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "30 !" });
            queue.EnQueue(31, obj => { Console.WriteLine(obj.Info); },
                new TestModel() { Info = "31 !" });

            Console.ReadKey();

            while (true)
            {
                queue.EnQueue(5, obj => { Console.WriteLine(obj.Info); },
                    new TestModel() { Info = "5 !" });

                Console.ReadKey();
            }

        }
    }

    public class TestModel
    {
        public string Info { get; set; }
    }
}

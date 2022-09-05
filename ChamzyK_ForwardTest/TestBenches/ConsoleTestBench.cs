using System;
using System.Threading;
using ChamzyK_ForwardTest.Engines;

namespace ChamzyK_ForwardTest.TestBenches
{
    public class ConsoleTestBench : ITestBench
    {
        public IEngine Engine { get; set; }

        public double StartBench(double ambientT)
        {
            var time = 0; //время условное (модельное),
            Engine.StartEngine(ambientT);
            while (Engine.T < Engine.OverheatsT)
            {
                Thread.Sleep(250); //убрать для быстрого результата

                Engine.DoIteration(); //одна итерация - одна секунда

                Console.WriteLine($"Температура: {Engine.T}" +
                                  $"\nТемпература перегрева: {Engine.OverheatsT}\n");

                time++;
            }

            return time;
        }

        public ConsoleTestBench(IEngine engine)
        {
            Engine = engine;
        }
    }
}

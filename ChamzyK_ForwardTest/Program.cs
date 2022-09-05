using System;
using System.Globalization;
using ChamzyK_ForwardTest.Engines;
using ChamzyK_ForwardTest.TestBenches;
using Microsoft.Extensions.Configuration;


namespace ChamzyK_ForwardTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var engine = GetEngine();
            var bench = GetBench(engine);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите температуру окружающей среды: ");

                if (double.TryParse(Console.ReadLine(), out var ambientT))
                {
                    Console.WriteLine($"Перегрев наступил спустя {bench.StartBench(ambientT)} у.е. времени от начала запуска теста. (Время модельное)");
                }
                else
                {
                    Console.WriteLine("Неверный формат температуры\n");
                }
                Console.WriteLine("Нажмите любую клавишу для продолжения...\n");
                Console.ReadKey(true);
            }
        }

        private static IEngine GetEngine()
        {
            var configuration = new ConfigurationBuilder().
                AddJsonFile("engineconf.json", optional: true, reloadOnChange: true).
                Build();

            var M = configuration.GetSection("M").Get<double[]>();
            var V = configuration.GetSection("V").Get<double[]>();

            return new CombustionEngine
            {
                I = double.Parse(configuration["I"], CultureInfo.InvariantCulture),
                ArrayV = V,
                ArrayM = M,
                OverheatsT = double.Parse(configuration["OverheatsT"], CultureInfo.InvariantCulture),
                Hv = double.Parse(configuration["Hv"], CultureInfo.InvariantCulture),
                Hm = double.Parse(configuration["Hm"], CultureInfo.InvariantCulture),
                C = double.Parse(configuration["C"], CultureInfo.InvariantCulture)
            };
        }

        private static ITestBench GetBench(IEngine engine) => new ConsoleTestBench(engine);
    }
}

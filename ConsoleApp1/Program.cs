using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace ConsoleApp1
{

    internal class Program
    {

        static private int incrementarCompartida = 0;
        static private object cerrojo = new object();

        static void Main(string[] args)
        {
            Tareasytiempo();
        }

        // RETO 1
        static void Miprimerhilo()
        {
            Console.WriteLine("ID main: " + Environment.CurrentManagedThreadId);

            Thread hilo = new Thread(TextoHilo);
            hilo.Start();

            Console.WriteLine("Hello, World!");
        }

        static void TextoHilo()
        {
            Console.WriteLine("ID hilo: " + Environment.CurrentManagedThreadId);

            Console.WriteLine("1");
            Thread.Sleep(500);
            Console.WriteLine("2");
            Thread.Sleep(500);
            Console.WriteLine("3");
            Thread.Sleep(500);
            Console.WriteLine("4");
            Thread.Sleep(500);
            Console.WriteLine("5");
        }
        //

        // RETO 2
        static void Condiciondecarrera()
        {
            //Thread hilo1 = new Thread(incrementarVariable);
            //Thread hilo2 = new Thread(incrementarVariable);
            Thread hilo1 = new Thread(incrementarVariableInterlocked);
            Thread hilo2 = new Thread(incrementarVariableInterlocked);

            hilo1.Start();
            hilo2.Start();

            hilo1.Join();
            hilo2.Join();
            Console.WriteLine(incrementarCompartida);
        }

        static void incrementarVariable()
        {
            for (int i = 0; i < 100000; i++) lock (cerrojo)
            {
                incrementarCompartida++;
            }
        }

        static void incrementarVariableInterlocked()
        {
            for (int i = 0; i < 100000; i++)
            {
                Interlocked.Increment(ref incrementarCompartida);
            }
        }
        //

        // RETO 3
        static void Tareasytiempo()
        {
            Stopwatch sw = Stopwatch.StartNew();

            /*Task.Delay(1000).Wait();
            Task.Delay(2000).Wait();
            Task.Delay(3000).Wait();*/



            Task.WhenAll(Task.Delay(1000),
            Task.Delay(2000),
            Task.Delay(3000)).Wait();

            sw.Stop();

            Console.WriteLine(sw.Elapsed);

        }
        //
    }
}

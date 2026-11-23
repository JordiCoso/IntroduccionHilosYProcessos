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
            Condiciondecarrera();
        }

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
    }
}

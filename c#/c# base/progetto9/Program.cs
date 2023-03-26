using System;

namespace progetto9
{
    class Program
    {

        static void time (out int h, out int m, out int s) // funzione out, come ref ma non necessita di dichiazaione delle variabili nel main
        {
            DateTime d = DateTime.Now;
            h = d.Hour;
            m = d.Minute;
            s= d.Second;
            
        }

        static void Main(string[] args)
        {
            int hour, minute, second;
            time(out hour, out minute, out second);

            Console.WriteLine($"Ora: {hour}:{minute}:{second}");
        }
    }
}

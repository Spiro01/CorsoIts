using System;


namespace progetto2
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            i++;

            Console.WriteLine("Hello World!");
            int a = 10;
            int b = 20;
            int risultato;
            risultato = somma(a, b);
            

            // Console.WriteLine("La somma di "+a+" + "+b+" è "+risultato);
            Console.WriteLine("La somma di {0} + {1} è {2}", a, b, risultato);

        }
        static int somma(int x, int y)
        {
            return x + y;
        }

    }
}

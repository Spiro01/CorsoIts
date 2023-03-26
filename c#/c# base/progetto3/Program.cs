using System;

namespace progetto3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string nome="gobbo daniele";
            Console.WriteLine(nome);
            Console.WriteLine("La stringa è lunga {0} caratteri",nome.Length);
            Console.WriteLine($"La stringa è lunga {nome.Length} caratteri");


            */
            int k = 10;

            Console.WriteLine("Inserisci il numero da tastiera: ");
            
            k = Convert.ToInt32(Console.ReadLine());

            int j = k;
            Console.Write("[");
            while (k > 0)
            {
                if (j % 2 != 0)
                {
                    Console.Write(k);
                    if (k == j / 2 + 2)
                    {
                        Console.Write("] [{0}] [", k - 1);
                        k--;
                    }
                    else if (k != 1) Console.Write(",");
                }
                else
                {
                    Console.Write(k);
                    if (k == j / 2 + 1)
                    {
                        Console.Write("] [");
                    }
                    else if (k != 1) Console.Write(",");

                }


                k--;
            }
            Console.Write("]");

        }
    }

}

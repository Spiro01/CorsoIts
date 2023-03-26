using System;


namespace progetto4
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            int scelta = 0;
            Console.WriteLine(" 1 - Stampa dei numeri da 1 a N \n 2 - Stampa i primi n numeri dispari \n 3 - Stampa i primi n multipli del 7 \n 4 - Stampa i primi n numeri interi \n 5 - Stampa numeri random fino al 9000");
            Console.WriteLine("Inserisci la selezione:");

            scelta = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            switch (scelta)
            {

                case 1:
                    int n = 0;
                    Console.WriteLine("inserisci n: ");

                    n = Convert.ToInt32(Console.ReadLine());
                    for (int i = 1; i <= n; i++)
                    {

                        Console.Write(i + " ");
                    }
                    break;

                case 2:
                
                    Console.WriteLine("inserisci n: ");

                    n = Convert.ToInt32(Console.ReadLine());
                    for (int i = 1; i <= n; i++)
                    {

                        if (i % 2 != 0) Console.Write(i + " ");

                    }
                    break;

                case 3:
                
                    Console.WriteLine("inserisci n: ");

                    n = Convert.ToInt32(Console.ReadLine());
                    int j = 1;
                    int k = 1;
                    while (j < n+1)
                    {
                        if (k % 7 == 0)
                        {
                            Console.Write(k + " ");

                            j++;
                        }
                        k++;

                    }
                    break;

                case 4:
                
                    Console.WriteLine("inserisci n: ");

                    n = Convert.ToInt32(Console.ReadLine());
                    int y = 0;
                    for (int i = 0; i < n+1; i++)
                    {
                        y = y + i;

                    }
                    Console.Write(y);
                    break;

                case 5:
                    bool flag = true;
                    int num;
                    while (flag)
                    {


                        num = rand.Next(1, 10000);
                        Console.Write(num + " ");
                        if (num > 9000) flag = false;

                    }
                    break;
            }
        }
    }
}

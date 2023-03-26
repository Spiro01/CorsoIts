using System;

namespace progetto7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            /* int[] numbers = {1,2,3,4,5,6,7,8,9};

             for(int i=0;i<numbers.Length;i++) if(numbers[i]%2 !=0) Console.Write("{0} ",numbers[i]);
             Console.WriteLine();
             foreach(int v in numbers)if(v%2 ==0) Console.Write(" {0}",v);*/

            Console.Write("Inserisci la dimensione del vettore: ");

            int dim = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            int[] vetcas = new int[dim];

            popolavett(vetcas);
            // stampavet(vetcas);
            // Console.WriteLine("La somma degli elementi di vetcas è: " + sommaelementivett(vetcas));
            Console.WriteLine("il vettore ordinato è:");
            ordinavet(vetcas, 0);
            stampavet(vetcas);


        }

        private static void popolavett(int[] vetcas)
        {

            Random ran = new Random();

            for (int i = 0; i < vetcas.Length; i++) vetcas[i] = ran.Next(0, 100);

        }

        private static void stampavet(int[] vetcas)
        {

            foreach (int k in vetcas) Console.Write("{0} ", k);
            Console.WriteLine();

        }

        private static int sommaelementivett(int[] vetcas)
        {
            int somma = 0;
            foreach (int c in vetcas)
            {

                somma = somma + c;
            }

            return somma;
        }

        private static void ordinavet(int[] vetcas, int cd)
        {

            int temp = 0;

            if (cd == 0)
            {
                for (int i = 0; i < vetcas.Length; i++)
                {
                    for (int k = 0; k < vetcas.Length - 1; k++)
                    {
                        if (vetcas[k] > vetcas[k + 1])
                        {
                            temp = vetcas[k + 1];
                            vetcas[k + 1] = vetcas[k];
                            vetcas[k] = temp;
                        }
                    }




                }
            }
            else
            {

                for (int i = 0; i < vetcas.Length; i++)
                {
                    for (int k = 0; k < vetcas.Length - 1; k++)
                    {
                        if (vetcas[k] < vetcas[k + 1])
                        {
                            temp = vetcas[k + 1];
                            vetcas[k + 1] = vetcas[k];
                            vetcas[k] = temp;
                        }
                    }
                
                

                }




            }

        }
    }



   
}   
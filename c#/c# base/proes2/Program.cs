using System;

namespace proes2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] val = new int[110];

            for (int i = 0; i < val.Length; i++)
            {


                val[i] = i;

            
            }

            stampa_primi(val);



        }

        public static void stampa_primi(int[]x)

        {
            foreach (int i in x) if (i%2!=0 & i%3!=0 & i%5!=0 & i%7!=0) Console.WriteLine(i+" ");

        }
    }
}

    


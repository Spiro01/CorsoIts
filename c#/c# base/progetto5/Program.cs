using System;


namespace progetto5
{
    class Program
    {
        static void Main(string[] args)
        {
            string frase = "null";
            int conta_a = 0;
            int conta_A = 0;
            int conta_vocali = 0;
            int conta_consonanti=0;
            int conta_punt =0;
            int conta_numeri=0;
            int conta=0;
            frase = Console.ReadLine();
            /*  for(int i =0; i<frase.Length;i++){
              if(frase[i] == 'a') conta_a++;
              if(frase[i] == 'A') conta_a++;
              }
             */

            foreach (char c in frase){

                if(frase.IndexOf(c)>0)conta_vocali++;

            }

            /*
            conta_a = frase.Length - frase.Replace("a", "").Length;
            conta_A = frase.Length - frase.Replace("A", "").Length;
            */
            
            Console.WriteLine("Ci sono {0} vocali e {1} A", conta_vocali, conta_A);
        }
    }
}

using System;

namespace proes1
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] num = new int[1000];
            
            popolaarray(num);

            
            
            Console.WriteLine("La media vale: "+media(num));
            
        }

        public static void popolaarray (int[] a){
            int i = 0;
            string buffer = null;
            Console.WriteLine("Inserisci un numero o fine");
            do{
                buffer = Console.ReadLine();
                if(!buffer.Contains("fine")){
                a[i] =  Convert.ToInt32(buffer);

                i++;}
            }while (!buffer.Contains("fine"));

            Array.Resize(ref a,i);




        }


        public static int media (int[] x){
            int somma =0;
            foreach(int c in x){
                somma = somma +c ;

            }

            return (somma/x.Length);
        }
        
    }
}

using System;


namespace progetto_1
{
    class Program
    {
        static void Main(string[] args)
        {
            

            int n1=10; //n1
            int n2=2;
            int n3=0;
            int n4=0;


            Console.Clear();
            Console.WriteLine("inserisci n1:");   
            n1=Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("inserisci n2:");
            n2=Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(n1);


           
            n3=n1%n2;
            while(n3!=0){
                n1=n2;
                n2=n3;
                n3=n1%n2;
                n4=n1/n2;
            System.Console.WriteLine(n1+" = "+n2+" * "+n4+" + "+n3);

            }

           
            
            
            System.Console.WriteLine(n2+" ");
        }
    }
}

using System;

namespace mcm
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int n1=21333456;
            int n2=54784771;
            int n3=0;
            int n4=0;
            
            if(n1<n2){

               n3=n1;
               n1=n2;
               n2=n3;
            }
            n3=n1;


            while(n3%n2!=0){

                n3=n3+n1;
               // n4=n3%n2;
                //Console.WriteLine(n3);

            }


            Console.WriteLine(""+n3);
        }
    }
}

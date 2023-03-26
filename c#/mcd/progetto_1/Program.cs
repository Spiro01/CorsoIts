using System;


namespace progetto_1
{
    class Program
    {
        static void Main(string[] args)
        {
            

            int n1=100; //n1
            int n2=3;
            int n3=0;
            int n4=0;


           if(n1<n2){

               n3=n1;
               n1=n2;
               n2=n3;
            }
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

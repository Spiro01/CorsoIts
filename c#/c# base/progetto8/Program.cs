using System;

namespace progetto8
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMath m = new MyMath();
            MethodInvocationnamespace.execcube();
            MethodInvocationnamespace.execsqrt();


        }
    }

    class MyMath
    {

        public static long cube(long number)
        {

            long risultato = 0;
            risultato = number * number * number;
            return risultato;

        }

        public double sqrt(double number)
        {

            double risultato;
            risultato = number;
            return Math.Sqrt(risultato);

        }
    }


    class MethodInvocationnamespace
    {
        public static void execcube()
        {

            long numero = 0;
            Console.Write("Inserisci un numero tra -1000 e 1000 di cui calcolare il numero");
            numero = Convert.ToInt64(Console.ReadLine());
            while (numero < -1000 || numero > 1000)
            {
                Console.Clear();
                Console.Write("Numero inserito sbagliato");
                Console.Write("Inserisci un numero tra -1000 e 1000 di cui calcolare il numero");
                numero = Convert.ToInt64(Console.ReadLine());
            }
            Console.Clear();
            Console.Write($"Il cubo del numero {numero} è: {MyMath.cube(numero)}");
        }

        public static void execsqrt()
        {
            double number;
            Console.Write("Inserisci un numero tra 0 e 1000 di cui calcolare la radquad: ");
            number = Convert.ToDouble(Console.ReadLine());
            while (number<0 || number>1000){
                Console.Clear();
                Console.Write("Inserisci un numero tra 0 e 1000 di cui calcolare la radquad: ");
                number = Convert.ToDouble(Console.ReadLine());
            }
            MyMath k = new MyMath();
            Console.Write($"La radice quadrata di number è: {k.sqrt(number)}");
        }
        
    }




}

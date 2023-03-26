using System;

namespace es1
{
    class Program
    {
        static void Main(string[] args)
        {
            string ins;
            int l = 0;
            int num = 0;
            string ris = System.String.Empty;
            
            {
                 
            }


            Console.Clear();
            Console.WriteLine("Inserisci il tipo di conversione: ");
           



            if (Convert.ToInt32(Console.ReadLine()) == 0)
            {
                Console.Clear();
                Console.WriteLine("Inserisci il numero in binario: ");

                ins = Console.ReadLine();
                if (ins.Contains('0') && ins.Contains('1') || ins.Length == 0)
                {
                    l = ins.Length - 1;
                    for (int i = 0; i < ins.Length; i++)
                    {


                        num = (Convert.ToInt32(ins[l - i]) - 48) * Convert.ToInt32(Math.Pow(2, i)) + num;

                    }
                    Console.Clear();

                    Console.WriteLine("La conversione di {0} è {1}", ins, num);
                }
                else Console.WriteLine("Inserisci correttamente un numero binario");


                



            }
            else
            {

                Console.Clear();
                Console.WriteLine("Inserisci il numero decimale: ");

                ins = Console.ReadLine();
                if (true)
                {
                    l = ins.Length - 1;
                    int rem = Convert.ToInt32(ins);
                    Console.WriteLine(rem);
                    Console.Clear();
                    int con = 0;
                    while (rem > 0)
                    {
                        {

                            if (rem % 2 == 0)
                            {


                                ris = ris + "0";

                            }
                            if (rem % 2 == 1)
                            {

                                ris = ris + "1";
                            }


                            rem = rem / 2;
                            con++;

                        }
                    }

                    Console.Clear();
                    Console.WriteLine("La conversione di {0} è {1}", ins, Reverse(ris));

                 




                } /*else {Console.Clear(); 
                    Console.WriteLine ("Inserisci un numero decimale valido");}*/
            }
            Console.ReadLine();
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }



}

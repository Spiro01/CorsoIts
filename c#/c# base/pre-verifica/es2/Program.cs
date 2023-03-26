using System;

namespace es2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            string s = null;

            Console.Write("Inserisci la frase di cui fare l'acronimo ====> ");
            s = Console.ReadLine();
           
            Console.WriteLine(getAcronimo(s));

        }
        static string getAcronimo(string st)
        {
            string a = null;
            char[] b = new char [10];
            string[] c = new string[10];
            st = st.ToUpper();
            a = st.TrimEnd();
            a = System.Text.RegularExpressions.Regex.Replace(a, @"_-,", " ");
            
           
           c =  a.Split(' ',10);
            
            for (int i = 0; i < c.Length; i++){

                b[i] = c[i][0];
                

            }
             
            
          
           

            return new string (b);
        }
        

    }
}

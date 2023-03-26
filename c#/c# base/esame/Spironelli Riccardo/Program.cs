using System;
using System.Text;
namespace Spironelli_Riccardo
{
    class Program
    {
        static void Main(string[] args)
        {
            int scelta = 0;
            bool flag = true;


            do
            {

                Console.WriteLine("1----Tiro con l'arco");
                Console.WriteLine("2----Acronimo");
                Console.WriteLine("3---- Per uscire");
                scelta = InsInt("Inserisci la tua scelta:");

                switch (scelta)
                {

                    case 1: //Arcere

                        Console.WriteLine("Il punteggio vale: " + Gara());
                        Console.WriteLine("Premi un qualsiasi tasto per contnuare");
                        Console.ReadKey(true);
                        break;
                    case 2: //Acronimo

                        Console.Clear();

                        string s = null;

                        Console.Write("Inserisci la frase di cui fare l'acronimo ====> ");
                        s = Console.ReadLine();
                        Console.WriteLine(GetAcronimo(s));
                        Console.WriteLine("Premi un qualsiasi tasto per contnuare");
                        Console.ReadKey(true);
                        break;
                    default: flag = false; break;
                }
                Console.Clear();
            } while (flag); //ciclo finchè l'utente non decide di uscire dal programma
        }







        static int InsInt(string s) //metodo per controllare che il valore inserito sia un intero
        {
            int n = 0;

            
            do
            {
                Console.Write(s);

            } while (!int.TryParse(Console.ReadLine(), out n));

            return n;

        }

        static int Punteggio(double x, double y)
        {
            double dc = 0; //distanza centro

            dc = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)); //calcolo la distanza dal centro usando la formula della distanza tra due punti

            if (dc <= 2) return 10; //Calcolo il punteggio usando degli if annidati
            else if (dc > 2 || dc <= 3) return 9;
            else if (dc > 4 || dc <= 5) return 8;
            else if (dc > 6 || dc <= 7) return 7;
            else if (dc > 8 || dc <= 9) return 6;
            else if (dc > 10 || dc <= 11) return 5;
            else if (dc > 12 || dc <= 13) return 4;
            else if (dc > 14 || dc <= 15) return 3;
            else if (dc > 16 || dc <= 17) return 2;
            else if (dc > 18 || dc <= 20) return 1;
            else return 0;



        }

        static int Gara()
        {
            int somma = 0;
            
            Random r = new Random(); //Inizializzo l'oggetto r della classe random
        
            for (int i = 0; i < 3; i++) somma = Punteggio(r.NextDouble() * r.Next(-30, 30), r.NextDouble() * r.Next(-30, 30)) + somma; //genero una x e y random per 60 volte, assegno il punteggio e aggiungo alla somma totale
            
            
            return somma;
        }

        static string GetAcronimo(string st) //1^ metodo
        {
            string a = null;
            char[] b = new char[10];
            string[] c = new string[10];
            st = st.ToUpper(); //tutto maiuscolo

            
            a = System.Text.RegularExpressions.Regex.Replace(a, @"_ -,", " "); //sostituisco i caratteri speciali con uno spazio
            a = System.Text.RegularExpressions.Regex.Replace(st, " {2,}", " "); //riumuovo i doppi spazi


            c = a.Split(' ', 10); //separo la stringa in sottostrighe nel momento in cui trovo uno spazio

            for (int i = 0; i < c.Length; i++)
            {

                b[i] = c[i][0]; //inserisco nell'array b le iniziali delle sottostringhe


            }

            return new string(b); //converto b in stringa e invio
        }

        public static string GetAcronimo2(string st) //2^ metodo (più efficiente in teoria)
        {

            StringBuilder s = new StringBuilder(); // uso la classe String Builder per motivi di performance
            char[] li = { ' ', '-', '_' }; //lsita indesiderati
            st = st.ToUpper(); //converto tutta la stringa in maiuscolo
            st = System.Text.RegularExpressions.Regex.Replace(st, " {2,}", " "); //uso il metodo replace della classe regex per rimuovere tutti i doppi spazi
            string[] s1 = st.Split(li); //separo la stringa quando trovo un "carattere separatore" e lo butto in una sottostringa
            for (int i = 0; i < s1.Length; i++)
            {
                s.Append(s1[i][0]); //metto il primo carattere delle sottostringhe s1 creando così l'acronimo
            }
            return s.ToString(); //converto s in stringa e

        }
    }
}

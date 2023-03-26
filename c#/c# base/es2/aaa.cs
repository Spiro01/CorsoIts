using System;
namespace circ_nopi
{
    public class circ
    {   
            
        static int p = 0;
        public  circ (int p1){p=p1;
        Array.Resize<double>(ref x, p);
        Array.Resize<double>(ref y, p);
        }
    

        private static double r;
        public static double[] x = new double[p];
        public static double[] y = new double[p];
        private static double c;

        public  void getint()
        {

            r =  Convert.ToDouble(Console.ReadLine());

        }

        public  void calc_form()
        {

            

            for (int i = 0; i < y.Length; i++)
            {
                y[i] = Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x[i], 2));

            }


            
        }


        public  void fill_array()
        {
            int cont = x.Length / 2;

            for (int i = 0; i < x.Length / 2; i++)
            {

                x[i] = -(double)2 * cont * r / x.Length;
                cont--;
            }

            cont = 0;
            for (int i = x.Length / 2; i < x.Length; i++)
            {



                x[i] = (double)2 * cont * r / x.Length;

                cont++;

            }
            x[x.Length-1] = r;

            
        }


        public  void calc_circ()
        {

            double sum = 0;
            for (int i = 0; i < x.Length - 1; i++)
            {

                sum = Math.Sqrt(Math.Pow((x[i + 1] - x[i]), 2) + Math.Pow((y[i + 1] - y[i]), 2)) + sum;
            }

            c =  (sum * 2d);

        }

        public  void stampa_punti(double[] x, double[] y)
        {
                Console.Clear();
                Console.WriteLine("x y");

            for (int i = 0; i < y.Length; i++)
            {
                
                Console.Write(x[i] + " ");
                Console.WriteLine(y[i]);

            }

        }
        public  void trova_errore (){

            double circ = 0f;
            double err = 0f;
        circ = (2*Math.PI*r);

            err= Math.Abs((c-circ)/circ)*100;
        Console.WriteLine("La circ calcolata senza PI vale : " + c);
        Console.WriteLine("La circ calcolata con PI vale:    "+circ);
        
        Console.WriteLine("L'errore di questo calolo è stato del :" +err+"%");
        

        }
    }
}
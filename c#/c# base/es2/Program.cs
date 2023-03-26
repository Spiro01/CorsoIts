//x^2 + y^2 + ax + by + c = 0;
//y^2= -x^2 -ax-by-c;
//y= -sqrt(r^2-x^2)
//y= sqrt(r^2-x^2)


using System;
using System.ComponentModel.DataAnnotations;
using circ_nopi;
namespace es2
{
    
    class Program
    {

        

        static void Main(string[] args)
        {
            //******Dichiarazione variabili************
            
            circ c = new circ(10);
            
            //Chiedo il raggio della circonferenza in input
            Console.Clear();
            Console.WriteLine("Inserisci il raggio della circonferenza:");
            c.getint();


            c.fill_array(); //Riempo un array di x va lori con numeri da -r a r

            c.calc_form(); //Estraggo i punti della semicirconferenza

            
            
            

            c.calc_circ(); //calcolo la circonferenza con la formula della distanza tra due punti

            //Stampo a video il risulatato
            Console.Clear();
            
            c.trova_errore();
            
            //stampa_punti(x,y);


            


        }

        
        

    }

}


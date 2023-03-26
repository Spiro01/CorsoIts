using System;

namespace es1
{
    class Program
    {
        static void Main(string[] args)
        {

            
             Console.Clear();
            int x,y = 0;
            do{
            Console.Write("Inserisci x ===> ");
            }while(!Insint(out x)); //Chiedo in input x finchè l'utente non inserisce un intero
            
           

            do{
            Console.Write("Inserisci y ===> ");
            }while(!Insint(out y));  //Chiedo in input y finchè l'utente non inserisce un intero
            
            Console.Clear();

            Console.WriteLine($"Il punteggio con x = {x} e y = {y} vale : {Punteggio(x,y)}"); //Stampo il risultato


            Console.WriteLine(InsInt("Inserisci x =====> "));

        

        }
       
    

    static int Punteggio (int x, int y){

        int dc =0;

        dc = Convert.ToInt32(Math.Sqrt(Math.Pow(x,2)+Math.Pow(y,2))); //calcolo la distanza dal centro usando la formula della distanza tra due punti
        //Console.WriteLine("Distanza = "+ dc); //Linea di debug
        //Assegno un punteggio basato sulla distanza dal centro
        if(dc<=10) return 10;
        else if(dc<=20 && dc > 10) return 5;
        else if(dc<=30 && dc > 20) return 1;
        else return 0;        
        
        
        
    }

//Controllo che il valore inserito sia un intero e in caso lo salvo nella variabile passata per rifermineto x
    static bool Insint (out int x){
         
        return int.TryParse(Console.ReadLine(),out x) ;
    
    }

    static int InsInt (string s){
        int n = 0;

        Console.Clear();
        do{
        Console.Write(s);
        
        }while(!int.TryParse(Console.ReadLine(),out n));

        return n;
    }

}

}
using System;

namespace progetto_10
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine($"Il mese di marzo ha: {GiorniMese(3,2020)} giorni");

           for (int anno =1900;anno<=2105;anno++) if (GiorniMese2(2,anno)==29) Console.WriteLine ($"L'anno {anno} è bisestile");
    
        }

        static int GiorniMese (int mese, int anno){

            if(mese ==1 || mese ==3 || mese ==5 || mese ==7 || mese ==8 || mese ==10 || mese ==12) return 31;
            else if (mese ==4 || mese ==6 || mese ==9 || mese ==11) return 30;
            else return (28 + (anno%400 == 0 || (anno%4 == 0 && anno%100!=0)? 1:0));
            
        }

         static int GiorniMese1 (int mese, int anno){
             switch(mese){
                 case 4: case 6: case 9: case 11: return 30;
                 case 2: return (28 + (anno%400 == 0 || (anno%4 == 0 && anno%100!=0)? 1:0));
                 default: return 31;

             }}
          static int GiorniMese2 (int mese, int anno){
             
             int[] giorni = {31,28,31,30,31,30,31,31,30,31,30,31};

             return (giorni[mese-1]+(mese==2 &&(anno%400 == 0 || (anno%4 == 0 && anno%100!=0))?1:0 ));

             }

         static bool DataCorretta (int g,int m,int a){
            return (a>0 && m>=1 && m>=12 && g>0 &&g<=GiorniMese2(m,a));
         }

        static int DifferenzaDateInGiorni (int g1,int m1,int a1, int g2,int m2,int a2){
            int differenza = 0;
            if(m1==m2 && a1==a2) return (Math.Abs(g2-g1));
            differenza = GiorniMese2(m1,a1)-g1;
            for (int mese = m1+1; mese<m2;mese++) differenza += GiorniMese2(mese,a1);
            return differenza+=g2;


        }

        static int LeggiIntero (string messaggio){
            bool esito = false;
            int n=0; //variabile che conterrà l'intero letto

            do{
                
                Console.Write (messaggio+"-->");
                if(!(esito = int.TryParse(Console.ReadLine(),out n))){
                    Console.WriteLine("Non è un numero intero, riprova... premi un tasto per continuare");
                    Console.ReadKey(true);
                    

                }
                 
            }while(!esito);


            return (n);
        }
    }

    

}

using System;
using System.Collections.Generic;
using Spectre.Console;

namespace simulazione
{
    public class Parcheggio
    {
        public List <string> targa = new List<string>();
        public int pdi { get; set; } //posti disponibili

        public List <string> ingresso = new List<string> ();
        public List <string> uscita = new List<string> ();

        public List <string> storico = new List<string>();
    public Parcheggio(int cap){
        pdi = cap;

    }

    

    }

    public class gui{

        
        
        
        public static void main_menu(ref Parcheggio p){
        string scelta ="";
        Console.WriteLine("Posti disponibili: "+p.pdi);

        
        string[] scelte = {"Entra","Esci","Admin"};
        string[] sceltea = {"Esci","Admin"};
        string[] sc = sceltea;
        if(p.pdi >0){sc = scelte;}
        
        
         scelta = AnsiConsole.Prompt(
         new SelectionPrompt<string>()
        .Title("***Menu Parcheggio***")
        
        .AddChoices(sc));
        
         
        
        if(scelta == "Entra"){
            string ins = Console.ReadLine();
            if(!p.targa.Contains(ins)){p.targa.Add(ins);
            p.ingresso.Add(GetTimestamp(DateTime.Now));
            p.storico.Add(ins);
            
            p.pdi--;}
            else{ Console.WriteLine("Targa già presente");
            Console.ReadKey();}
        }
        
        
        else if (scelta == "Esci"){
            Console.WriteLine("Inserisci la targa in uscita:");
                string ins = Console.ReadLine();
             try{  
            p.targa.RemoveAt(p.targa.IndexOf(ins));
            p.pdi++;
            int index = p.storico.IndexOf(ins);
            for(int i = p.uscita.Count ; i<=index;i++){
            p.uscita.Add("");
            }
          
           p.uscita[index] = GetTimestamp(DateTime.Now);
            
             }
             catch  {Console.WriteLine("la targa non esiste!");
             Console.ReadKey();} 
            





        }else{
            
            Console.WriteLine("Inserisci la password: ");
            if(password()=="cisco"){

            Console.Clear();

             // Create a table
            var table = new Table();

            // Add some columns
            
            table.AddColumn(new TableColumn("TARGA").Centered());
            table.AddColumn(new TableColumn("ENTRATA").Centered());
            table.AddColumn(new TableColumn("USCITA").Centered());
            //Console.WriteLine(p.storico.Count);
            for(int i = p.uscita.Count ; i<p.ingresso.Count;i++){
                p.uscita.Add("");

            }
            for(int i = 0 ;i<p.storico.Count ;i++){
                
                table.AddRow(p.storico[i],p.ingresso[i],p.uscita[i]);
            
            }
            AnsiConsole.Write(table); }
            Console.ReadKey();
            
        }
        Console.Clear();
        }

        public static string password()
        {

            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }

        public static String GetTimestamp(DateTime value)
{
        return value.ToString("dd/MM/yyyy HH:mm:ss");
}
    }



    
}
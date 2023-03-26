using System;
using System.Collections.Generic;
using Spectre.Console;



namespace simulazione_cper
{
    public class ristorante
    {

        private int tavoli;
        private List <ordine> ordini = new List<ordine>();
        
        public ristorante (int tavoli){
            this.tavoli = tavoli;
         }
    


        public void menu (){
        Console.Clear();
        string scelta ="";
        

        
        string[] scelte = {"Nuovo ordine","Mostra comande"};
        
        
        
         scelta = AnsiConsole.Prompt(
         new SelectionPrompt<string>()
        .Title($"***Menu Ristorante***")
        
        .AddChoices(scelte));
        

        if(scelta == "Nuovo ordine"){
            Console.WriteLine("Inserisci il tavolo");
            ordine ord = new ordine();
            int tav = 0;
            while(!Int32.TryParse(Console.ReadLine(),out tav)); 
            bool flag = true;
            foreach(ordine o in ordini){if(o.tavolo == tav) flag = false;}
            if(flag && tav<tavoli && tav>0 ){
            ord.tavolo = tav;

            
            ord.ins();
            ordini.Add(ord);
            }else {Console.WriteLine("Controlla il numero del tavolo");
                    Console.ReadKey();}

        } else{
            if(ordini.Count > 0){
                List <string> sm = new List<string>();
                for(int i = 0 ; i<ordini.Count ; i++){
                    
                    sm.Add($"{i+1}: Tavolo {ordini[i].tavolo} ({GetTime(ordini[i].ora)})");
                }
                sm.Add("Esci");

            scelta = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title($"***Menu Ristorante***")
        
            .AddChoices(sm.ToArray())); 


            if(scelta!="Esci"){
            ordine sel = new ordine();
            sel = ordini[sm.IndexOf(scelta)];
            sm.Clear();
            
            Console.WriteLine(scelta);
            
             var table = new Table();

            // Add some columns
            
            table.AddColumn(new TableColumn("QTA").Centered());
            table.AddColumn(new TableColumn("Descrizione").Centered());
            
            //Console.WriteLine(p.storico.Count);
            
            for(int i = 0 ;i<sel.ordini.Count ;i++){
                
                table.AddRow(""+sel.qta[i],sel.ordini[i]);
            
            }
            AnsiConsole.Write(table); 

            Console.WriteLine("Completato? [y/n] (n)");
            if(Console.ReadKey().Key == ConsoleKey.Y){
                ordini.Remove(sel);
            }
            
            Console.ReadKey();

            }

           } else {Console.WriteLine("Nessun ordine presente al momento...");
            Console.ReadKey();
            }



        }

        
        
        
        }

        public static string GetTime(DateTime value)
{
        return value.ToString("hh:mm:ss");
}
}

    public class ordine{
        public List <string>ordini = new List<string>();
        public DateTime ora;
        public int tavolo;
        public List <int> qta = new List<int>();

        
        public void ins(){
            string s = "";
            Console.WriteLine($"Tavolo: {tavolo}");
            Console.WriteLine("Inserisci gli ordini: ");
            do{
            
            Console.Write("=>");
            s = Console.ReadLine();
            if(s!=""){
                
                if(!ordini.Contains(s)){
                    ordini.Add(s);
                    qta.Add(1);
                    }
                else{qta[ordini.IndexOf(s)]++;}
                
                }
            }while(s != "");
            ora = DateTime.Now;
        }
        

    }



}
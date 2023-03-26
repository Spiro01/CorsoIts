
using System;
using System.Collections.Generic;
using Spectre.Console;

namespace prova_esame
{
    public class Partita
    {
        
    private List <string> storico = new List<string>();    
    private int casa;
    private int ospiti;
    private List <TimeSpan> tempo = new List<TimeSpan>();
    private bool flag = true;
     private DateTime inizio;  



        public Partita (){

            inizio = DateTime.Now;



        }




        public bool gui(){
        string scelta ="";
        

        
        string[] scelte = {"Goal casa","Goal ospiti","Fine partita"};
        
        Console.WriteLine($"Casa {casa} - {ospiti} Ospiti");
        
         scelta = AnsiConsole.Prompt(
         new SelectionPrompt<string>()
        .Title($"***Menu Partita***")
        
        .AddChoices(scelte));
        
         
        if(scelta == "Goal casa"){

            Console.WriteLine($"Goal casa !!!");
            casa++;
            storico.Add($"{casa} - {ospiti}");
            DateTime adesso = DateTime.Now;
            tempo.Add(adesso.Subtract(inizio));
            return true;

        }
        else if (scelta == "Goal ospiti"){

            Console.WriteLine($"Goal ospiti !!!");
            ospiti++;
            storico.Add($"{casa} - {ospiti}");
            DateTime adesso = DateTime.Now;
            tempo.Add(adesso.Subtract(inizio));
            return true;

        }

        else{
            Console.WriteLine("Ecco il resoconto della partita:");
             var table = new Table();

            // Add some columns
            
            table.AddColumn(new TableColumn("PUNTEGGIO").Centered());
            table.AddColumn(new TableColumn("ORARIO").Centered());
            
            //Console.WriteLine(p.storico.Count);
            
            for(int i = 0 ;i<storico.Count ;i++){
                
                table.AddRow(storico[i],GetTimestamp(tempo[i]));
            
            }
            AnsiConsole.Write(table); 
            Console.ReadKey();

            return false;
        }
            
        }

        

        public static String GetTimestamp(TimeSpan value)
{
        return value.ToString("mm':'ss");
}
    }
}
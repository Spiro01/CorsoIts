using System;

namespace simulazione
{
    class Program
    {
        static void Main(string[] args)
        {
            Parcheggio park = new Parcheggio(20);
            while(true){
            gui.main_menu(ref park);
            }
        }
    }
}

using System;

namespace simulazione_cper
{
    class Program
    {
        static void Main(string[] args)
        {
            ristorante ris = new ristorante(10);
            //!! Il database non funziona se connessi alla rete ITSLAB !!
            while(true)ris.menu();
        }
    }
}

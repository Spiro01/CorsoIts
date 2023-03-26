using System;
using System.Collections.Generic;
using Spectre.Console;

namespace esame_c_
{
    class Program
    {
        static void Main(string[] args)
        {
            ristorante ris = new ristorante(10);
            while(true) ris.menu();
        }
    }
}

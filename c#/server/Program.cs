using System;

namespace tcp_socket // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            ser c = new ser(1111);

           c.Listen();
            
            //c.SendMessage("hey gay");
        }
    }
}
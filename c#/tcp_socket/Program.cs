using System;

namespace tcp_socket // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            cli c = new cli("127.0.0.1",1111);
            c.Open();
            c.SendMessage("uno");
            
            c.SendFile("test.txt");
            c.Close();
            //c.SendMessage("hey gay");
        }
    }
}
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcp_socket
{
    public class ser
    {
        private Socket listener;
        private IPEndPoint localEndPoint;
        public ser (int port){
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        localEndPoint = new IPEndPoint(ipAddress, port); 
        listener = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp ); 
        Console.WriteLine (localEndPoint.ToString());
        }

        public void Listen (){
             Socket handler = null;
            try{

            listener.Bind(localEndPoint);  
            listener.Listen(10); 
            Console.WriteLine ("Waiting for a connection...");
            handler = listener.Accept();
            while(true){
            
            string data = null;
            byte[] bytes = new Byte[1024]; 
            while (true) {  
                    int bytesRec = handler.Receive(bytes);  
                    data += Encoding.ASCII.GetString(bytes,0,bytesRec);  
                    if (data.IndexOf("<ﯻﯽ>") > -1) {  
                        break;  
                    }  
                }
                data = data.Remove(data.Length-3);
                if (data.Contains("<ﯻ>")){
                data = data.Replace("<ﯻ>","");
                
                }
                Console.WriteLine( "Text received : {0}", data);
                
             
        }
        
        }catch(SocketException e) {Console.WriteLine(e);}
        finally{
            handler.Close();
            listener.Close();
            listener.Shutdown(SocketShutdown.Both);
            }
    
}}}
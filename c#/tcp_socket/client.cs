using System;
using System.Collections; 
using System.Collections.Generic; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 


namespace tcp_socket
{
    public class cli
    {
        private TcpClient client;
        private  Socket sender;
        private IPEndPoint remoteEP;
        private IPAddress ipAddress;
        public cli(string server, int port){
            try{

            ipAddress = IPAddress.Parse(server);
            remoteEP = new IPEndPoint(ipAddress,port);
             

            }catch (Exception e) {Console.WriteLine("Errore nel stabilire una connessione:" + e);}
        }

        public void Open(){
            try{
            sender = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp ); 
            sender.Connect(remoteEP);
            Console.WriteLine("Socket connected to {0}",sender.RemoteEndPoint.ToString()); 
            }catch (SocketException e) {Console.WriteLine("Errore di connessione");}
        }

        public void Close(){
            sender.Shutdown(SocketShutdown.Both);  
            sender.Close();
        }

        public void SendMessage(string message){
            try{
                
                
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message+"<ﯻﯽ>");
            int bytesSent = sender.Send(data); 
            Console.WriteLine("Sent: {0}", message);

            }catch (Exception e) {Console.WriteLine("Errore nell'invio del messaggio:" + e);}
            
            finally{}
            
            }

            public void SendFile (string path){
                try{
                    FileInfo fileinfo = new FileInfo(path);
                    Byte[] pb = System.Text.Encoding.ASCII.GetBytes("<EOF>");
                    Byte[] prb = System.Text.Encoding.ASCII.GetBytes("<ﯻ> "+$"!:{fileinfo.Length}:!");
                    
                    
                    sender.SendFile(path,prb,pb,TransmitFileOptions.UseDefaultWorkerThread);

                }catch (Exception e){Console.WriteLine(e);}


            }
        }

    }
    

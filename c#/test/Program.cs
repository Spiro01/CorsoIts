using System;
using SimpleTCP;
using System.Net;
using System.Text;
using System.Threading;


var client = new SimpleTcpClient().Connect("192.168.137.243", 15000);
client.Delimiter = 0x13;
 //client.StringEncoder = Encoding.UTF8;
TimeSpan timeout = new TimeSpan (0,0,2);
client.DelimiterDataReceived+= ricevi;
//while(client.WriteLineAndGetReply("100",timeout).MessageString != "ok");



while(true){Console.WriteLine(client.WriteLineAndGetReply("C1 00 03",timeout).MessageString);
Thread.Sleep(1);
}



 void ricevi (object sender, Message e){

    Console.WriteLine( "Messaggio" + e.MessageString);
 }
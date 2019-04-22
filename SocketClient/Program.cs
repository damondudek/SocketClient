using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjetoEnvio objetoEnvio = new ObjetoEnvio()
            {
                Id = 10,
                Name = "Envio de informação" 
            };

            var jsonString  = JsonConvert.SerializeObject(objetoEnvio);

            Encoding EncodingIso = Encoding.GetEncoding("ISO-8859-1");
            byte[] bytesSocket = new byte[10000];

            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.197"), 8081);
            
            
            Socket socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEndPoint);

            socket.Send(EncodingIso.GetBytes(jsonString));

            Console.WriteLine("Envio -> " + jsonString);
                
            int bytes = socket.Receive(bytesSocket, bytesSocket.Length, 0);

            var encodeRetorno = EncodingIso.GetString(bytesSocket, 0, bytes);
            var jsonObjectRetorno = JsonConvert.DeserializeObject<ObjetoRetorno>(encodeRetorno);
            Console.WriteLine("Retorno -> " + encodeRetorno);
            
            Console.ReadKey();
        }
    }
}

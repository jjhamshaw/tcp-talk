using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        static void Main()
        {
            try
            {
                var ipAddress = "172.20.83.18";
                const int port = 8001;

                var tcpClient = new TcpClient();
                Console.WriteLine("Connecting...");
                
                tcpClient.Connect(ipAddress, port);

                Console.WriteLine("Connected");
                Console.Write("Enter the string to be transmitted : ");

                var str = Console.ReadLine();
                Stream stream = tcpClient.GetStream();

                var asciiEncoding = new ASCIIEncoding();
                var bytes = asciiEncoding.GetBytes(str);
                Console.WriteLine("Transmitting...");

                stream.Write(bytes, 0, bytes.Length);

                var bytes2 = new byte[100];
                var k = stream.Read(bytes2, 0, 100);

                for (var i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(bytes2[i]));
                }

                tcpClient.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error......." + e.StackTrace);
            }
        }
    }
}

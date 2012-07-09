using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Server
    {
        private static void Main()
        {
            try
            {
                var ipAddress = IPAddress.Parse("172.20.83.18");
                const int port = 8001;

                //Initialises the listener
                var listener = new TcpListener(ipAddress, port);

                //Start listening to the specified port
                listener.Start();

                Console.WriteLine("The server is running at port {0}...", port);
                Console.WriteLine("The local endpoint is : {0}", listener.LocalEndpoint);
                Console.WriteLine("Waiting for a connection...");

                Socket socket = listener.AcceptSocket();
                Console.WriteLine("Connection accepted from {0}", socket.RemoteEndPoint);

                var bytes = new byte[100];
                var k = socket.Receive(bytes);
                Console.WriteLine("Received...");
                for (int i = 0; i < k; i++)
                {
                    Console.Write(Convert.ToChar(bytes[i]));
                }

                var asciiEncoding =  new ASCIIEncoding();
                socket.Send(asciiEncoding.GetBytes("The string was received by the server."));
                Console.WriteLine("\nSent Acknowledgement");

                //Clean up
                socket.Close();
                listener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error.......!" + e.StackTrace);
            }
        }
    }
}
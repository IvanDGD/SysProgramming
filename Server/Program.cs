using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Server started");

        TcpClient client = listener.AcceptTcpClient();
        Console.WriteLine("Client accepted.");

        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if (bytesRead == 0) break;

            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Clients message: " + message);
        }

        client.Close();
        listener.Stop();
    }
}

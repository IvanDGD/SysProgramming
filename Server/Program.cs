using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ServerApp
{ 
    static void Main()
    {
        TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
        listener.Start();
        Console.WriteLine("Server started. Waiting for connections...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[256];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();

            Console.WriteLine($"Received request: {request}");

            string response = request.ToLower() switch
            {
                "time" => DateTime.Now.ToString("HH:mm:ss"),
                "date" => DateTime.Now.ToString("dd.MM.yyyy"),
                _ => "Unknown command"
            };

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);

            client.Close();
            Console.WriteLine("Connection closed.\n");
        }
    }
}

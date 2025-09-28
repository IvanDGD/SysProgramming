using System;
using System.Net.Sockets;
using System.Text;

class ClientApp
{
    static void Main()
    {
        Console.WriteLine("Enter command (time or date):");
        string command = Console.ReadLine();

        TcpClient client = new TcpClient("127.0.0.1", 5000);
        NetworkStream stream = client.GetStream();

        byte[] requestBytes = Encoding.UTF8.GetBytes(command);
        stream.Write(requestBytes, 0, requestBytes.Length);

        byte[] buffer = new byte[256];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Console.WriteLine($"Server response: {response}");

        client.Close();
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        UdpClient client = new UdpClient();
        IPEndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 8888);

        Console.WriteLine("Введите название комплектующей (или 'exit' для выхода):");

        while (true)
        {
            string message = Console.ReadLine();
            if (message.ToLower() == "exit") break;

            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, serverEP);

            IPEndPoint remoteEP = null;
            byte[] responseData = client.Receive(ref remoteEP);
            string response = Encoding.UTF8.GetString(responseData);

            Console.WriteLine(response);
            Console.WriteLine();
        }

        client.Close();
    }
}

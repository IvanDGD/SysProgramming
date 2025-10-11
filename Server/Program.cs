using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        UdpClient server = new UdpClient(8888);
        Console.WriteLine("Server start");

        Dictionary<string, string> prices = new Dictionary<string, string>()
        {
            { "cpu", "130 $" },
            { "gpu", "297 $" },
            { "motherboard", "237 $" },
            { "ram", "127 $" },
            { "hdd", "50 $" },
            { "ssd", "100 $" },
            { "power supply", "80 $" }
        };

        IPEndPoint remoteEP = null;

        while (true)
        {
            byte[] data = server.Receive(ref remoteEP);
            string request = Encoding.UTF8.GetString(data).ToLower();
            Console.WriteLine($"Запрос от {remoteEP}: {request}");

            string response = prices.ContainsKey(request)
                ? $"Цена на {request}: {prices[request]}"
                : $"Товар '{request}' не найден.";

            byte[] responseData = Encoding.UTF8.GetBytes(response);
            server.Send(responseData, responseData.Length, remoteEP);
        }
    }
}

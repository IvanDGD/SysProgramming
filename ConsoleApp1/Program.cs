using System.Net.Sockets;
using System.Text;

class Program
{
    static async Task Main()
    {
        string host = "127.0.0.1";
        int port = 8888;

        using TcpClient client = new TcpClient();
        await client.ConnectAsync(host, port);
        Console.WriteLine("Connected to currency server.");
        Console.WriteLine("Type query: <FROM> <TO> (USD EUR). Type 'exit' to stop.");

        var reader = new StreamReader(client.GetStream(), Encoding.UTF8);
        var writer = new StreamWriter(client.GetStream(), Encoding.UTF8);

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (input == null || input.ToLower() == "exit") break;

            await writer.WriteLineAsync(input);
            await writer.FlushAsync();

            string? response = await reader.ReadLineAsync();
            Console.WriteLine("Server: " + response);
        }

        Console.WriteLine("Disconnected.");
    }
}

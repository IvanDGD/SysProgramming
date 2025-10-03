using System.Net;
using System.Net.Sockets;
using System.Text;

// простой словарь курсов валют (условные курсы)
class ExchangeRates
{
    private Dictionary<string, double> rates = new()
    {
        { "USD", 1.0 },
        { "EUR", 0.85 },
        { "UAH", 41.2 },
        { "GBP", 0.82 },
        { "JPY", 150.3 }
    };

    public string Convert(string from, string to)
    {
        double result = rates[to] / rates[from];
        return $"1 {from} = {result:F4} {to}";
    }
}

class Program
{
    static async Task Main()
    {
        Server server = new Server();
        await server.ListenAsync();
    }
}

class Server
{
    TcpListener listener = new TcpListener(IPAddress.Any, 8888);
    List<ClientHandler> clients = new List<ClientHandler>();
    ExchangeRates rates = new ExchangeRates();

    public async Task ListenAsync()
    {
        listener.Start();
        Console.WriteLine("Currency Server started...");

        while (true)
        {
            var tcpClient = await listener.AcceptTcpClientAsync();
            var client = new ClientHandler(tcpClient, this, rates);
            clients.Add(client);
            Task.Run(client.ProcessAsync);
        }
    }

    public void RemoveClient(ClientHandler client)
    {
        clients.Remove(client);
        client.Close();
    }
}

class ClientHandler
{
    private TcpClient client;
    private Server server;
    private ExchangeRates rates;
    private StreamReader reader;
    private StreamWriter writer;
    private string clientInfo;

    public ClientHandler(TcpClient tcpClient, Server srv, ExchangeRates r)
    {
        client = tcpClient;
        server = srv;
        rates = r;

        var stream = client.GetStream();
        reader = new StreamReader(stream, Encoding.UTF8);
        writer = new StreamWriter(stream, Encoding.UTF8);
    }

    public async Task ProcessAsync()
    {
        Console.WriteLine($"[{DateTime.Now}] Client connected: {clientInfo}");
        try
        {
            while (true)
            {
                string? request = await reader.ReadLineAsync();
                if (request == null) break;

                string[] parts = request.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    string from = parts[0].ToUpper();
                    string to = parts[1].ToUpper();
                    string result = rates.Convert(from, to);

                    Console.WriteLine($"[{DateTime.Now}] {clientInfo}: {request} -> {result}");
                    await writer.WriteLineAsync(result);
                    await writer.FlushAsync();
                }
                else
                {
                    await writer.WriteLineAsync("Format: <FROM> <TO>");
                    await writer.FlushAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error with {clientInfo}: {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"[{DateTime.Now}] Client disconnected: {clientInfo}");
            server.RemoveClient(this);
        }
    }

    public void Close()
    {
        writer.Close();
        reader.Close();
        client.Close();
    }
}

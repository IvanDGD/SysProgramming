using System.IO.Compression;
using System.Text.RegularExpressions;

class Program
{
    static readonly string logsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
    static readonly string archivePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs_archive.zip");
    static readonly string errorLogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLogs.txt");
    static readonly object statsLock = new object();
    static readonly Dictionary<string, int> logLinesCount = new Dictionary<string, int>();
    static int totalErrors = 0;

    static async Task Main()
    {
        Directory.CreateDirectory(logsDir);
        List<Task> tasks = new List<Task>();

        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(logsDir, $"log{i}.txt");
            logLinesCount[filePath] = 0;
            tasks.Add(Task.Run(() => GenerateLogs(filePath)));
        }

        Task archiver = Task.Run(() => ArchiveLogs());
        Task analyzer = Task.Run(() => AnalyzeLogs());

        await Task.WhenAll(tasks);
        await Task.Delay(12000);

        Console.WriteLine("\n=== Stats ===");
        lock (statsLock)
        {
            foreach (var kv in logLinesCount)
                Console.WriteLine($"{Path.GetFileName(kv.Key)}: {kv.Value} lines");
            Console.WriteLine($"Errors/Warnings found: {totalErrors}");
            if (File.Exists(archivePath))
                Console.WriteLine($"Archive size: {new FileInfo(archivePath).Length} bytes");
        }
    }


    static async Task GenerateLogs(string filePath)
    {
        Random rnd = new Random();
        for (int i = 0; i < 50; i++)
        {
            string message = $"[{DateTime.Now:HH:mm:ss}] Thread {Path.GetFileName(filePath)}: processing data";
            if (rnd.Next(0, 100) < 10)
                message += " Warning: simulated error";
            lock (statsLock) logLinesCount[filePath]++;
            await File.AppendAllTextAsync(filePath, message + Environment.NewLine);
            await Task.Delay(rnd.Next(100, 300));
        }
    }

    static async Task ArchiveLogs()
    {
        while (true)
        {
            foreach (string file in Directory.GetFiles(logsDir, "*.txt"))
            {
                if (new FileInfo(file).Length > 1024)
                {
                    using (var zip = new FileStream(archivePath, File.Exists(archivePath) ? FileMode.Open : FileMode.Create))
                    using (var archive = new ZipArchive(zip, ZipArchiveMode.Update))
                    {
                        string entryName = Path.GetFileName(file);
                        archive.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);
                    }
                    File.WriteAllText(file, string.Empty);
                }
            }
            await Task.Delay(5000);
        }
    }

    static async Task AnalyzeLogs()
    {
        Regex regex = new Regex(@"Ошибка|Warning|Exception", RegexOptions.IgnoreCase);
        while (true)
        {
            foreach (string file in Directory.GetFiles(logsDir, "*.txt"))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    if (regex.IsMatch(line))
                    {
                        using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var reader = new StreamReader(stream))
                        {
                            string content = await reader.ReadToEndAsync();
                        }

                    }
                }
            }
            await Task.Delay(10000);
        }
    }
}

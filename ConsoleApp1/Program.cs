using System.IO.Compression;

class Program
{
    static async Task Main(string[] args)
    {
        #region GeneralTask
        //Console.WriteLine("Enter path to directory:");
        //string directoryPath = Console.ReadLine();

        //if (!Directory.Exists(directoryPath))
        //{
        //    Console.WriteLine("Directory not exist.");
        //    return;
        //}

        //string[] files = Directory.GetFiles(directoryPath);

        //Task[] compressionTasks = new Task[files.Length];
        //for (int i = 0; i < files.Length; i++)
        //{
        //    string file = files[i];
        //    compressionTasks[i] = CompressFileAsync(file);
        //}

        //await Task.WhenAll(compressionTasks);

        //Console.WriteLine("Compression end.");
        #endregion
        #region AdditionTask
        using (HttpClient httpClient = new HttpClient())
        {
            string url = $"https://api.weatherapi.com/v1/current.json?key=accb029b1e9f4bbe98972252251309&q={city}";

            Task<string> task = httpClient.GetStringAsync(url);

            await Task.WhenAll(task);

            string weatherTask = task.Result;

            Console.WriteLine("WeatherAPI");
            Console.WriteLine(weatherTask);
        }
        #endregion
    }

    static async Task CompressFileAsync(string filePath)
    {
        string compressedFile = filePath + ".gz";

        if (File.Exists(compressedFile))
        {
            Console.WriteLine($"File {Path.GetFileName(filePath)} compression already.");
            return;
        }

        try
        {
            using (FileStream originalFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Create))
            using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
            {
                await originalFileStream.CopyToAsync(compressionStream);
            }

            Console.WriteLine($"Compression: {Path.GetFileName(filePath)} → {Path.GetFileName(compressedFile)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error with file {filePath}: {ex.Message}");
        }
    }
}

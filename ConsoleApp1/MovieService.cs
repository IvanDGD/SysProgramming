using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

class MovieService
{
    private readonly string apiKey;
    private readonly HttpClient httpClient;

    public MovieService()
    {
        string json = File.ReadAllText("appsettings.json");
        var config = JsonSerializer.Deserialize<Config>(json);
        apiKey = config.ApiKey;
        httpClient = new HttpClient();
    }

    public async Task<Movie?> GetMovieAsync(string title)
    {
        
        
        string url = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(title)}&language=ru-RU";

        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"HTTP error: {response.StatusCode}");
            return null;
        }

        string json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<MovieSearchResult>(json, options);

        if (result == null || result.results == null || result.results.Length == 0)
        {
            Console.WriteLine("Фильм не найден.");
            return null;
        }

        return result.results[0];
    }
}

class Config
{
    public string ApiKey { get; set; }
}

class MovieSearchResult
{
    public Movie[] results { get; set; }
}

class Movie
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("overview")]
    public string Overview { get; set; }

    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; set; }
}

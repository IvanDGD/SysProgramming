using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        MovieService movieService = new MovieService();

        Console.WriteLine("Enter film name (или 'exit' для выхода):");

        while (true)
        {
            Console.Write("> ");
            string title = Console.ReadLine();

            if (title == "exit") break;

            var movie = await movieService.GetMovieAsync(title);

            if (movie != null)
            {
                Console.WriteLine("\nFilm info:");
                Console.WriteLine($"Name: {movie.Title}");
                Console.WriteLine($"Exits date: {movie.ReleaseDate}");
                Console.WriteLine($"Rate: {movie.VoteAverage}");
                Console.WriteLine($"Desc: {movie.Overview}\n");
            }
        }
    }
}

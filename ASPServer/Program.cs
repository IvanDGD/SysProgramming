namespace ASPServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Run(async (context) =>
            {
                using StreamReader reader = new StreamReader(context.Request.Body);
                string name = await reader.ReadToEndAsync();

                await context.Response.WriteAsync($"Data received: {name}");
            });

            app.Run();
        }
    }
}

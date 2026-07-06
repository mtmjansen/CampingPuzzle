using CampingPuzzle.Server.Classes;
using CampingPuzzle.Server.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CampingPuzzle.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            var hubContext = app.Services.GetRequiredService<IHubContext<CampingHub>>();
            Tile.HubContext = hubContext;

            app.UseDefaultFiles();
            app.MapStaticAssets();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHub<CampingHub>("/campingHub");
            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}

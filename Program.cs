using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RewatchIt.Data;

namespace RewatchIt
{
  public class Program
  {
    #region Methods

    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

      CreateDbIfNotExists(host);

      host.Run();
    }

    private static void CreateDbIfNotExists(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<WatchedMovieContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseStartup<Startup>();
        });
    }

    #endregion
  }

  public static class DbInitializer
  {
      public static void Initialize(WatchedMovieContext context)
      {
          bool result = context.Database.EnsureCreated();

          int count = context.Movies.Count();

          // Look for any students.
          if (context.Movies.Any())
          {
              Console.WriteLine("Test!");
          }
          else
          {
              Console.WriteLine("Test!");
          }
      }
  }
}
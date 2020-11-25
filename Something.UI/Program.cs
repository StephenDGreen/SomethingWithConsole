using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Something.Application;
using Something.Domain;
using Something.Persistence;
using System;
using System.Threading.Tasks;

namespace Something.UI
{
    class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                var host = CreateHostBuilder(args).Build();
                var somethingService = ActivatorUtilities.CreateInstance<SomethingService>(host.Services);
                _ = somethingService.Run();
            }
            catch (Exception)
            {
                Log.Fatal("Application failed to run");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
                        .ConfigureServices((_, services) =>
                        services.AddSingleton<ISomethingFactory, SomethingFactory>()
                        .AddScoped<ISomethingCreateInteractor, SomethingCreateInteractor>()
                        .AddScoped<ISomethingReadInteractor, SomethingReadInteractor>()
                        .AddScoped<ISomethingPersistence, SomethingPersistence>()
                        .AddSingleton<ISomethingElseFactory, SomethingElseFactory>()
                        .AddScoped<ISomethingElseCreateInteractor, SomethingElseCreateInteractor>()
                        .AddScoped<ISomethingElseReadInteractor, SomethingElseReadInteractor>()
                        .AddScoped<ISomethingElseUpdateInteractor, SomethingElseUpdateInteractor>()
                        .AddScoped<ISomethingElseDeleteInteractor, SomethingElseDeleteInteractor>()
                        .AddScoped<ISomethingElsePersistence, SomethingElsePersistence>()
                        .AddTransient<ISomethingService, SomethingService>()
                        .AddSingleton(Log.Logger)
                        .AddDbContext<AppDbContext>(
                            options => options.UseInMemoryDatabase(databaseName: "Something")
                            ))
                        .UseSerilog();
            
    }
}

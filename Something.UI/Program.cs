using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Something.Application;
using Something.Domain;
using Something.Persistence;
using System;

namespace Something.UI
{
    class Program
    {
        /// <param name="createDummyData">An option whose argument is parsed as an bool</param>
        public static void Main(bool createDummyData)
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
                var host = CreateHostBuilder().Build();
                var somethingService = ActivatorUtilities.CreateInstance<SomethingService>(host.Services);
                _ = somethingService.Run(createDummyData);
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

        static IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
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

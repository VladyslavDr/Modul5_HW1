using Microsoft.Extensions.Logging;
using Modul5Task.Services;
using Modul5Task.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.VisualBasic.FileIO;
using Modul5Task.Services.Abstractions;
using Modul5Task.Configurations;

namespace Modul5Task
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            /*
            var httpClientService = new HttpClientService();

            var loggerFactory = LoggerFactory.Create(b => b.AddConsole());
            var logger = loggerFactory.CreateLogger<UserService>();

            var userServise = new UserService(httpClientService, logger);
            var application = new Application(userServise);

            await application.Start();
            */
            void ConfigureServices(ServiceCollection serviceCollection, IConfiguration configuration)
            {
                serviceCollection.AddOptions<ApiOption>().Bind(configuration.GetSection("Api"));
                serviceCollection
                    .AddLogging(configure => configure.AddConsole())
                    .AddHttpClient()
                    .AddTransient<IHttpClientService, HttpClientService>()
                    .AddTransient<IUserService, UserService>()
                    .AddTransient<Application>();
            }

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var provider = serviceCollection.BuildServiceProvider();

            var application = provider.GetService<Application>();
            await application!.Start();
        }
    }
}
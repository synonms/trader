using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Synonms.Trader.Core.Services.CoinMarketCap;

namespace Synonms.Trader.Cli
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddTransient<ICoinMarketCapService, CoinMarketCapService>(services => 
                        new CoinMarketCapService(services.GetRequiredService<IHttpClientFactory>(), "<YOUR_API_KEY_HERE>"));
                }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var coinMarketCapService = services.GetRequiredService<ICoinMarketCapService>();
                    var listings = await coinMarketCapService.GetListingsLatest();

                    Console.WriteLine(listings);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred.");
                }
            }

            Console.WriteLine("Press a key to exit...");
            Console.ReadLine();

            return 0;
        }
    }
}

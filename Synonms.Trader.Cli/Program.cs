using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Synonms.Trader.Core.Services.Bittrex;

namespace Synonms.Trader.Cli
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var bittrexApiKey = GetBittrexApiKey();
            var bittrexApiSecret = GetBittrexApiSecret();

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    //services.AddTransient<ICoinMarketCapService, CoinMarketCapService>(services => 
                    //    new CoinMarketCapService(
                    //        services.GetRequiredService<IHttpClientFactory>(), 
                    //        "<YOUR_API_KEY_HERE>"));
                    services.AddTransient<IBittrexService, BittrexService>(services =>
                        new BittrexService(
                            services.GetRequiredService<ILogger<BittrexService>>(), 
                            services.GetRequiredService<IHttpClientFactory>(),
                            bittrexApiKey,
                            bittrexApiSecret));
                })
                .ConfigureLogging(loggingBuilder => 
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                })
                .UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    //var coinMarketCapService = services.GetRequiredService<ICoinMarketCapService>();
                    //var listings = await coinMarketCapService.GetListingsLatest();

                    //Console.WriteLine(listings);

                    var bittrexService = services.GetRequiredService<IBittrexService>();

                    var balances = await bittrexService.GetBalances();
                    var closedOrders = await bittrexService.GetClosedOrders();
                    var markets = await bittrexService.GetMarkets();
                    var marketSummaries = await bittrexService.GetMarketSummaries();
                    var tickers = await bittrexService.GetTickers();

                    Console.WriteLine("=========================");
                    Console.WriteLine("BALANCES:");
                    Console.WriteLine("=========================");
                    foreach (var balance in balances)
                    {
                        Console.WriteLine(balance);
                    }

                    Console.WriteLine("=========================");
                    Console.WriteLine("CLOSED ORDERS:");
                    Console.WriteLine("=========================");
                    foreach (var order in closedOrders)
                    {
                        Console.WriteLine(order);
                    }

                    Console.WriteLine("=========================");
                    Console.WriteLine("MARKETS:");
                    Console.WriteLine("=========================");
                    foreach (var market in markets)
                    {
                        Console.WriteLine(market);
                    }

                    Console.WriteLine("=========================");
                    Console.WriteLine("MARKET SUMMARIES:");
                    Console.WriteLine("=========================");
                    foreach (var marketSummary in marketSummaries)
                    {
                        Console.WriteLine(marketSummary);
                    }

                    Console.WriteLine("=========================");
                    Console.WriteLine("TICKERS:");
                    Console.WriteLine("=========================");
                    foreach (var ticker in tickers)
                    {
                        Console.WriteLine(ticker);
                    }
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

        public static string GetBittrexApiKey()
        {
            var apiKey = string.Empty;
         
            while(string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("Enter Bittrex API Key:");

                apiKey = Console.ReadLine();
            }

            return apiKey;
        }

        public static string GetBittrexApiSecret()
        {
            var apiSecret = string.Empty;

            while (string.IsNullOrWhiteSpace(apiSecret))
            {
                Console.WriteLine("Enter Bittrex API Secret:");

                apiSecret = Console.ReadLine();
            }

            return apiSecret;
        }

    }
}

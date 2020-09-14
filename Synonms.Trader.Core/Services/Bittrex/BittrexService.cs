using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Synonms.Trader.Core.Services.Bittrex.Models;

namespace Synonms.Trader.Core.Services.Bittrex
{
    public class BittrexService : IBittrexService
    {
        private readonly ILogger<BittrexService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;
        private readonly string _apiSecret;

        public BittrexService(ILogger<BittrexService> logger, IHttpClientFactory httpClientFactory, string apiKey, string apiSecret)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _apiKey = apiKey;
            _apiSecret = apiSecret;
        }



        public Task<IEnumerable<Balance>> GetBalances()
        {
            _logger.LogInformation("GET Balances...");

            return Get<IEnumerable<Balance>>("https://api.bittrex.com/v3/balances");
        }

        public Task<IEnumerable<Order>> GetClosedOrders()
        {
            _logger.LogInformation("GET Closed Orders...");

            return Get<IEnumerable<Order>>("https://api.bittrex.com/v3/orders/closed");
        }

        public Task<IEnumerable<Market>> GetMarkets()
        {
            _logger.LogInformation("GET Markets...");

            return Get<IEnumerable<Market>>("https://api.bittrex.com/v3/markets");
        }

        public Task<IEnumerable<MarketSummary>> GetMarketSummaries()
        {
            _logger.LogInformation("GET Market Summaries...");

            return Get<IEnumerable<MarketSummary>>("https://api.bittrex.com/v3/markets/summaries");
        }

        public Task<IEnumerable<Ticker>> GetTickers()
        {
            _logger.LogInformation("GET Tickers...");

            return Get<IEnumerable<Ticker>>("https://api.bittrex.com/v3/markets/tickers");
        }

        private async Task<T> Get<T>(string uri)
        {
            var request = CreateMessage(HttpMethod.Get, uri, string.Empty);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            _logger.LogInformation("Response recieved:");

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug(content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Request succeeded");

                return JsonSerializer.Deserialize<T>(content);
            }
            else
            {
                _logger.LogError($"Request failed with StatusCode {response.StatusCode}");

                return default;
            }
        }

        private HttpRequestMessage CreateMessage(HttpMethod method, string requestUri, string content)
        {
            var apiTimestamp = Math.Round((DateTime.UtcNow - DateTime.UnixEpoch).TotalMilliseconds).ToString();
            var contentHash = HashContent(content);

            var request = new HttpRequestMessage(method, requestUri);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Api-Key", _apiKey);
            request.Headers.Add("Api-Timestamp", apiTimestamp);
            request.Headers.Add("Api-Content-Hash", contentHash);
            request.Headers.Add("Api-Signature", Sign(apiTimestamp, requestUri, "GET", contentHash));

            return request;
        }

        /// <summary>
        /// Pre-Sign: Api-Timestamp header & Full URI (inc. QueryString) & HTTP Method in caps & Api-Content-Hash header
        /// Sign it via HmacSHA512, using your API secret as the signing secret. 
        /// Hex-encode the result of this operation.
        /// </summary>
        /// <param name="apiTimestamp">Api-Timestamp header</param>
        /// <param name="uri">Full URI (inc. QueryString)</param>
        /// <param name="method">HTTP Method in caps</param>
        /// <param name="contentHash">Api-Content-Hash header</param>
        /// <returns></returns>
        private string Sign(string apiTimestamp, string uri, string method, string contentHash)
        {
            var preSign = $"{apiTimestamp}{uri}{method}{contentHash}";

            byte[] data = Encoding.UTF8.GetBytes(preSign);
            byte[] key = Encoding.UTF8.GetBytes(_apiSecret);

            using var hash = new HMACSHA512(key);

            var hashBytes = hash.ComputeHash(data);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private string HashContent(string content)
        {
            byte[] data = Encoding.UTF8.GetBytes(content);

            using var sha512 = new SHA512Managed();

            var hash = sha512.ComputeHash(data);

            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}

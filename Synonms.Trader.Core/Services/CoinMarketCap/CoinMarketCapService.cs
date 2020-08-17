using System.Net.Http;
using System.Threading.Tasks;

namespace Synonms.Trader.Core.Services.CoinMarketCap
{
    public class CoinMarketCapService : ICoinMarketCapService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public CoinMarketCapService(IHttpClientFactory httpClientFactory, string apiKey)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = apiKey;
        }

        public async Task<string> GetListingsLatest()
        {
            var uri = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest?start=1&limit=10&convert=GBP";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }
    }
}

using System.Threading.Tasks;

namespace Synonms.Trader.Core.Services.CoinMarketCap
{
    public interface ICoinMarketCapService
    {
        Task<string> GetListingsLatest();
    }
}
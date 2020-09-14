using System.Collections.Generic;
using System.Threading.Tasks;
using Synonms.Trader.Core.Services.Bittrex.Models;

namespace Synonms.Trader.Core.Services.Bittrex
{
    public interface IBittrexService
    {
        Task<IEnumerable<Balance>> GetBalances();
        Task<IEnumerable<Order>> GetClosedOrders();
        Task<IEnumerable<Market>> GetMarkets();
        Task<IEnumerable<MarketSummary>> GetMarketSummaries();
        Task<IEnumerable<Ticker>> GetTickers();
    }
}
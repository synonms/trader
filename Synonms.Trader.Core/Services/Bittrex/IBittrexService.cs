using System.Collections.Generic;
using System.Threading.Tasks;
using Synonms.Trader.Core.Services.Bittrex.Models;

namespace Synonms.Trader.Core.Services.Bittrex
{
    public interface IBittrexService
    {
        Task<IEnumerable<Balance>> GetBalances();
    }
}
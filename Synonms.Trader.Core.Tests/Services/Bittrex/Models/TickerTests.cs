using System.Collections.Generic;
using System.Text.Json;
using Synonms.Trader.Core.Services.Bittrex.Models;
using Xunit;

namespace Synonms.Trader.Core.Tests.Services.Bittrex.Models
{
    public class TickerTests
    {
        [Fact]
        public void Deserialise_GivenJsonObject_ThenReturnsObject()
        {
            var json = @"{
    ""symbol"": ""4ART-BTC"",
    ""lastTradeRate"": ""0.00000212"",
    ""bidRate"": ""0.00000208"",
    ""askRate"": ""0.00000212""
  }";

            var result = JsonSerializer.Deserialize<Ticker>(json);

            Assert.NotNull(result);
            Assert.Equal("4ART-BTC", result.Symbol);
            Assert.Equal("0.00000212", result.LastTradeRate);
            Assert.Equal("0.00000208", result.BidRate);
            Assert.Equal("0.00000212", result.AskRate);
        }

        [Fact]
        public void Deserialise_GivenJsonArray_ThenReturnsEnumerableObjects()
        {
            var json = @"[
  {
    ""symbol"": ""4ART-BTC"",
    ""lastTradeRate"": ""0.00000212"",
    ""bidRate"": ""0.00000208"",
    ""askRate"": ""0.00000212""
  },
  {
    ""symbol"": ""4ART-USDT"",
    ""lastTradeRate"": ""0.02229000"",
    ""bidRate"": ""0.02229000"",
    ""askRate"": ""0.02246000""
  },
  {
    ""symbol"": ""ABBC-BTC"",
    ""lastTradeRate"": ""0.00001700"",
    ""bidRate"": ""0.00001666"",
    ""askRate"": ""0.00001699""
  },
  {
    ""symbol"": ""ABYSS-BTC"",
    ""lastTradeRate"": ""0.00000179"",
    ""bidRate"": ""0.00000177"",
    ""askRate"": ""0.00000179""
  }]";
            var results = JsonSerializer.Deserialize<IEnumerable<Ticker>>(json);

            Assert.Collection(results,
                item => Assert.Equal("4ART-BTC", item.Symbol),
                item => Assert.Equal("4ART-USDT", item.Symbol),
                item => Assert.Equal("ABBC-BTC", item.Symbol),
                item => Assert.Equal("ABYSS-BTC", item.Symbol)
            );
        }
    }
}

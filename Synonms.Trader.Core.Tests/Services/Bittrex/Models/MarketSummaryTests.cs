using System;
using System.Collections.Generic;
using System.Text.Json;
using Synonms.Trader.Core.Services.Bittrex.Models;
using Xunit;

namespace Synonms.Trader.Core.Tests.Services.Bittrex.Models
{
    public class MarketSummaryTests
    {
        [Fact]
        public void Deserialise_GivenJsonObject_ThenReturnsObject()
        {
            var json = @"{
    ""symbol"": ""4ART-BTC"",
    ""high"": ""0.00000221"",
    ""low"": ""0.00000208"",
    ""volume"": ""26835.80024230"",
    ""quoteVolume"": ""0.05711676"",
    ""percentChange"": ""-1.85"",
    ""updatedAt"": ""2020-09-14T15:13:13.257Z""
  }";
            var expectedUpdatedAt = DateTime.Parse("2020-09-14T15:13:13.257Z", null, System.Globalization.DateTimeStyles.AdjustToUniversal);

            var result = JsonSerializer.Deserialize<MarketSummary>(json);

            Assert.NotNull(result);
            Assert.Equal("4ART-BTC", result.Symbol);
            Assert.Equal("0.00000221", result.High);
            Assert.Equal("0.00000208", result.Low);
            Assert.Equal("26835.80024230", result.Volume);
            Assert.Equal("0.05711676", result.QuoteVolume);
            Assert.Equal("-1.85", result.PercentChange);
            Assert.Equal(expectedUpdatedAt, result.UpdatedAt);
        }

        [Fact]
        public void Deserialise_GivenJsonArray_ThenReturnsEnumerableObjects()
        {
            var json = @"[
{
    ""symbol"": ""4ART-BTC"",
    ""high"": ""0.00000221"",
    ""low"": ""0.00000208"",
    ""volume"": ""26835.80024230"",
    ""quoteVolume"": ""0.05711676"",
    ""percentChange"": ""-1.85"",
    ""updatedAt"": ""2020-09-14T15:13:13.257Z""
  },
  {
    ""symbol"": ""4ART-USDT"",
    ""high"": ""0.02229000"",
    ""low"": ""0.02221000"",
    ""volume"": ""8064.87449249"",
    ""quoteVolume"": ""179.46509731"",
    ""percentChange"": ""0.27"",
    ""updatedAt"": ""2020-09-14T15:13:13.257Z""
  },
  {
    ""symbol"": ""ABBC-BTC"",
    ""high"": ""0.00001700"",
    ""low"": ""0.00001589"",
    ""volume"": ""198097.90743505"",
    ""quoteVolume"": ""3.26955029"",
    ""percentChange"": ""4.36"",
    ""updatedAt"": ""2020-09-14T15:12:48.257Z""
  },
  {
    ""symbol"": ""ABYSS-BTC"",
    ""high"": ""0.00000000"",
    ""low"": ""0.00000000"",
    ""volume"": ""0.00000000"",
    ""quoteVolume"": ""0.00000000"",
    ""updatedAt"": ""2020-09-14T08:09:33.003Z""
  }
  ]";
            var results = JsonSerializer.Deserialize<IEnumerable<MarketSummary>>(json);

            Assert.Collection(results,
                item => Assert.Equal("4ART-BTC", item.Symbol),
                item => Assert.Equal("4ART-USDT", item.Symbol),
                item => Assert.Equal("ABBC-BTC", item.Symbol),
                item => Assert.Equal("ABYSS-BTC", item.Symbol)
            );
        }
    }
}

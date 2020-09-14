using System;
using System.Collections.Generic;
using System.Text.Json;
using Synonms.Trader.Core.Services.Bittrex.Models;
using Xunit;

namespace Synonms.Trader.Core.Tests.Services.Bittrex.Models
{
    public class MarketTests
    {
        [Fact]
        public void Deserialise_GivenJsonObject_ThenReturnsObject()
        {
            var json = @" {
    ""symbol"": ""4ART-BTC"",
    ""baseCurrencySymbol"": ""4ART"",
    ""quoteCurrencySymbol"": ""BTC"",
    ""minTradeSize"": ""10.00000000"",
    ""precision"": 8,
    ""status"": ""ONLINE"",
    ""createdAt"": ""2020-06-10T15:05:29.833Z"",
    ""notice"": """",
    ""prohibitedIn"": [
      ""US""
    ],
    ""associatedTermsOfService"": []
  }";
            var expectedCreatedAt = DateTime.Parse("2020-06-10T15:05:29.833Z", null, System.Globalization.DateTimeStyles.AdjustToUniversal);

            var result = JsonSerializer.Deserialize<Market>(json);

            Assert.NotNull(result);
            Assert.Equal("4ART-BTC", result.Symbol);
            Assert.Equal("4ART", result.BaseCurrencySymbol);
            Assert.Equal("BTC", result.QuoteCurrencySymbol);
            Assert.Equal("10.00000000", result.MinTradeSize);
            Assert.Equal(8, result.Precision);
            Assert.Equal("ONLINE", result.Status);
            Assert.Equal(expectedCreatedAt, result.CreatedAt);
        }

        [Fact]
        public void Deserialise_GivenJsonArray_ThenReturnsEnumerableObjects()
        {
            var json = @"[
  {
    ""symbol"": ""4ART-BTC"",
    ""baseCurrencySymbol"": ""4ART"",
    ""quoteCurrencySymbol"": ""BTC"",
    ""minTradeSize"": ""10.00000000"",
    ""precision"": 8,
    ""status"": ""ONLINE"",
    ""createdAt"": ""2020-06-10T15:05:29.833Z"",
    ""notice"": """",
    ""prohibitedIn"": [
      ""US""
    ],
    ""associatedTermsOfService"": []
  },
  {
    ""symbol"": ""4ART-USDT"",
    ""baseCurrencySymbol"": ""4ART"",
    ""quoteCurrencySymbol"": ""USDT"",
    ""minTradeSize"": ""10.00000000"",
    ""precision"": 5,
    ""status"": ""ONLINE"",
    ""createdAt"": ""2020-06-10T15:05:40.98Z"",
    ""notice"": """",
    ""prohibitedIn"": [
      ""US""
    ],
    ""associatedTermsOfService"": []
    },
  {
    ""symbol"": ""ABBC-BTC"",
    ""baseCurrencySymbol"": ""ABBC"",
    ""quoteCurrencySymbol"": ""BTC"",
    ""minTradeSize"": ""30.00000000"",
    ""precision"": 8,
    ""status"": ""ONLINE"",
    ""createdAt"": ""2020-02-18T23:51:50.197Z"",
    ""notice"": """",
    ""prohibitedIn"": [
      ""US""
    ],
    ""associatedTermsOfService"": []
},
  {
    ""symbol"": ""ABYSS-BTC"",
    ""baseCurrencySymbol"": ""ABYSS"",
    ""quoteCurrencySymbol"": ""BTC"",
    ""minTradeSize"": ""155.27950311"",
    ""precision"": 8,
    ""status"": ""ONLINE"",
    ""createdAt"": ""2019-07-02T17:35:13.857Z"",
    ""prohibitedIn"": [
      ""US""
    ],
    ""associatedTermsOfService"": []
  }]";
            var results = JsonSerializer.Deserialize<IEnumerable<Market>>(json);

            Assert.Collection(results,
                item => Assert.Equal("4ART-BTC", item.Symbol),
                item => Assert.Equal("4ART-USDT", item.Symbol),
                item => Assert.Equal("ABBC-BTC", item.Symbol),
                item => Assert.Equal("ABYSS-BTC", item.Symbol)
            );
        }
    }
}

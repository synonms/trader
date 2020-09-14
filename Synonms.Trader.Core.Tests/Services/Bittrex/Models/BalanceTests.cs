using System;
using System.Collections.Generic;
using System.Text.Json;
using Synonms.Trader.Core.Services.Bittrex.Models;
using Xunit;

namespace Synonms.Trader.Core.Tests.Services.Bittrex.Models
{
    public class BalanceTests
    {
        [Fact]
        public void Deserialise_GivenJsonObject_ThenReturnsObject()
        {
            var json = @"{
""currencySymbol"": ""ABBC"",
""total"": ""1.00000001"",
""available"": ""2.00000002"",
""updatedAt"": ""2020-09-02T16:09:27.44Z""
}";
            var expectedUpdatedAt = DateTime.Parse("2020-09-02T16:09:27.44Z", null, System.Globalization.DateTimeStyles.AdjustToUniversal);

            var result = JsonSerializer.Deserialize<Balance>(json);

            Assert.NotNull(result);
            Assert.Equal("ABBC", result.CurrencySymbol);
            Assert.Equal("1.00000001", result.Total);
            Assert.Equal("2.00000002", result.Available);
            Assert.Equal(expectedUpdatedAt, result.UpdatedAt);
        }

        [Fact]
        public void Deserialise_GivenJsonArray_ThenReturnsEnumerableObjects()
        {
            var json = @"[
  {
    ""currencySymbol"": ""ABBC"",
    ""total"": ""0.00000000"",
    ""available"": ""0.00000000"",
    ""updatedAt"": ""2020-09-02T16:09:27.44Z""
  },
  {
    ""currencySymbol"": ""ADA"",
    ""total"": ""3190.20480599"",
    ""available"": ""0.00000000"",
    ""updatedAt"": ""2020-08-30T13:56:26.72Z""
  },
  {
    ""currencySymbol"": ""ADX"",
    ""total"": ""0.00000000"",
    ""available"": ""0.00000000"",
    ""updatedAt"": ""2020-09-11T00:51:25.74Z""
  },
  {
    ""currencySymbol"": ""ALGO"",
    ""total"": ""0.00000000"",
    ""available"": ""0.00000000"",
    ""updatedAt"": ""2020-09-02T21:52:44.54Z""
  }]";
            var results = JsonSerializer.Deserialize<IEnumerable<Balance>>(json);

            Assert.Collection(results,  
                item => Assert.Equal("ABBC", item.CurrencySymbol),
                item => Assert.Equal("ADA", item.CurrencySymbol),
                item => Assert.Equal("ADX", item.CurrencySymbol),
                item => Assert.Equal("ALGO", item.CurrencySymbol)
            );
        }
    }
}

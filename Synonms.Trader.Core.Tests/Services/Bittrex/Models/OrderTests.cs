using System;
using System.Collections.Generic;
using System.Text.Json;
using Synonms.Trader.Core.Services.Bittrex.Models;
using Xunit;

namespace Synonms.Trader.Core.Tests.Services.Bittrex.Models
{
    public class OrderTests
    {
        [Fact]
        public void Deserialise_GivenJsonObject_ThenReturnsObject()
        {
            var json = @"{
    ""id"": ""61d1e002-fc5b-4c4b-9c2d-820f81f07296"",
    ""marketSymbol"": ""ADX-BTC"",
    ""direction"": ""SELL"",
    ""type"": ""LIMIT"",
    ""quantity"": ""1590.00000000"",
    ""limit"": ""0.00001676"",
    ""timeInForce"": ""GOOD_TIL_CANCELLED"",
    ""fillQuantity"": ""1590.00000000"",
    ""commission"": ""0.00005329"",
    ""proceeds"": ""0.02664840"",
    ""status"": ""CLOSED"",
    ""createdAt"": ""2020-09-03T13:00:31.18Z"",
    ""updatedAt"": ""2020-09-11T00:51:25.74Z"",
    ""closedAt"": ""2020-09-11T00:51:25.74Z""
  }";
            var expectedCreatedAt = DateTime.Parse("2020-09-03T13:00:31.18Z", null, System.Globalization.DateTimeStyles.AdjustToUniversal);
            var expectedUpdatedAt = DateTime.Parse("2020-09-11T00:51:25.74Z", null, System.Globalization.DateTimeStyles.AdjustToUniversal);
            var expectedClosedAt = DateTime.Parse("2020-09-11T00:51:25.74Z", null, System.Globalization.DateTimeStyles.AdjustToUniversal);

            var result = JsonSerializer.Deserialize<Order>(json);

            Assert.NotNull(result);
            Assert.Equal(Guid.Parse("61d1e002-fc5b-4c4b-9c2d-820f81f07296"), result.Id);
            Assert.Equal("ADX-BTC", result.MarketSymbol);
            Assert.Equal("SELL", result.Direction);
            Assert.Equal("LIMIT", result.Type);
            Assert.Equal("1590.00000000", result.Quantity);
            Assert.Equal("0.00001676", result.Limit);
            Assert.Equal("GOOD_TIL_CANCELLED", result.TimeInForce);
            Assert.Equal("1590.00000000", result.FillQuantity);
            Assert.Equal("0.00005329", result.Commission);
            Assert.Equal("0.02664840", result.Proceeds);
            Assert.Equal("CLOSED", result.Status);
            Assert.Equal(expectedCreatedAt, result.CreatedAt);
            Assert.Equal(expectedUpdatedAt, result.UpdatedAt);
            Assert.Equal(expectedClosedAt, result.ClosedAt);
        }

        [Fact]
        public void Deserialise_GivenJsonArray_ThenReturnsEnumerableObjects()
        {
            var json = @"[
 {
    ""id"": ""61d1e002-fc5b-4c4b-9c2d-820f81f07296"",
    ""marketSymbol"": ""ADX-BTC"",
    ""direction"": ""SELL"",
    ""type"": ""LIMIT"",
    ""quantity"": ""1590.00000000"",
    ""limit"": ""0.00001676"",
    ""timeInForce"": ""GOOD_TIL_CANCELLED"",
    ""fillQuantity"": ""1590.00000000"",
    ""commission"": ""0.00005329"",
    ""proceeds"": ""0.02664840"",
    ""status"": ""CLOSED"",
    ""createdAt"": ""2020-09-03T13:00:31.18Z"",
    ""updatedAt"": ""2020-09-11T00:51:25.74Z"",
    ""closedAt"": ""2020-09-11T00:51:25.74Z""
  },
  {
    ""id"": ""e0d395aa-3e05-40ef-94f8-6168b0fe332c"",
    ""marketSymbol"": ""LINK-BTC"",
    ""direction"": ""BUY"",
    ""type"": ""LIMIT"",
    ""quantity"": ""21.00141394"",
    ""limit"": ""0.00118802"",
    ""timeInForce"": ""GOOD_TIL_CANCELLED"",
    ""fillQuantity"": ""21.00141394"",
    ""commission"": ""0.00004990"",
    ""proceeds"": ""0.02495009"",
    ""status"": ""CLOSED"",
    ""createdAt"": ""2020-09-09T08:17:29.76Z"",
    ""updatedAt"": ""2020-09-09T08:17:29.76Z"",
    ""closedAt"": ""2020-09-09T08:17:29.76Z""
  },
  {
    ""id"": ""de454293-436f-4222-b45c-0dacdc2478a4"",
    ""marketSymbol"": ""XMR-BTC"",
    ""direction"": ""SELL"",
    ""type"": ""LIMIT"",
    ""quantity"": ""3.13697647"",
    ""limit"": ""0.00820173"",
    ""timeInForce"": ""GOOD_TIL_CANCELLED"",
    ""fillQuantity"": ""3.13697647"",
    ""commission"": ""0.00005145"",
    ""proceeds"": ""0.02572863"",
    ""status"": ""CLOSED"",
    ""createdAt"": ""2020-09-07T10:42:40.59Z"",
    ""updatedAt"": ""2020-09-08T21:36:07.13Z"",
    ""closedAt"": ""2020-09-08T21:36:07.13Z""
  },
  {
    ""id"": ""57fb8698-ada0-4b93-99d6-f54aaee20490"",
    ""marketSymbol"": ""DOT-BTC"",
    ""direction"": ""BUY"",
    ""type"": ""LIMIT"",
    ""quantity"": ""15.63531569"",
    ""limit"": ""0.00058087"",
    ""timeInForce"": ""GOOD_TIL_CANCELLED"",
    ""fillQuantity"": ""15.63531569"",
    ""commission"": ""0.00001816"",
    ""proceeds"": ""0.00908208"",
    ""status"": ""CLOSED"",
    ""createdAt"": ""2020-09-07T10:38:29.26Z"",
    ""updatedAt"": ""2020-09-07T10:38:29.26Z"",
    ""closedAt"": ""2020-09-07T10:38:29.26Z""
  }]";
            var results = JsonSerializer.Deserialize<IEnumerable<Order>>(json);

            Assert.Collection(results,
                item => Assert.Equal(Guid.Parse("61d1e002-fc5b-4c4b-9c2d-820f81f07296"), item.Id),
                item => Assert.Equal(Guid.Parse("e0d395aa-3e05-40ef-94f8-6168b0fe332c"), item.Id),
                item => Assert.Equal(Guid.Parse("de454293-436f-4222-b45c-0dacdc2478a4"), item.Id),
                item => Assert.Equal(Guid.Parse("57fb8698-ada0-4b93-99d6-f54aaee20490"), item.Id)
            );
        }
    }
}

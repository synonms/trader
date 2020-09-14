using System;
using System.Text.Json.Serialization;

namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class Market
    {
        private double _minTradeSize;
        private MarketStatus _status;

        public enum MarketStatus
        {
            ONLINE, OFFLINE
        }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("baseCurrencySymbol")]
        public string BaseCurrencySymbol { get; set; }

        [JsonPropertyName("quoteCurrencySymbol")]
        public string QuoteCurrencySymbol { get; set; }

        [JsonPropertyName("minTradeSize")]
        public string MinTradeSize
        {
            get
            {
                return _minTradeSize.ToString($"F{Precision}");
            }
            set
            {
                _minTradeSize = double.Parse(value);
            }
        }

        [JsonPropertyName("precision")]
        public int Precision { get; set; }

        [JsonPropertyName("status")]
        public string Status
        {
            get
            {
                return _status.ToString();
            }
            set
            {
                _status = (MarketStatus)Enum.Parse(typeof(MarketStatus), value);
            }
        }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("notice")]
        public string Notice { get; set; }

        [JsonPropertyName("prohibitedIn")]
        public string[] ProhibitedIn { get; set; }

        public override string ToString()
        {
            return $"{Symbol} ({QuoteCurrencySymbol}/{BaseCurrencySymbol}) [{Status}] - Min trade size {MinTradeSize} (precision {Precision}).  Created {CreatedAt}.";
        }
    }
}

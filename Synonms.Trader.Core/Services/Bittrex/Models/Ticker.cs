using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class Ticker
    {
        private double _lastTradeRate;
        private double _bidRate;
        private double _askRate;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("lastTradeRate")]
        [Required]
        public string LastTradeRate
        {
            get
            {
                return _lastTradeRate.ToString("F8");
            }
            set
            {
                _lastTradeRate = double.Parse(value);
            }
        }

        [JsonPropertyName("bidRate")]
        [Required]
        public string BidRate
        {
            get
            {
                return _bidRate.ToString("F8");
            }
            set
            {
                _bidRate = double.Parse(value);
            }
        }

        [JsonPropertyName("askRate")]
        [Required]
        public string AskRate
        {
            get
            {
                return _askRate.ToString("F8");
            }
            set
            {
                _askRate = double.Parse(value);
            }
        }

        public override string ToString()
        {
            return $"{Symbol} | BID {BidRate} ASK {AskRate} | Last trade {LastTradeRate}";
        }
    }
}

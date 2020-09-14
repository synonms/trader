using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class MarketSummary
    {
        private double _high;
        private double _low;
        private double _volume;
        private double _quoteVolume;
        private double _percentChange;

        [JsonPropertyName("symbol")]
        [Required]
        public string Symbol { get; set; }

        [JsonPropertyName("high")]
        [Required]
        public string High
        {
            get
            {
                return _high.ToString("F8");
            }
            set
            {
                _high = double.Parse(value);
            }
        }

        [JsonPropertyName("low")]
        [Required]
        public string Low
        {
            get
            {
                return _low.ToString("F8");
            }
            set
            {
                _low = double.Parse(value);
            }
        }

        [JsonPropertyName("volume")]
        [Required]
        public string Volume
        {
            get
            {
                return _volume.ToString("F8");
            }
            set
            {
                _volume = double.Parse(value);
            }
        }

        [JsonPropertyName("quoteVolume")]
        [Required]
        public string QuoteVolume
        {
            get
            {
                return _quoteVolume.ToString("F8");
            }
            set
            {
                _quoteVolume = double.Parse(value);
            }
        }

        [JsonPropertyName("percentChange")]
        [Required]
        public string PercentChange
        {
            get
            {
                return _percentChange.ToString("F2");
            }
            set
            {
                _percentChange = double.Parse(value);
            }
        }

        [JsonPropertyName("updatedAt")]
        [Required]
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"{Symbol} ({QuoteVolume}/{Volume}) [{PercentChange}%] - High {High} Low {Low}.  Last updated {UpdatedAt}.";
        }
    }
}

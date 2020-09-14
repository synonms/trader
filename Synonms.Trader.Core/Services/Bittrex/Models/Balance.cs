using System;
using System.Text.Json.Serialization;

namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class Balance
    {
        private double _total;
        private double _available;

        /// <summary>
        /// Unique ID for the currency this balance is associated with
        /// </summary>
        [JsonPropertyName("currencySymbol")]
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Total amount
        /// </summary>
        [JsonPropertyName("total")]
        public string Total
        { 
            get 
            { 
                return _total.ToString(); 
            }
            set 
            { 
                _total = double.Parse(value);
            }            
        }

        /// <summary>
        /// Available amount
        /// </summary>
        [JsonPropertyName("available")]
        public string Available 
        { 
            get 
            { 
                return _available.ToString(); 
            } 
            set 
            {
                _available = double.Parse(value); 
            }            
        }

        /// <summary>
        /// Time stamp when this balance was last updated
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"{CurrencySymbol} - {Available} available of {Total}.  Last updated {UpdatedAt}."; 
        }
    }
}

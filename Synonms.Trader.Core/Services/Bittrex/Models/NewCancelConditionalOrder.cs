using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class NewCancelConditionalOrder
    {
        public enum NewCancelConditionalOrderType
        {
            ORDER, CONDITIONAL_ORDER
        }

        [JsonPropertyName("typr")]
        [Required]
        public NewCancelConditionalOrderType Type { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }
    }


}

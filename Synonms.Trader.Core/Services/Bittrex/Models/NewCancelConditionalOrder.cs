using System;
using System.ComponentModel.DataAnnotations;

namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class NewCancelConditionalOrder
    {
        public enum NewCancelConditionalOrderType
        {
            ORDER, CONDITIONAL_ORDER
        }

        [Required]
        public NewCancelConditionalOrderType Type { get; set; }

        public Guid Id { get; set; }
    }


}

using System;
using System.ComponentModel.DataAnnotations;

namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class Order
    {
        public enum OrderDirection
        { 
            BUY, SELL
        }

        public enum OrderType
        {
            LIMIT, MARKET, CEILING_LIMIT, CEILING_MARKET
        }

        public enum OrderTimeInForce
        {
            GOOD_TIL_CANCELLED, IMMEDIATE_OR_CANCEL, FILL_OR_KILL, POST_ONLY_GOOD_TIL_CANCELLED, BUY_NOW
        }

        public enum OrderStatus
        {
            OPEN, CLOSED
        }

        [Required]
        public Guid id { get; set; }

        [Required]
        public string MarketSymbol { get; set; }

        [Required]
        public OrderDirection Direction { get; set; }

        [Required]
        public OrderType Type { get; set; }

        public double Quantity { get; set; }

        public double Limit { get; set; }

        public double Ceiling { get; set; }

        [Required]
        public OrderTimeInForce TimeInForce { get; set; }

        public Guid ClientOrderId { get; set; }

        [Required]
        public double FillQuantity { get; set; }

        [Required]
        public double Commission { get; set; }

        [Required]
        public double Proceeds { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime ClosedAt { get; set; }

        public NewCancelConditionalOrder OrderToCancel { get; set; }
    }
}

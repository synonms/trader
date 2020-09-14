using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class Order
    {
        private double _quantity;
        private double _limit;
        private double _ceiling;
        private double _proceeds;
        private double _commission;
        private double _fillQuantity;
        private OrderDirection _direction;
        private OrderType _type;
        private OrderTimeInForce _timeInForce;
        private OrderStatus _status;

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

        /// <summary>
        /// Unique ID of this order, assigned by the service (always present) Note that this ID is completely unrelated to the optional ClientOrderId.
        /// </summary>
        [JsonPropertyName("id")]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Unique symbol of the market this order is being placed on (always present, matches the field in NewOrder)
        /// </summary>
        [JsonPropertyName("marketSymbol")]
        [Required]
        public string MarketSymbol { get; set; }

        /// <summary>
        /// Order direction (always present, matches the field in NewOrder)
        /// </summary>
        [JsonPropertyName("direction")]
        [Required]
        public string Direction
        {
            get
            {
                return _direction.ToString();
            }
            set
            {
                _direction = (OrderDirection)Enum.Parse(typeof(OrderDirection), value);
            }
        }

        /// <summary>
        /// Order type (always present, matches the field in NewOrder)
        /// </summary>
        [JsonPropertyName("type")]
        [Required]
        public string Type
        {
            get
            {
                return _type.ToString();
            }
            set
            {
                _type = (OrderType)Enum.Parse(typeof(OrderType), value);
            }
        }

        /// <summary>
        /// Quantity (optional, matches the field in NewOrder)
        /// </summary>
        [JsonPropertyName("quantity")]
        public string Quantity
        {
            get
            {
                return _quantity.ToString($"F8");
            }
            set
            {
                _quantity = double.Parse(value);
            }
        }

        /// <summary>
        /// Limit price (optional, matches the field in NewOrder)
        /// </summary>
        [JsonPropertyName("limit")]
        public string Limit
        {
            get
            {
                return _limit.ToString($"F8");
            }
            set
            {
                _limit = double.Parse(value);
            }
        }

        /// <summary>
        /// Ceiling(optional, matches the field in NewOrder)
        /// </summary>
        [JsonPropertyName("ceiling")]
        public string Ceiling
        {
            get
            {
                return _ceiling.ToString($"F8");
            }
            set
            {
                _ceiling = double.Parse(value);
            }
        }

        /// <summary>
        /// Time in force (always present, matches the field in NewOrder)
        /// </summary>
        [JsonPropertyName("timeInForce")]
        [Required]
        public string TimeInForce
        {
            get
            {
                return _timeInForce.ToString();
            }
            set
            {
                _timeInForce = (OrderTimeInForce)Enum.Parse(typeof(OrderTimeInForce), value);
            }
        }

        /// <summary>
        /// Client-provided identifier for advanced order tracking (optional, matches the field in NewOrder)
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public Guid ClientOrderId { get; set; }

        /// <summary>
        /// Fill quantity (always present, even when there is no fill)
        /// </summary>
        [JsonPropertyName("fillQuantity")]
        [Required]
        public string FillQuantity
        {
            get
            {
                return _fillQuantity.ToString($"F8");
            }
            set
            {
                _fillQuantity = double.Parse(value);
            }
        }

        /// <summary>
        /// Commission (always present, even when there is no fill)
        /// </summary>
        [JsonPropertyName("commission")]
        [Required]
        public string Commission
        {
            get
            {
                return _commission.ToString($"F8");
            }
            set
            {
                _commission = double.Parse(value);
            }
        }

        /// <summary>
        /// Proceeds (always present, even when there is no fill)
        /// </summary>
        [JsonPropertyName("proceeds")]
        [Required]
        public string Proceeds
        {
            get
            {
                return _proceeds.ToString($"F8");
            }
            set
            {
                _proceeds = double.Parse(value);
            }
        }


        /// <summary>
        /// Order status (always present)
        /// </summary>
        [JsonPropertyName("status")]
        [Required]
        public string Status
        {
            get
            {
                return _status.ToString();
            }
            set
            {
                _status = (OrderStatus)Enum.Parse(typeof(OrderStatus), value);
            }
        }

        /// <summary>
        /// Timestamp (UTC) of order creation (always present)
        /// </summary>
        [JsonPropertyName("createdAt")]
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Timestamp (UTC) of last order update (optional)
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Timestamp (UTC) when this order was closed (optional)
        /// </summary>
        [JsonPropertyName("closedAt")]
        public DateTime ClosedAt { get; set; }

        /// <summary>
        /// Conditional order to cancel if this order executes Note that this relationship is reciprocal.
        /// </summary>
        public NewCancelConditionalOrder OrderToCancel { get; set; }

        public override string ToString()
        {
            return $"{MarketSymbol} - {Direction} ({Status}): {Quantity} ({FillQuantity} filled) @ {Limit} = {Proceeds}.  Last updated {UpdatedAt}.";
        }
    }
}

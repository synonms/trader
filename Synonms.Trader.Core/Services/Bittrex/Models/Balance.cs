namespace Synonms.Trader.Core.Services.Bittrex.Models
{
    public class Balance
    {
        /// <summary>
        /// Unique ID for the currency this balance is associated with
        /// </summary>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Total amount
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Available amount
        /// </summary>
        public double Available { get; set; }

        /// <summary>
        /// Time stamp when this balance was last updated
        /// </summary>
        public string UpdatedAt { get; set; }

        public override string ToString()
        {
            return $"{CurrencySymbol} - {Available} available of {Total}.  Last updated {UpdatedAt}."; 
        }
    }
}

using System.Runtime.Serialization;

namespace RentMyWrox.Models
{
    public class ShoppingCartSummary
    {
        public int Quantity { get; set; }

        public double TotalValue { get; set; }

        public string UserDisplayName { get; set; }
    }
}
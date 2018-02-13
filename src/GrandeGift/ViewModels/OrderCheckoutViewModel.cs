using System.Collections.Generic;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class OrderCheckoutViewModel
    {
        public IEnumerable<OrderLine> OrderLines { get; set; }
        public double TotalPrice { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
    }
}

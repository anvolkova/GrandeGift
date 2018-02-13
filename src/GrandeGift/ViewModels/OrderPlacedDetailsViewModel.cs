using System.Collections.Generic;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class OrderPlacedDetailsViewModel
    {
        public IEnumerable<OrderLine> OrderLines { get; set; }
        public double TotalPrice { get; set; }
        public Address Address { get; set; }
    }
}

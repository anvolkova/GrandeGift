using System.Collections.Generic;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class OrderPlacedViewModel
    {
        public IEnumerable<Order> OrderList { get; set; }
        public int TotalOrders { get; set; }             
    }
}

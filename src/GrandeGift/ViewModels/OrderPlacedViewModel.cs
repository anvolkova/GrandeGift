using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class OrderPlacedViewModel
    {
        public IEnumerable<Order> OrderList { get; set; }
        public int TotalOrders { get; set; }             
    }
}

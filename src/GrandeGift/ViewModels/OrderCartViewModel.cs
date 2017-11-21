using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class OrderCartViewModel
    {     
        public IEnumerable <OrderLine> OrderLines { get; set; }        
        public double TotalPrice { get; set; }
    }
}

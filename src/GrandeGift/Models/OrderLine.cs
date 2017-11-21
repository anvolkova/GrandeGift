using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public int OrderId { get; set; }
        public int HamperId { get; set; }    
        public Hamper Hamper { get; set; }    
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string HamperName { get; set; }
    }
}

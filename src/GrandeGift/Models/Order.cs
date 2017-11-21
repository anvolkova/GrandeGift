using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.Models
{
    public class Order
    {
        public enum OrderStatus { Cart, Placed };
        public int OrderId { get; set; }        
        public ICollection<OrderLine> OrderLines { get; set; }
        public string Username { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class AddressListViewModel
    {
        public IEnumerable<Address> Addresses { get; set; }
    }
}

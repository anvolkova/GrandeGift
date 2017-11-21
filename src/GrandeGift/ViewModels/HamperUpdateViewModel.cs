using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class HamperUpdateViewModel
    {
        public int HamperId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public bool Hidden { get; set; }
    }
}

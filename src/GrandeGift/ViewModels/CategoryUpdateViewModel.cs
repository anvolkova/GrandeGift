using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGift.ViewModels
{
    public class CategoryUpdateViewModel
    {        
        public string Name { get; set; }
        public string Details { get; set; }
        public string Picture { get; set; }
        public int CategoryId { get; set; }
    }
}

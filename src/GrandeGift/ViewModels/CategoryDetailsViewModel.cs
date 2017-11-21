using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrandeGift.Models;

namespace GrandeGift.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public int Total { get; set; } //total number of hampers in category    
        public string Name { get; set; }
        public string Details { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; } 
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }       
    }
}

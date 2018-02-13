using System.Collections.Generic;

namespace GrandeGift.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Picture { get; set; }
        public ICollection<Hamper> Hampers { get; set; }
    }
}

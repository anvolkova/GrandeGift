using System.ComponentModel.DataAnnotations;

namespace GrandeGift.ViewModels
{
    public class CategoryUpdateViewModel
    {        
        [Required, MaxLength(256)]
        public string Name { get; set; }
        public string Details { get; set; }
        public string Picture { get; set; }
        public int CategoryId { get; set; }
    }
}

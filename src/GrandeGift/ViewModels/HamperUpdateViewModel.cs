using System.ComponentModel.DataAnnotations;

namespace GrandeGift.ViewModels
{
    public class HamperUpdateViewModel
    {
        public int HamperId { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public string Details { get; set; }
        public string Picture { get; set; }
        [Required]
        public double Price { get; set; }
        public bool Hidden { get; set; }
    }
}

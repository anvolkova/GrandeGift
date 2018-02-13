using System.ComponentModel.DataAnnotations;

namespace GrandeGift.ViewModels
{
    public class AddressUpdateViewModel
    {
        public int AddressId { get; set; }
        [Required, MaxLength(256)]
        public string Name { get; set; }
        [Required, MaxLength(256)]
        public string Street { get; set; }
        [Required, MaxLength(256)]
        public string City { get; set; }
        [Required, MaxLength(256)]
        public string Region { get; set; }
        [Required, MaxLength(256)]
        public string Country { get; set; }
        [Required, DataType(DataType.PostalCode)]
        public double PostCode { get; set; }
    }
}

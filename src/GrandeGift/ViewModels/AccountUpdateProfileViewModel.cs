using System.ComponentModel.DataAnnotations;

namespace GrandeGift.ViewModels
{
    public class AccountUpdateProfileViewModel
    {
        public int ProfileId { get; set; }
        [Required, MaxLength(256)]
        public string FirstName { get; set; }
        [Required, MaxLength(256)]
        public string LastName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}

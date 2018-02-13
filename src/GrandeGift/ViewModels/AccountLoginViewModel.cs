using System.ComponentModel.DataAnnotations;

namespace GrandeGift.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}

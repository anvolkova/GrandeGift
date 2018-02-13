namespace GrandeGift.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public double PostCode { get; set; }
        public int Removed { get; set; }
    }
}

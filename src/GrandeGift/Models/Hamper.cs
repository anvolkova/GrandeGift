namespace GrandeGift.Models
{
    public class Hamper
    {
        public int HamperId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Picture { get; set; }       
        public double Price { get; set; }
        public int Hidden { get; set; }
        // one category - many products
        public int CategoryId { get; set; }
    }
}

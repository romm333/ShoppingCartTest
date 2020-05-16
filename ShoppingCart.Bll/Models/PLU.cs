using ShoppingCart.Bll.Enums;

namespace ShoppingCart.Bll.Models
{
    public class PLU
    {
        public EPluType PluType { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

namespace CTRLE_Handel_oev_identity.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }  // Kategorin kan ha många produkter
    }
}

namespace CTRLE_Handel_oev_identity.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }

        // Navigation property
        public Category Category { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}

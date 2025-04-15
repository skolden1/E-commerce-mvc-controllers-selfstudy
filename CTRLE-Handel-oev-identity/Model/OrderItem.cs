namespace CTRLE_Handel_oev_identity.Model
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }  // Primärnyckel
        public int OrderId { get; set; }  // Relaterad beställning
        public Order Order { get; set; }  // Navigeringsegenskap till Order

        public int ProductId { get; set; }  // Relaterad produkt
        public Product Product { get; set; }  // Navigeringsegenskap till Product

        public int Quantity { get; set; }  // Antalet produkter i ordern
        public decimal Price { get; set; }  // Pris på produkten vid köp
    }
}

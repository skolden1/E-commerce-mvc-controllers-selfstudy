using CTRLE_Handel_oev_identity.Areas.Identity.Data;

namespace CTRLE_Handel_oev_identity.Model
{
    public class Order
    {
        public int OrderId { get; set; }  // Primärnyckel
        public string UserId { get; set; }  // Referens till användaren som gjort beställningen (kan vara FK)
        public DateTime OrderDate { get; set; }  // När ordern skapades
        public decimal TotalAmount { get; set; }  // Totalt belopp för beställningen

        // Navigeringsegenskap till användaren (relaterar till IdentityUserTable)
        public IdentityUserTable User { get; set; }

        // Navigeringsegenskap till orderitems
        public List<OrderItem> OrderItems { get; set; }  // En order kan innehålla flera OrderItems
    }
}

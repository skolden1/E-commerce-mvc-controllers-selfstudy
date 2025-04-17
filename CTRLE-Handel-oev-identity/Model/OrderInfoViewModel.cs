namespace CTRLE_Handel_oev_identity.Model
{
    public class OrderInfoViewModel
    {
        public int OrderInfoViewModelId { get; set; }
        public string FullName { get; set; }
        public string CardNumber { get; set; }
        public List<Cart> CartItems { get; set; } 
    }
}

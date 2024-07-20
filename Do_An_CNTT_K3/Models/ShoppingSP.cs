namespace Do_An_CNTT_K3.Models
{
    public class ShoppingSP
    {
        public int Id { get; set; }
        public InformationSP? InformationSP { get; set; }
        public int Qty { get; set; }
        public string? ShoppingCartId { get; set; }
        public decimal TotalPrice => InformationSP != null ? InformationSP.GiaTien * Qty : 0;
    }
}

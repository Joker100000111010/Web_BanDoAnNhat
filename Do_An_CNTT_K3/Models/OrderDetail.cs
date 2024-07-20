namespace Do_An_CNTT_K3.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public InformationSP? Product { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductName { get; set; }
    }
}

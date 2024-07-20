namespace Do_An_CNTT_K3.Models.Interfaces
{
    public interface IShoppingProduct
    {
        void AddToCart(InformationSP product);
        int RemoveFromCart(InformationSP product);
        List<ShoppingSP> GetAllShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingSP> ShoppingCartItems { get; set; }
        int GetQuantity(int productId);
    }
}

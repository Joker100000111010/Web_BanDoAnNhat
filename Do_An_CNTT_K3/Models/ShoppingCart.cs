namespace Do_An_CNTT_K3.Models
{
    public class ShoppingCart
    {
        public List<CartItem> CartItems { get; set; }

        // Constructor để khởi tạo giỏ hàng
        public ShoppingCart()
        {
            CartItems = new List<CartItem>();
        }

        // Phương thức để thêm một mục vào giỏ hàng
        public void AddItemToCart(CartItem item)
        {
            CartItems.Add(item);
        }

        // Phương thức để xóa một mục khỏi giỏ hàng
        public void RemoveItemFromCart(CartItem item)
        {
            CartItems.Remove(item);
        }

        // Phương thức để chuyển thông tin từ giỏ hàng sang đơn hàng
        public Order ConvertToOrder()
        {
            Order order = new Order();

            foreach (var cartItem in CartItems)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    // Gán các thuộc tính khác của sản phẩm vào OrderDetail nếu cần
                };

                order.OrderDetails.Add(orderDetail);
            }

            return order;
        }

        // Phương thức để xóa các mục đã mua khỏi giỏ hàng sau khi đặt hàng
        public void ClearCart()
        {
            CartItems.Clear();
            // Có thể thực hiện các xử lý khác như lưu giỏ hàng sau khi xóa vào cơ sở dữ liệu nếu cần
        }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        // Các thuộc tính khác của sản phẩm có thể được thêm vào nếu cần
    }

}

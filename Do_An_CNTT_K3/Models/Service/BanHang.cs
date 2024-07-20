using Do_An_CNTT_K3.Data;
using Do_An_CNTT_K3.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Do_An_CNTT_K3.Models.Service
{
    public class BanHang : IBanHang
    {
        private FoodDbContext dbContext;
        private IShoppingProduct banHang;

        public BanHang(FoodDbContext dbContext, IShoppingProduct banHang)
        {
            this.dbContext = dbContext;
            this.banHang = banHang;
        }

        public void PlaceOrder(Order order)
        {
            // Lấy thông tin các mặt hàng trong giỏ hàng
            var shopping = banHang.GetAllShoppingCartItems();
            order.OrderDetails = new List<OrderDetail>();

            // Tạo OrderDetail cho mỗi mặt hàng trong giỏ hàng
            foreach (var shop in shopping)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = shop.Qty,
                    ProductId = shop.InformationSP.IdSP,
                    Price = shop.InformationSP.GiaTien // Ensure you add Price to OrderDetail
                };

                order.OrderDetails.Add(orderDetail);
            }

            // Thiết lập thời gian đặt hàng và tổng giá trị đơn hàng
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = banHang.GetShoppingCartTotal();

            // Lưu đơn hàng vào cơ sở dữ liệu
            dbContext.orders.Add(order);
            dbContext.SaveChanges();
        }
    }
}

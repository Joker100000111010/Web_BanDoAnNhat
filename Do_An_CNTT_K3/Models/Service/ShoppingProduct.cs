using Do_An_CNTT_K3.Data;
using Do_An_CNTT_K3.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Do_An_CNTT_K3.Models.Service
{
    public class ShoppingProduct : IShoppingProduct
    {
        private readonly FoodDbContext dbContext;
        public List<ShoppingSP>? ShoppingCartItems { get; set; }
        public string? ShoppingCartId { get; set; }

        public ShoppingProduct(FoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public static ShoppingProduct GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<FoodDbContext>() ?? throw new Exception("Error initializing FoodDbContext");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingProduct(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(InformationSP product)
        {
            var shoppingCartItem = dbContext.shoppingSPs.FirstOrDefault(s => s.InformationSP.IdSP == product.IdSP && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingSP
                {
                    ShoppingCartId = ShoppingCartId,
                    InformationSP = product,
                    Qty = 1,
                };
                dbContext.shoppingSPs.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }

            // Cập nhật giá sản phẩm
            shoppingCartItem.InformationSP.GiaTien = product.GiaTien;

            dbContext.SaveChanges();

            // Cập nhật lại tổng giá sau khi thêm sản phẩm vào giỏ hàng
            GetShoppingCartTotal();
        }

        public void ClearCart()
        {
            var cartItems = dbContext.shoppingSPs.Where(s => s.ShoppingCartId == ShoppingCartId);
            dbContext.shoppingSPs.RemoveRange(cartItems);
            dbContext.SaveChanges();
        }

        public List<ShoppingSP> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ??= dbContext.shoppingSPs.Where(s => s.ShoppingCartId == ShoppingCartId).Include(p => p.InformationSP).ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            return dbContext.shoppingSPs.Where(s => s.ShoppingCartId == ShoppingCartId)
          .Select(s => s.InformationSP.GiaTien * s.Qty).Sum();
        }

        public int RemoveFromCart(InformationSP product)
        {
            var shoppingCartItem = dbContext.shoppingSPs.FirstOrDefault(s => s.InformationSP.IdSP == product.IdSP && s.ShoppingCartId == ShoppingCartId);
            var quantity = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    dbContext.shoppingSPs.Remove(shoppingCartItem);
                }
                dbContext.SaveChanges();
            }
            return quantity;
        }



        public int GetQuantity(int productId)
        {
            var shoppingCartItem = dbContext.shoppingSPs.FirstOrDefault(s => s.InformationSP.IdSP == productId && s.ShoppingCartId == ShoppingCartId);
            return shoppingCartItem?.Qty ?? 0;
        }
    }
}

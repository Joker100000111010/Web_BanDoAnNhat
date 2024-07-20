using Do_An_CNTT_K3.Models;
using Do_An_CNTT_K3.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Do_An_CNTT_K3.Controllers
{
    [Authorize]
    public class BanHangController : Controller
    {
        private readonly IBanHang _orderRepository;
        private readonly IShoppingProduct _shoppingCartRepository;

        public BanHangController(IBanHang orderRepository, IShoppingProduct shoppingCartRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }
        [Authorize(Roles = "User,Admin")]
        public IActionResult Checkout()
        {
            var cartItems = _shoppingCartRepository.GetAllShoppingCartItems();
            var order = new Order
            {
                OrderDetails = cartItems.Select(item => new OrderDetail
                {
                    ProductId = item.InformationSP.IdSP,
                    Quantity = item.Qty,
                    Price = item.InformationSP.GiaTien,
                    ProductImage = item.InformationSP.Image1,
                    ProductName = item.InformationSP.TenSP
                }).ToList()
            };
            order.OrderTotal = _shoppingCartRepository.GetShoppingCartTotal();

            return View(order);
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderPlaced = DateTime.Now;
                _orderRepository.PlaceOrder(order);
                _shoppingCartRepository.ClearCart();
                HttpContext.Session.SetInt32("CartCount", 0);
                return RedirectToAction("CheckoutComplete");
            }
            return View(order);
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}

using Do_An_CNTT_K3.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Do_An_CNTT_K3.Controllers
{
    public class MuaHangController : Controller
    {
        private readonly IShoppingProduct _shoppingProduct;
        private readonly IThongTinSanPham _thongTinSanPham;
        private readonly ILogger<MuaHangController> _logger;

        public MuaHangController(IShoppingProduct shoppingProduct, IThongTinSanPham thongTinSanPham, ILogger<MuaHangController> logger)
        {
            _shoppingProduct = shoppingProduct;
            _thongTinSanPham = thongTinSanPham;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var items = _shoppingProduct.GetAllShoppingCartItems();
            _shoppingProduct.ShoppingCartItems = items;
            ViewBag.TotalCart = _shoppingProduct.GetShoppingCartTotal();
            return View(items);
        }

        public RedirectToActionResult AddToShoppingCart(int pId)
        {
            var product = _thongTinSanPham.GetAllProducts().FirstOrDefault(p => p.IdSP == pId);
            if (product != null)
            {
                _shoppingProduct.AddToCart(product);
                int cartCount = _shoppingProduct.GetAllShoppingCartItems().Count();
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            else
            {
                _logger.LogWarning($"Product with ID {pId} not found.");
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int pId)
        {
            var product = _thongTinSanPham.GetAllProducts().FirstOrDefault(p => p.IdSP == pId);
            if (product != null)
            {
                _shoppingProduct.RemoveFromCart(product);
                int cartCount = _shoppingProduct.GetAllShoppingCartItems().Count();
                HttpContext.Session.SetInt32("CartCount", cartCount);
            }
            else
            {
                _logger.LogWarning($"Product with ID {pId} not found.");
            }

            return RedirectToAction("Index");
        }


    }
}

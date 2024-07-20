using Do_An_CNTT_K3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Do_An_CNTT_K3.Models.Interfaces;
using Microsoft.Extensions.Logging;

namespace Do_An_CNTT_K3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhanQuyen : Controller
    {
        private readonly IThongTinSanPham _ithongtinsanpham;
        private readonly ILogger<PhanQuyen> _logger;

        public PhanQuyen(IThongTinSanPham ithongtinsanpham, ILogger<PhanQuyen> logger)
        {
            _ithongtinsanpham = ithongtinsanpham;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult QuanLy()
        {
            try
            {
                var products = _ithongtinsanpham.GetAllProducts(); // Lấy danh sách sản phẩm
                var posters = _ithongtinsanpham.Poster(); // Lấy danh sách bài viết

                var viewModel = new HomePage
                {
                    Products = products,
                    Posters = posters,
                };
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard");
                return View("Error"); // Assuming you have an error view
            }
        }

        // Hiển thị form để thêm sản phẩm mới
        [HttpGet]
        public IActionResult CreateInformation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInformation(InformationSP product)
        {
            if (ModelState.IsValid)
            {
                // Khởi tạo danh sách DanhGias nếu nó bị null
                if (product.DanhGias == null)
                {
                    product.DanhGias = new List<DanhGia>();
                }

                _ithongtinsanpham.AddProduct(product);
                return RedirectToAction("QuanLy");
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _ithongtinsanpham.GetProductDetail(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InformationSP product)
        {
            if (ModelState.IsValid)
            {
                _ithongtinsanpham.UpdateProduct(product);
                return RedirectToAction("QuanLy");
            }
            return View(product);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _ithongtinsanpham.GetProductDetail(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _ithongtinsanpham.DeleteProduct(id);
            return RedirectToAction("QuanLy");
        }


    }
}

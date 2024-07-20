using Do_An_CNTT_K3.Data;
using Do_An_CNTT_K3.Models;
using Do_An_CNTT_K3.Models.Interfaces;
using Do_An_CNTT_K3.Models.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Do_An_CNTT_K3.Controllers
{
    public class SanPhamController : Controller
    {
        public FoodDbContext _context;
        public IThongTinSanPham _ithongtinsanpham;
        private readonly ILogger<SanPhamController> _logger;
        public SanPhamController(IThongTinSanPham ithongtinsanpham, FoodDbContext foodDbContext, ILogger<SanPhamController> logger)
        {
            _ithongtinsanpham = ithongtinsanpham;
            _context = foodDbContext;
            _logger = logger;
        }
        public IActionResult TrangChu()
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
                _logger.LogError(ex, "Error loading home page");
                return View("Error"); // Assuming you have an error view
            }
        }

        [HttpPost]
        public ActionResult UpdateViewCount(int postId)
        {
            try
            {
                var post = _context.baiViets.FirstOrDefault(p => p.IdBV == postId);
                if (post != null)
                {
                    post.Luotxem++;
                    _context.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating view count for post ID {postId}", postId);
                return Json(new { success = false, message = "An error occurred while updating view count." });
            }
        }

        [HttpPost]
        public ActionResult UpdatePostTime(int postId, DateTime newTime)
        {
            Console.WriteLine($"Received postId: {postId}, newTime: {newTime}"); // Log để kiểm tra dữ liệu nhận

            var post = _context.baiViets.FirstOrDefault(p => p.IdBV == postId);

            if (post != null)
            {
                post.ThoiGianBaiViet = newTime;
                _context.SaveChanges();
                Console.WriteLine("Update successful."); // Log để xác nhận cập nhật thành công
                return Json(new { success = true });
            }
            else
            {
                Console.WriteLine("Post not found."); // Log để xác nhận không tìm thấy post
                return Json(new { success = false });
            }
        }
        public IActionResult ThongTinBaiViet(int id)
        {
            try
            {
                var poster = _ithongtinsanpham.PosterDetail(id);
                var posters = _ithongtinsanpham.Poster(); // Lấy danh sách bài viết

                // Tách từ khóa từ tiêu đề của bài viết chính
                var keywords = poster.TieuDe.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                            .Select(k => k.ToLower())
                                            .ToList();

                // Lọc các bài viết liên quan dựa trên các từ khóa
                var relatedPosters = posters.Where(p => p.IdBV != poster.IdBV &&
                                                        keywords.Any(k => p.TieuDe.ToLower().Contains(k)))
                                            .OrderByDescending(p => p.Luotxem)
                                            .Take(3)
                                            .ToList();

                var viewModel = new Promo
                {
                    BaiViets = poster,
                    Posters = relatedPosters // Danh sách bài viết liên quan
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Promo");
                return View("Error"); // Assuming you have an error view
            }

            return NotFound();
        }




        public IActionResult MoTaSP(int id)
    {
        var products = _ithongtinsanpham.GetAllProducts(); // Lấy danh sách sản phẩm
        var mota = _ithongtinsanpham.GetProductDetail(id);
        var danhGias = _ithongtinsanpham.GetDanhGiasByProductId(id); // Lấy danh sách bình luận liên quan đến sản phẩm

        var viewModel = new HomePage
        {
            Products = products,
            mota = mota,
            DanhGias = danhGias
        };

        return View(viewModel);
    }
        [HttpPost]
        public IActionResult BinhLuan(DanhGia binhLuan)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem ProductId có hợp lệ không
                var productExists = _ithongtinsanpham.GetProductDetail(binhLuan.InformationSPIdSP) != null;
                if (!productExists)
                {
                    ModelState.AddModelError("", "ProductId không hợp lệ.");
                }
                else
                {
                    // Log giá trị InformationSPIdSP
                    Console.WriteLine("InformationSPIdSP: " + binhLuan.InformationSPIdSP);
                    _ithongtinsanpham.AddBinhLuan(binhLuan);
                    return RedirectToAction("MoTaSP", new { id = binhLuan.InformationSPIdSP });
                }
            }

            // Nếu ModelState không hợp lệ hoặc ProductId không hợp lệ, nạp lại model HomePage
            var products = _ithongtinsanpham.GetAllProducts(); // Lấy danh sách sản phẩm
            var mota = _ithongtinsanpham.GetProductDetail(binhLuan.InformationSPIdSP);
            var danhGias = _ithongtinsanpham.GetDanhGiasByProductId(binhLuan.InformationSPIdSP); // Lấy danh sách bình luận liên quan đến sản phẩm

            var viewModel = new HomePage
            {
                Products = products,
                mota = mota,
                DanhGias = danhGias
            };

            return View("MoTaSP",viewModel);
        }

        public IActionResult SeaFood()
        {
            return View(_ithongtinsanpham.GetAllSeafood());
        }
        public IActionResult INSTRUCTIONS()
        {
            return View();
        }
        public IActionResult Search()
        {
            var ThongTinsp = _ithongtinsanpham.GetAllProducts();
            if (ThongTinsp != null)
            {
                var viewModel = new TimKiem
                {
                    ThongTinSanPham = ThongTinsp
                };

                return View(viewModel);
            }
            return NotFound();
        }

    }
}

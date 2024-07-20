using Do_An_CNTT_K3.Models;
using Do_An_CNTT_K3.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Do_An_CNTT_K3.Controllers
{
    public class LienLacController : Controller
    {
        private readonly ILienLacRepository _commentRepository;

        public LienLacController(ILienLacRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitBinhLuan(Contact binhLuan)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("trankimlongk19@gmail.com");
                    mail.To.Add("longloivinh123@gmail.com");
                    mail.Subject = "Shopping Contact Message";
                    mail.IsBodyHtml =true;
                    mail.Body = $"Tên: {binhLuan.Ten}<br>Email: {binhLuan.Email}<br>Tên Món Ăn: {binhLuan.TenMonAn}<br>Bình Luận: {binhLuan.Message}";

                    // Tạo đối tượng SmtpClient và thiết lập thông tin máy chủ SMTP của Gmail
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                    // Sử dụng thông tin xác thực của bạn (email và mật khẩu ứng dụng)
                    smtpClient.Credentials = new NetworkCredential("longloivinh123@gmail.com", "vhegjnweqamqwedr");
                    smtpClient.EnableSsl = true; // Bật SSL để bảo mật

                    smtpClient.Send(mail);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Gửi email không thành công: " + ex.Message);
                    return View("Contact", binhLuan);
                }

                // Chuyển hướng hoặc hiển thị thông điệp thành công
                return RedirectToAction("ContactSuccess");
            }

            // Nếu ModelState không hợp lệ, quay lại trang liên hệ
            return View("Contact", binhLuan);
        }

        public IActionResult ContactSuccess()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}

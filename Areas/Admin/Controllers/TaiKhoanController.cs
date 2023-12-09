using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Models;


namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiKhoanController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        public TaiKhoanController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [Route("dang-nhap", Name ="NameDangNhap")]
        public IActionResult DangNhap()
        {
            return View();
        }

        [Route("dang-nhap")]
        [HttpPost]
        public IActionResult DangNhap(InputDangNhap input)
        {
            string mk=EncryptPassword(input.Password);
            var item = _context.NguoiDungs.FirstOrDefault(c => c.Email == input.Email && c.MatKhau == mk);
            if(item != null)
            {
                var xt=_context.XacThucs.FirstOrDefault(c=>c.IdNguoiDung==item.IdNguoiDung);
                if (xt.TrangThai==1)
                {
                    Response.Cookies.Append("Id", item.IdNguoiDung);
                    Response.Cookies.Append("Role", item.IdQuyen);
                    Response.Cookies.Append("UserName", item.Ten);
                    return CheckRole(item.IdQuyen);
                }
                if(xt.TrangThai==2)
                {
                    TempData["error"] = "Tài khoản của bạn đã bị chặn";
                    return View();
                }
                if(xt.TrangThai==0)
                {
                    SendOTP(input.Email, item.IdNguoiDung);
                    
                    return RedirectToAction("XacThuc", "TaiKhoan", new { Areas = "Admin", id=item.IdNguoiDung});
                   
                }   
            }
            TempData["error"] = "Email hoặc mật khẩu không đúng";
            return View();
        }
        private string EncryptPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                var data = Encoding.UTF8.GetBytes(password);
                var hash = md5.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }
        private IActionResult CheckRole(string role)
        {
            if (role == "Admin") return RedirectToAction("DanhSachThongTinWeb", "QuanLyThongTinWeb", new { Areas = "Admin" });
            if (role == "User") return RedirectToAction("Home", "Home", new { Areas = "" });
            return Redirect("/");
        }
        [Route("dang-xuat", Name ="dangxuat")]
        public IActionResult DangXuat()
        {
            Response.Cookies.Delete("UserName");
            Response.Cookies.Delete("Role");
            Response.Cookies.Delete("Id");
            return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin" });
        }
        [Route("dang-ky")]
        public IActionResult DangKy()
        {
            return View();
        }
        [Route("dang-ky")]
        [HttpPost]
        public IActionResult DangKy(InputDangKy input)
        {
            if(ModelState.IsValid)
            {

                var item=_context.NguoiDungs.FirstOrDefault(c=>c.Email==input.Email);
                if (item == null)
                {
                    string id= Guid.NewGuid().ToString();
                    var nguoidung = new NguoiDung();
                    nguoidung.IdNguoiDung = id;
                    nguoidung.Email = input.Email;
                    nguoidung.Ten = input.UserName;
                    nguoidung.MatKhau = EncryptPassword(input.Password);
                    nguoidung.IdQuyen = "User";
                    var xacthuc = new XacThuc();
                    xacthuc.IdNguoiDung = id;
                    xacthuc.TrangThai = 0;
                    _context.NguoiDungs.Add(nguoidung);
                    _context.XacThucs.Add(xacthuc);
                    _context.SaveChanges();
                    SendOTP(input.Email, id);
                    return RedirectToAction("XacThuc", "TaiKhoan", new { id=id });
                }
                if(item!=null)
                {
                    var xacthuc2 = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                    if(xacthuc2.TrangThai==2)
                    {
                        ViewBag.error = "Tài khoản của bạn đã bị chặn";
                        return View();
                    }
                    if(xacthuc2.TrangThai==1||xacthuc2.TrangThai==0)
                    {
                        TempData["error"] = "Email này đã được đăng ký";
                        return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin" });
                    }
                }
                else 
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }  
            }
            return View();
        }
        [Route("Quen-mat-khau")]
        public IActionResult QuenMatKhau()
        {
            return View();

        }
        [Route("Quen-mat-khau")]
        [HttpPost]
        public IActionResult QuenMatKhau(string email)
        {
            var nguoidung=_context.NguoiDungs.FirstOrDefault(c=>c.Email == email);
            if(nguoidung==null)
            {
                TempData["error"] = "Email này chưa được đăng ký";
                return View();
            }
            SendOTP(nguoidung.Email, nguoidung.IdNguoiDung);
            return RedirectToAction("QuenMatKhau2", "TaiKhoan", new { Areas = "Admin", id = nguoidung.IdNguoiDung });
        }
        [Route("quen-mat-khau-2")]
        public IActionResult QuenMatKhau2(string id)
        {
            var item = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == id);
            return View(item);
        }
        [Route("quen-mat-khau-2")]
        [HttpPost]
        public IActionResult QuenMatKhau2(InputOTP input)
        {
            if(ModelState.IsValid)
            {
                var item = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == input.IdNguoiDung);
                if(input.MaXacThuc==item.MaXacThuc)
                {
                    return RedirectToAction("DoiLaiMatKhau", "TaiKhoan", new { Areas = "Admin", id=item.IdNguoiDung });
                }
                    TempData["error"] = "Mã xác thực không đúng";
                    return View();
                
            }
            TempData["error"] = "Vui lòng nhập mã xác thực";
            return View();
        }
        [Route("Doi-lai-mat-khau")]
        public IActionResult DoiLaiMatKhau(string id)
        {
            var nguoidung=_context.NguoiDungs.FirstOrDefault(c=>c.IdNguoiDung==id);
            return View(nguoidung);
        }
        [Route("Doi-lai-mat-khau")]
        [HttpPost]
        public IActionResult DoiLaiMatKhau(DoiMatKhau input)
        {
            if (ModelState.IsValid)
            {
                var nguoidung=_context.NguoiDungs.FirstOrDefault(c=>c.IdNguoiDung==input.IdNguoiDung);
                nguoidung.MatKhau = EncryptPassword(input.MatKhauMoi);
                _context.NguoiDungs.Update(nguoidung);
                _context.SaveChanges();
                return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin" });
            }
            TempData["error"] = "Vui lòng nhập mật khẩu";
            return View();
        }
        [Route("xac-thuc")]
        public IActionResult XacThuc(string id)
        {
            var item = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == id);
            if(item!=null)
            {
            return View(item);
            }
            return View();
        }
        [Route("xac-thuc")]
        [HttpPost]
        public IActionResult XacThuc(InputOTP input)
        {
            if(ModelState.IsValid)
            {
                var item = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == input.IdNguoiDung);
                if (input.MaXacThuc == item.MaXacThuc)
                {
                    item.TrangThai = 1;
                    _context.Update(item);
                    _context.SaveChanges();
                    return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin" });
                }
                else
                {
                    TempData["error"] = "Mã OTP của bạn không đúng";
                    return View();
                }

            }
            TempData["erroe"] = "Vui lòng nhập OTP";
            return View();
        }
        [HttpPost]
        public IActionResult SendOTP(string email, string id)
        {
            // Tạo mã OTP ngẫu nhiên
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            var xacthuc = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == id);
            xacthuc.MaXacThuc = otp.ToString();
            _context.Update(xacthuc);
            _context.SaveChanges();

            // Đường dẫn tới file hình ảnh footer trong máy của bạn
            string imagePath = "D:\\Web\\QuanLyKhachSan\\wwwroot\\images\\MerPerle.png";

            // Tạo mẫu email HTML với hình ảnh nhúng
            string emailTemplate = @"
<html>
<body style='width:100%'>
    <h1>OTP Verification</h1>
    <p>Your OTP is: " + otp + @"</p>
<footer style='width:100%'>
    <img src='cid:footerImage' style='width:100%; height:100%; object-fit:contain' alt='Footer Image'>
</footer>
</body>
</html>";

            // Gửi email chứa mã OTP và ảnh footer
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("khachsanminhminhueh@gmail.com", "vryudjggtixnykjb");
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("khachsanminhminhueh@gmail.com");
                    mailMessage.To.Add(email);
                    mailMessage.Subject = "OTP Verification";
                    mailMessage.Body = emailTemplate;
                    mailMessage.IsBodyHtml = true;

                    // Đính kèm hình ảnh
                    Attachment imageAttachment = new Attachment(imagePath);
                    imageAttachment.ContentId = "footerImage";
                    mailMessage.Attachments.Add(imageAttachment);

                    smtpClient.Send(mailMessage);
                }

                return Ok("OTP sent successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to send OTP: {ex.Message}");
            }
        }
    }
}

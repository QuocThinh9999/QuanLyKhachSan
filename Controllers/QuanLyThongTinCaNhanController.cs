using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Models.CapNhatNguoiDung;

namespace QuanLyKhachSan.Controllers
{
    
    public class QuanLyThongTinCaNhanController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuanLyThongTinCaNhanController(DbQuanLyKhachSanContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        [Route("cap-nhat-thong-tin-ca-nhan")]
        public IActionResult CapNhatThongTinCaNhan()
        {
            var httpcontext = _httpContextAccessor.HttpContext;
            var nguoidung = _context.NguoiDungs.FirstOrDefault(c => c.IdQuyen == "User" && c.IdNguoiDung == httpcontext.Request.Cookies["Id"]);
           if(nguoidung == null) 
            {
                return Redirect("/dang-nhap");
            }
            return View(nguoidung);
        }
        [Route("cap-nhat-thong-tin-ca-nhan")]
        [HttpPost]
        public IActionResult CapNhatThongTinCaNhan(capnhatnguoidung input)
        {
            var httpcontext = _httpContextAccessor.HttpContext;
            var nguoidung = _context.NguoiDungs.FirstOrDefault(c => c.IdQuyen == "User" && c.IdNguoiDung == httpcontext.Request.Cookies["Id"]);
            if (nguoidung == null)
            {
                return Redirect("/dang-nhap");
            }
            if (input.ten == null)
            {
                TempData["error"] = "Không được bỏ trống tên!";
                return View(nguoidung);
            }
            nguoidung.Ten = input.ten;
            nguoidung.SoDienThoai = input.sdt;
            nguoidung.GioiTinh = input.gioitinh;
            nguoidung.NgaySinh=input.ngaysinh;
            _context.Update(nguoidung);
            _context.SaveChanges();
            return View(nguoidung);
        }
    }
}

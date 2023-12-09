using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Models
{
    public class Auth : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Auth(DbQuanLyKhachSanContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult CheckAuthentication()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context.Request.Cookies["Id"] != "d07ce7c7-666a-4a98-a2ff-85dcca77479c")
            {
                return RedirectToAction("DangXuat", "TaiKhoan", new { Areas = "Admin" });
            }

            return null;
        }
    }
}
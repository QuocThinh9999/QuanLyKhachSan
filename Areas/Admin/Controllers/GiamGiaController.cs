using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiamGiaController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public GiamGiaController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("danh-sach-giam-gia")]
        public IActionResult DanhSachGiamGia()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items = _context.Phongs.ToList();
            return View(items);
        }
        [Route("danh-sach-giam-gia")]
        [HttpPost]
        public IActionResult DanhSachGiamGia(string ten)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items=_context.Phongs.ToList();
            if(ten!=null)
            {
                items=_context.Phongs.Where(c=>c.TenPhong.Contains((ten?? "").ToLower())).ToList();
            }
            return View(items);
        }
        [Route("cap-nhat-giam-gia")]
        public IActionResult CapNhatGiamGia(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var item = _context.Phongs.Find(id);
            return View(item);
        }
        [Route("cap-nhat-giam-gia")]
        [HttpPost]
        public IActionResult CapNhatGiamGia(string id,GiamGia input)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var item= _context.Phongs.Find(id);
            item.GiaPhong=input.GiaPhong;
            item.GiaSauGiam=input.GiaSauGiam;
            _context.Phongs.Update(item);
            _context.SaveChanges();
            return RedirectToAction("DanhSachGiamGia", "GiamGia", new { Areas = "Admin" });
        }
    }
}

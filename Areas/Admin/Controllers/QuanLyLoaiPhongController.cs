using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Models;


namespace QuanLyKhachSan.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class QuanLyLoaiPhongController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public QuanLyLoaiPhongController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("danh-sach-loai-phong")]
        public IActionResult DanhSachLoaiPhong()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items = _context.LoaiPhongs.ToList();
            return View(items);
        }
        [Route("danh-sach-loai-phong")]
        [HttpPost]
        public IActionResult DanhSachLoaiPhong(string ten)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items = _context.LoaiPhongs.ToList();
            if (ten!=null)
            {

             items = _context.LoaiPhongs.Where(c=>c.TenLoaiPhong.Contains((ten ?? "").ToLower())).ToList();
            }

            return View(items);
        }
        [Route("xoa-loai-phong")]
        public IActionResult XoaLoaiPhong(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var loaiPhong = _context.LoaiPhongs.FirstOrDefault(k => k.IdLoaiPhong == id);

            var phongs = _context.Phongs.Where(p => p.IdLoaiPhong == id).ToList();
            foreach (var phong in phongs)
            {
                var chitietphong = _context.ChiTietPhongs.FirstOrDefault(ctp => ctp.IdPhong == phong.Id);
                _context.ChiTietPhongs.Remove(chitietphong);
                var hoadon = _context.HoaDons.Where(c => c.IdPhong == phong.Id).ToList();
                
                _context.HoaDons.RemoveRange(hoadon);
            }
            _context.Phongs.RemoveRange(phongs);
            _context.LoaiPhongs.Remove(loaiPhong);
            _context.SaveChanges();
            return RedirectToAction("DanhSachLoaiPhong", "QuanLyLoaiPhong", new { Areas = "Admin" });
        }
        [Route("them-loai-phong")]
        public IActionResult ThemLoaiPhong()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            return View();
        }
        [Route("them-loai-phong")]
        [HttpPost]
        public IActionResult ThemLoaiPhong(ThemLoaiPhong input)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            if (ModelState.IsValid)
            {
                var loaiphong = new LoaiPhong();
                loaiphong.IdLoaiPhong = Guid.NewGuid().ToString();
                loaiphong.TenLoaiPhong = input.TenLoaiPhong;
                _context.LoaiPhongs.Add(loaiphong);
                _context.SaveChanges();
                return RedirectToAction("DanhSachLoaiPhong", "QuanLyLoaiPhong", new { Areas = "Admin" });
            }
            return View();
        }
        [Route("cap-nhat-loai-phong")]
        public IActionResult CapNhatLoaiPhong(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var item = _context.LoaiPhongs.Find(id);
            return View(item);
        }
        [Route("cap-nhat-loai-phong")]
        [HttpPost]
        public IActionResult CapNhatLoaiPhong(string id, ThemLoaiPhong input)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var item = _context.LoaiPhongs.FirstOrDefault(c => c.IdLoaiPhong == id);
                item.TenLoaiPhong = input.TenLoaiPhong;
                _context.Update(item);
                _context.SaveChanges();
            return RedirectToAction("DanhSachLoaiPhong", "QuanLyLoaiPhong", new { Areas = "Admin" });
        }
    }
}

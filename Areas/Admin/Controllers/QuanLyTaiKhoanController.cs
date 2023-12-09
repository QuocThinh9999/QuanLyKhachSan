using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.TaiKhoanNguoiDung;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyTaiKhoanController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public QuanLyTaiKhoanController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("danh-sach-tai-khoan")]
        public IActionResult DanhSachTaiKhoan()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items = _context.NguoiDungs.Where(c => c.IdQuyen == "User").ToList();
            
            List<TaiKhoanNguoiDung> taiKhoanNguoiDungs = new List<TaiKhoanNguoiDung>();
            foreach (var item in items)
            {
                var trangthai = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                var tknd = new TaiKhoanNguoiDung();
                tknd.Name = item.Ten;
                tknd.Email = item.Email;
                tknd.Id = item.IdNguoiDung;
                tknd.TrangThai = trangthai.TrangThai;
                taiKhoanNguoiDungs.Add(tknd);
            }
            return View(taiKhoanNguoiDungs);

        }
        [Route("danh-sach-tai-khoan")]
        [HttpPost]
        public IActionResult DanhSachTaiKhoan(string ten)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items = _context.NguoiDungs.Where(c=>c.IdQuyen=="User").ToList();
            if (ten != null)
            {

                items = _context.NguoiDungs.Where(c => c.Email.Contains((ten ?? "").ToLower())&&c.IdQuyen=="User").ToList();
            }
            List<TaiKhoanNguoiDung> taiKhoanNguoiDungs = new List<TaiKhoanNguoiDung>();
            foreach (var item in items)
            {
                var trangthai = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                var tknd = new TaiKhoanNguoiDung();
                tknd.Name = item.Ten;
                tknd.Email = item.Email;
                tknd.Id = item.IdNguoiDung;
                tknd.TrangThai = trangthai.TrangThai;
                taiKhoanNguoiDungs .Add(tknd);
            }
            return View(taiKhoanNguoiDungs);
            
        }
        [Route("cap-nhat-tai-khoan")]
        public IActionResult CapNhatTaiKhoan(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var taikhoan=_context.NguoiDungs.FirstOrDefault(c=>c.IdNguoiDung==id);
            var trangthai = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == id);
            TaiKhoanNguoiDung tknd = new TaiKhoanNguoiDung();
            tknd.Name = taikhoan.Ten;
            tknd.Email = taikhoan.Email;
            tknd.Id = id;
            tknd.TrangThai = trangthai.TrangThai;
            return View(tknd);
        }
        [Route("cap-nhat-tai-khoan")]
        [HttpPost]
        public IActionResult CapNhatTaiKhoan(string id, int trangthai)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var taikhoan=_context.XacThucs.FirstOrDefault(c=>c.IdNguoiDung==id);  
            taikhoan.TrangThai= trangthai;
            _context.XacThucs.Update(taikhoan);
            _context.SaveChanges();
            return RedirectToAction("DanhSachTaiKhoan", "QuanLyTaiKhoan", new { Areas = "Admin" });
        }
        
    }
}

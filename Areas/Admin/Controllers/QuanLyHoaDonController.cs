using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.HoaDonAdmin;
using QuanLyKhachSan.Areas.Admin.Models.TaiKhoanNguoiDung;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyHoaDonController : Controller
    {
        
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public QuanLyHoaDonController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("danh-sach-hoa-don")]
        public IActionResult DanhSachHoaDon()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items = _context.HoaDons.ToList();
            var tb = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "hoadon");
            
            List<HoaDonAdmin> hoadons=new List<HoaDonAdmin>();
            foreach (var item in items)
            {
                var ten = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                var phong=_context.Phongs.FirstOrDefault(c=>c.Id==item.IdPhong);
                var hoadon = new HoaDonAdmin();
                hoadon.IdHoaDon = item.IdHoaDon;
                hoadon.TenNguoiDung = ten.Ten;
                hoadon.GioCheckin = item.GioCheckin;
                hoadon.GioCheckout = item.GioCheckout;
                hoadon.TrangThai = item.TrangThai;
                hoadon.TongTien=item.TongTien;
                hoadon.TenPhong = phong.TenPhong;
                hoadon.thongbao = tb.Tttb;
                hoadons.Add(hoadon);

            }
            tb.Tttb = 0;
            _context.ThongBaos.Update(tb);
            _context.SaveChanges();
            hoadons = hoadons.OrderBy(tv => tv.TrangThai == "Chờ nhận phòng" ? 0 : 1)
                        .ThenBy(tv => tv.GioCheckin)
                        .ToList();

            return View(hoadons);
        }
        [Route("danh-sach-hoa-don")]
        [HttpPost]
        public IActionResult DanhSachHoaDon(string ten)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var nds = _context.NguoiDungs.Where(c => c.Email.Contains((ten ?? "").ToLower()) && c.IdQuyen == "User").ToList();
            List<HoaDon> hoaDons = new List<HoaDon>();
            var items = _context.HoaDons.ToList();
            foreach (var nd in nds)
            {
                foreach (var item in items) 
                { 
                    if(nd.IdNguoiDung == item.IdNguoiDung)
                    {
                        hoaDons.Add(item);
                    }
                }
            }
            
            List<HoaDonAdmin> hoadons = new List<HoaDonAdmin>();
            foreach (var item in hoaDons)
            {
                var tennd = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                var phong = _context.Phongs.FirstOrDefault(c => c.Id == item.IdPhong);
                var hoadon = new HoaDonAdmin();
                hoadon.IdHoaDon = item.IdHoaDon;
                hoadon.TenNguoiDung = tennd.Ten;
                hoadon.TenPhong = phong.TenPhong;
                hoadon.GioCheckin = item.GioCheckin.Date;
                hoadon.GioCheckout = item.GioCheckout.Date;
                hoadon.TrangThai = item.TrangThai;
                hoadon.TongTien = item.TongTien;
                hoadons.Add(hoadon);
            }
            hoadons = hoadons.OrderBy(tv => tv.TrangThai=="Chờ nhận phòng" ? 0 : 1)
                         .ThenBy(tv => tv.GioCheckin)
                         .ToList();
            return View(hoadons);
        }
        [Route("cap-nhat-hoa-don")]
        public IActionResult CapNhatHoaDon(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var hoadon=_context.HoaDons.FirstOrDefault(c=>c.IdHoaDon == id);
            var nguoidung = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == hoadon.IdNguoiDung);
            var phong = _context.Phongs.FirstOrDefault(c => c.Id == hoadon.IdPhong);
            var ct = new HoaDonAdmin();
            ct.yeucau = hoadon.YeuCau;
            ct.IdHoaDon = hoadon.IdHoaDon;
            ct.TenNguoiDung = nguoidung.Ten;
            ct.GioCheckout = hoadon.GioCheckout;
            ct.GioCheckin = hoadon.GioCheckin;
            ct.TongTien= hoadon.TongTien;
            ct.TrangThai = hoadon.TrangThai;
            ct.email = nguoidung.Email;
            ct.TenPhong = phong.TenPhong;
            return View(ct);
        }
        [Route("cap-nhat-hoa-don")]
        [HttpPost]
        public IActionResult CapNhaHoaDon(string IdHoaDon, string trangthai)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var hoadon=_context.HoaDons.FirstOrDefault(c=>c.IdHoaDon == IdHoaDon);
            hoadon.TrangThai = trangthai;
            
            //if(hoadon.TrangThai == null)
            //{
            //    var tb = new ThongBao(404,"asdfghj");
            //    return PartialView("_ThongBaoHoaDonPartial", tb);
            //}

            _context.HoaDons.Update(hoadon);
            _context.SaveChanges();
            return RedirectToAction("DanhSachHoaDon", "QuanLyHoaDon", new { Areas = "Admin" });
        }
        [Route("thong-bao-hoa-don")]
        public IActionResult ThongBaoHoaDon()
        {
            var tb= _context.ThongBaos.FirstOrDefault(c=>c.TenThongBao=="hoadon");
            return PartialView("_ThongBaoHoaDonPartial", tb);
        }
        
    }
}

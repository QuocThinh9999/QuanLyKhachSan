using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.PhongVaAnh;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Models.TrangPhong;
using static QuanLyKhachSan.Models.TrangChu.TrangChu;

namespace QuanLyKhachSan.Controllers
{
    public class PhongController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        public PhongController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [Route("danh-sach-phong")]
        public IActionResult DanhSachPhong(string phongs)
        {
            List<Phong> danhSachPhong = new List<Phong>();
            danhSachPhong = _context.Phongs.ToList();
            if (phongs != null)
            {
                danhSachPhong = System.Text.Json.JsonSerializer.Deserialize<List<Phong>>(phongs);
            }
            
            List<TrangPhong> trangPhong = new List<TrangPhong>();
            foreach(var phong in danhSachPhong)
            {
                var ctp = _context.ChiTietPhongs.FirstOrDefault(c => c.IdPhong == phong.Id);
                var tp=new TrangPhong();
                tp.id = phong.Id;
                tp.TenPhong = phong.TenPhong;
                tp.GiaSauGiam=phong.GiaSauGiam;
                tp.GiaPhong=phong.GiaPhong;
                tp.SoNguoiLon = ctp.SoNguoiLon;
                tp.SoTreEm=ctp.SoTreEm;
                tp.DienTich=ctp.DienTich;
                tp.MoTa = ctp.MoTa;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phong.UrlImage);
                tp.Url = ListUrl[0].UrlImage;
                trangPhong.Add(tp);
            }
            return View(trangPhong);
        }
        [Route("chi-tiet-phong")]
        public IActionResult ChiTietPhong(string id)
        {
            var phongc = _context.Phongs.FirstOrDefault(c => c.Id == id);
            var ctp = _context.ChiTietPhongs.FirstOrDefault(c=>c.IdPhong==id);
            var phong=new ChiTietTrangPhong();
            phong.id = phongc.Id;
            phong.TenPhong = phongc.TenPhong;
            phong.IdLoaiPhong=phongc.IdLoaiPhong;
            phong.GiaPhong=phongc.GiaPhong;
            phong.GiaSauGiam=phongc.GiaSauGiam;
            phong.SoNguoiLon = ctp.SoNguoiLon;
            phong.SoTreEm=ctp.SoTreEm;
            phong.DienTich= ctp.DienTich;
            phong.MoTa = ctp.MoTa;
            var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phongc.UrlImage);
            phong.UrlImages = new List<string>();
            foreach (var url in ListUrl)
            {
                phong.UrlImages.Add(url.UrlImage);
            }
            var phongNgauNhien = _context.Phongs.OrderBy(x => Guid.NewGuid()).ToList();
            phong.phongKhaDungs = new List<ChiTietTrangPhong.PhongKhaDung>();
            for (int i = 0; i < 2; i++)
            {
                var ListUrl2 = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phongNgauNhien[i].UrlImage);
                var phongKhaDung = new ChiTietTrangPhong.PhongKhaDung
                {
                    id = phongNgauNhien[i].Id,
                    TenPhong = phongNgauNhien[i].TenPhong,
                    Url = ListUrl2[0].UrlImage,
                    GiaPhong = phongNgauNhien[i].GiaPhong,
                    GiaSauGiam = phongNgauNhien[i].GiaSauGiam

                };
                phong.phongKhaDungs.Add(phongKhaDung);
            }
            return View(phong);
        }
        [Route("check-phong")]
        [HttpPost]
        public IActionResult DanhSachCheckPhong(CheckPhong input)
        {
            if (input.GioCheckin > input.GioCheckout)
            {
                TempData["error"] = "Ngày check out phải sau check in!";
                return RedirectToAction("DanhSachPhong", "Phong");
            }
            if (input.GioCheckin <= DateTime.Now)
            {
                TempData["error"] = "Ngày check in phải từ ngày hiện tại!";
                return RedirectToAction("DanhSachPhong", "Phong");
            }
            var danhSachPhong = _context.Phongs
    .Join(_context.ChiTietPhongs,
        phong => phong.Id,
        ctp => ctp.IdPhong,
        (phong, ctp) => new { Phong = phong, ChiTietPhong = ctp })
    .Where(x => x.Phong.GiaPhong < input.GiaPhongMax &&
                x.Phong.GiaPhong > input.GiaPhongMin &&
                x.ChiTietPhong.SoNguoiLon >= input.SoNguoiLon &&
                x.ChiTietPhong.SoTreEm >= input.SoTreEm)
    .Select(x => x.Phong)
    .ToList();
            List<Phong> item = new List<Phong>();
            foreach (var p in danhSachPhong)
            {
                var hoadons=_context.HoaDons.Where(c=>c.IdPhong == p.Id&&c.TrangThai=="Chờ nhận phòng").ToList();
                int i = 0;
                foreach (var cp in hoadons)
                {
                    if ((cp.GioCheckin <= input.GioCheckin && input.GioCheckin < cp.GioCheckout)
                        || (cp.GioCheckin < input.GioCheckout && input.GioCheckout <= cp.GioCheckout)
                        || (input.GioCheckin < cp.GioCheckin && cp.GioCheckout < input.GioCheckout))
                    {
                        i++;
                    }
                }
                if (p.SoLuong > i)
                {
                    item.Add(p);
                }
            }
            string phongs = System.Text.Json.JsonSerializer.Serialize(item);
            return RedirectToAction("DanhSachPhong", "Phong", new { phongs = phongs });
        }
    }
}
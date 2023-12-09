using System.Text.Json;
using QuanLyKhachSan.Areas.Admin.Models.ThemDuLieu;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.PhongVaAnh;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Common;
using APIQuanLyKhachSan.Models.ThemDuLieu;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyPhongController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public QuanLyPhongController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("danh-sach-phong-admin")]
        public IActionResult DanhSachPhong()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var phong = _context.Phongs.ToList();
            var listViewPhong = new List<ViewPhong>();
            foreach (var item in phong)
            {
                var loaiphong = _context.LoaiPhongs.FirstOrDefault(c => c.IdLoaiPhong == item.IdLoaiPhong);
                ViewPhong viewPhong = new ViewPhong();
                viewPhong.Id = item.Id;
                viewPhong.TenPhong = item.TenPhong;
                viewPhong.TenLoaiPhong = loaiphong.TenLoaiPhong;
                viewPhong.ChiTietPhong = item.ChiTietPhong;
                viewPhong.SoLuong = item.SoLuong;
                viewPhong.GiaPhong = item.GiaPhong;
                viewPhong.GiaSauGiam = item.GiaSauGiam;
                viewPhong.HoaDons = item.HoaDons;

                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(item.UrlImage);
                viewPhong.UrlImages = new List<string>();
                foreach (var url in ListUrl)
                {
                    viewPhong.UrlImages.Add(url.UrlImage);
                }
                listViewPhong.Add(viewPhong);
            }
            return View(listViewPhong);
        }
        [Route("danh-sach-phong-admin")]
        [HttpPost]
        public IActionResult DanhSachPhong(string ten)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var phong = _context.Phongs.ToList();
            if (ten != null)
            {
                 phong = _context.Phongs.Where(c=>c.TenPhong.Contains((ten ?? "").ToLower())).ToList();
            }
            var listViewPhong = new List<ViewPhong>();
            foreach (var item in phong)
            {
                var loaiphong = _context.LoaiPhongs.FirstOrDefault(c => c.IdLoaiPhong == item.IdLoaiPhong);
                ViewPhong viewPhong = new ViewPhong();
                viewPhong.Id = item.Id;
                viewPhong.TenPhong = item.TenPhong;
                viewPhong.TenLoaiPhong = loaiphong.TenLoaiPhong;
                viewPhong.ChiTietPhong = item.ChiTietPhong;
                viewPhong.SoLuong = item.SoLuong;
                viewPhong.GiaPhong = item.GiaPhong;
                viewPhong.GiaSauGiam = item.GiaSauGiam;
                viewPhong.HoaDons = item.HoaDons;

                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(item.UrlImage);
                viewPhong.UrlImages = new List<string>(); 
                foreach (var url in ListUrl)
                {
                    viewPhong.UrlImages.Add(url.UrlImage);
                }
                listViewPhong.Add(viewPhong);
            }
            return View(listViewPhong);
        }
        [Route("xoa-phong")]
        public IActionResult XoaPhong(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var phong = _context.Phongs.FirstOrDefault(k => k.Id == id);
            
            if (phong != null)
            {
                var chitietphong = _context.ChiTietPhongs.FirstOrDefault(ctp => ctp.IdPhong == phong.Id);
                _context.ChiTietPhongs.Remove(chitietphong);
                var hoadon = _context.HoaDons.Where(c => c.IdPhong == phong.Id).ToList();
                foreach (var hd in hoadon)
                {
                    var danhgias = _context.DanhGia.FirstOrDefault(dg => dg.IdHoaDon == hd.IdHoaDon);
                    _context.DanhGia.Remove(danhgias);
                }
                if (phong.UrlImage != null)
                {
                    var Urlcu = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phong.UrlImage);
                    foreach (var anhcu in Urlcu)
                    {
                        UploadFiles.RemoveImage(anhcu.UrlImage);
                    }
                }
                _context.HoaDons.RemoveRange(hoadon);
                _context.Phongs.Remove(phong);
                _context.SaveChanges();
                return RedirectToAction("DanhSachPhong", "QuanLyPhong", new { Areas = "Admin" });
            }
            else return View();
        }
        [Route("them-phong")]
        public IActionResult ThemPhong()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            return View();
        }
        [Route("Them-phong")]
        [HttpPost]
        public IActionResult ThemPhong( ThemPhong input)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            if (ModelState.IsValid)
            {
                string id = Guid.NewGuid().ToString();
                Phong phong = new Phong
                {
                    Id = id,
                    TenPhong = input.TenPhong,
                    SoLuong = input.SoLuong,
                    GiaPhong = input.GiaPhong,
                    GiaSauGiam = input.GiaPhong,         
                    IdLoaiPhong = input.IdLoaiPhong,
                    IdLoaiPhongNavigation = _context.LoaiPhongs.FirstOrDefault(c => c.IdLoaiPhong == input.IdLoaiPhong),

                };
                List<OutputImage> listimage = new List<OutputImage>();
                foreach (var img in input.Images)
                {
                    OutputImage output = new OutputImage();
                    output.UrlImage = UploadFiles.SaveImage(img);
                    
                    listimage.Add(output);
                }
                phong.UrlImage = System.Text.Json.JsonSerializer.Serialize(listimage);
                _context.Phongs.Add(phong);
                ChiTietPhong chiTietPhong = new ChiTietPhong
                {
                    IdPhong = id,
                    SoNguoiLon = input.SoNguoiLon,
                    SoTreEm = input.SoTreEm,
                    DienTich = input.DienTich,
                    MoTa = input.MoTa,
                    IdPhongNavigation = phong,
                };
                _context.ChiTietPhongs.Add(chiTietPhong);
                _context.SaveChanges();
                return RedirectToAction("DanhSachPhong", "QuanLyPhong", new { Areas = "Admin" });
            }
            else return View();

        }
        [Route("cap-nhat-phong")]
        public IActionResult CapNhatPhong(string id)
        {

            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var phong = _context.Phongs.Find(id);
            var ctp = _context.ChiTietPhongs.Find(id);
            CapNhatPhong item = new CapNhatPhong();
            item.id = id;
            item.TenPhong = phong.TenPhong;
            item.IdLoaiPhong=phong.IdLoaiPhong;
            item.GiaPhong = phong.GiaPhong;
            item.SoLuong = phong.SoLuong;
            item.SoNguoiLon = ctp.SoNguoiLon;
            item.SoTreEm=ctp.SoTreEm;
            item.MoTa = ctp.MoTa;
            item.DienTich=ctp.DienTich;
           
            return View(item);
        }
        [Route("cap-nhat-phong")]
        [HttpPost]
        public IActionResult CapNhatPhong(CapNhatPhong input)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            
                var item = _context.Phongs.Find(input.id);
                item.TenPhong = input.TenPhong;
                item.SoLuong = input.SoLuong;
                item.GiaPhong = input.GiaPhong;
            item.IdLoaiPhong = input.IdLoaiPhong;
                var ctp = _context.ChiTietPhongs.FirstOrDefault(c => c.IdPhong == input.id);
                ctp.SoNguoiLon = input.SoNguoiLon;
                ctp.SoTreEm = input.SoTreEm;
                ctp.DienTich = input.DienTich;
                ctp.MoTa = input.MoTa;

                if (input.Images != null)
                {
                    if (item.UrlImage != null)
                    {
                        var Urlcu = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(item.UrlImage);
                        foreach (var anhcu in Urlcu)
                        {
                            UploadFiles.RemoveImage(anhcu.UrlImage);
                        }
                    }
                    List<OutputImage> listimage = new List<OutputImage>();
                    foreach (var img in input.Images)
                    {
                        OutputImage output = new OutputImage();
                        output.UrlImage = UploadFiles.SaveImage(img);
                        
                        listimage.Add(output);
                    }
                    item.UrlImage = System.Text.Json.JsonSerializer.Serialize(listimage);
                }
                
                _context.Update(item);
                _context.Update(ctp);
                _context.SaveChanges();
                return RedirectToAction("DanhSachPhong", "QuanLyPhong", new { Areas = "Admin" });
            
            
        }
       
    }
}

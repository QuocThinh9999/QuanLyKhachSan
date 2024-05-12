using System.Diagnostics;
using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.PhongVaAnh;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Models.TrangChu;

namespace QuanLyKhachSan.Controllers
{

    public class HomeController : Controller
    {
        
        private readonly DbQuanLyKhachSanContext _context;
        public HomeController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        
        public IActionResult Home()
        {
            TrangChu trangChu=new TrangChu();
            trangChu.thongTinWeb = _context.ThongTinWebs.ToList();
            trangChu.phong = new List<TrangChu.PhongTrangChu>();
            var phongs=_context.Phongs.OrderBy(x => Guid.NewGuid()).ToList();

            foreach (var phong in phongs)
            {
                var phongtrangchu = new TrangChu.PhongTrangChu();
                var ctp = _context.ChiTietPhongs.FirstOrDefault(c => c.IdPhong == phong.Id);
                phongtrangchu.id=phong.Id;
                phongtrangchu.TenPhong = phong.TenPhong;
                phongtrangchu.GiaSauGiam = phong.GiaSauGiam;
                phongtrangchu.GiaPhong = phong.GiaPhong;
                phongtrangchu.SoNguoiLon = ctp.SoNguoiLon;
                phongtrangchu.SoTreEm=ctp.SoTreEm;
                phongtrangchu.DienTich=ctp.DienTich;
                phongtrangchu.MoTa = ctp.MoTa;
                trangChu.phong.Add(phongtrangchu);
                
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phong.UrlImage);
                phongtrangchu.Url = ListUrl[0].UrlImage;
            }
            trangChu.blog = new List<Blog>();
            var bg=_context.Blogs.OrderBy(x => Guid.NewGuid()).ToList();

            foreach (var b in bg)
            {
                var bgtrangchu = new Blog();
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(b.UrlImage);
                bgtrangchu.UrlImage = ListUrl[0].UrlImage;
                bgtrangchu.Tieude =b.Tieude;
                bgtrangchu.MoTa=b.MoTa;
                bgtrangchu.IdBlog = b.IdBlog;
                trangChu.blog.Add(bgtrangchu);
            }
            trangChu.NgayKhongKhaDung = "25/12/2023";
            return View(trangChu);
        }
        
       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
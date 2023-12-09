using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Models.TrangBaiViet;
using QuanLyKhachSan.Models.TrangPhong;

namespace QuanLyKhachSan.Controllers
{
    public class BaiVietController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        public BaiVietController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [Route("danh-sach-bai-viet")]
        public IActionResult DanhSachBaiViet()
        {
            var blogs = _context.Blogs.ToList();
            List<TrangBaiViet> trangBaiViet = new List<TrangBaiViet>();
            foreach (var blog in blogs)
            {
                
                var tbv = new TrangBaiViet();
                tbv.IdBlog=blog.IdBlog;
                tbv.Tieude=blog.Tieude;
                tbv.MoTa=blog.MoTa;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(blog.UrlImage);
                tbv.UrlImage = ListUrl[0].UrlImage;
                trangBaiViet.Add(tbv);
            }
            return View(trangBaiViet);
        }
        [Route("danh-sach-bai-viet")]
        [HttpPost]
        public IActionResult DanhSachBaiViet(string ten)
        {
            var blogs = _context.Blogs.Where(c => c.Tieude.Contains((ten ?? "").ToLower())).ToList();
            List<TrangBaiViet> trangBaiViet = new List<TrangBaiViet>();
            foreach (var blog in blogs)
            {

                var tbv = new TrangBaiViet();
                tbv.IdBlog = blog.IdBlog;
                tbv.Tieude = blog.Tieude;
                tbv.MoTa = blog.MoTa;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(blog.UrlImage);
                tbv.UrlImage = ListUrl[0].UrlImage;
                trangBaiViet.Add(tbv);
            }
            return View(trangBaiViet);
        }
        [Route("chi-tiet-bai-viet")]
        public IActionResult ChiTietBaiViet(string id)
        {
            var item=_context.Blogs.FirstOrDefault(c=>c.IdBlog==id);
            var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(item.UrlImage);
            var baiviet = new ChiTietTrangBaiViet();
            baiviet.IdBlog = item.IdBlog;
            baiviet.Tieude = item.Tieude;
            baiviet.MoTa = item.MoTa;
            baiviet.UrlImage = new List<string>();
            foreach (var url in ListUrl)
            {
                baiviet.UrlImage.Add(url.UrlImage);
            }
            var baivietkhadungs = _context.Blogs.OrderBy(x => Guid.NewGuid()).ToList();
            baiviet.baiVietKhaDungs = new List<ChiTietTrangBaiViet.BaiVietKhaDung>();

            for(int i = 0; i < 3; i++)
            {
                var bt=new ChiTietTrangBaiViet.BaiVietKhaDung();
                bt.IdBlog = baivietkhadungs[i].IdBlog;
                bt.Tieude = baivietkhadungs[i].Tieude;
                bt.MoTa = baivietkhadungs[i].MoTa;
                var ListUrl2 = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(baivietkhadungs[i].UrlImage);
                bt.UrlImage = ListUrl2[0].UrlImage;
                baiviet.baiVietKhaDungs.Add(bt);
            }
            return View(baiviet);
        }
    }
}

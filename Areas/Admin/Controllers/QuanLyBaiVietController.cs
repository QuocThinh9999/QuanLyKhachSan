using System.Reflection.Metadata;
using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.ThemDuLieu;
using QuanLyKhachSan.Common;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyBaiVietController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public QuanLyBaiVietController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("danh-sach-bai-viet-admin")]
        public IActionResult DanhSachBaiViet()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            List<Blog> blogs = new List<Blog>();
            var items = _context.Blogs.ToList();
            foreach (var b in items)
            {
                Blog blog = new Blog();
                blog.Tieude = b.Tieude;
                blog.IdBlog = b.IdBlog;
                blog.MoTa = b.MoTa;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(b.UrlImage);
                blog.UrlImage = ListUrl[0].UrlImage;
                blogs.Add(blog);
            }
            return View(blogs);
        }
        [Route("danh-sach-bai-viet-admin")]
        [HttpPost]
        public IActionResult DanhSachBaiViet(string ten)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            List<Blog> blogs=new List<Blog>();
            var items = _context.Blogs.ToList();
            if (ten != null)
            {
                items = _context.Blogs.Where(c => c.Tieude.Contains((ten ?? "").ToLower())).ToList();
            }
            foreach(var b in items)
            {
                Blog blog = new Blog();
                blog.Tieude = b.Tieude;
                blog.IdBlog=b.IdBlog;
                blog.MoTa=b.MoTa;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(b.UrlImage);
                blog.UrlImage = ListUrl[0].UrlImage;
                blogs.Add(blog);
            }
            return View(blogs);
        }
        [Route("xoa-bai-viet")]
        public IActionResult XoaBaiViet(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var item=_context.Blogs.FirstOrDefault(c=>c.IdBlog == id);
            if(item.UrlImage!= null)
            {
                var Urlcu = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(item.UrlImage);
                foreach (var anhcu in Urlcu)
                {
                    UploadFiles.RemoveImage(anhcu.UrlImage);
                }
            }
            _context.Blogs.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("DanhSachBaiViet", "QuanLyBaiViet", new { Areas = "Admin" });
        }
        [Route("them-bai-viet")]
        public IActionResult ThemBaiViet()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            return View();
        }
        [Route("them-bai-viet")]
        [HttpPost]
        public IActionResult ThemBaiViet(ThemBlog input)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            if (ModelState.IsValid)
            {
                var blog = new Blog();
                blog.IdBlog = Guid.NewGuid().ToString();
                blog.Tieude = input.Tieude;
                blog.MoTa = input.MoTa;
                List<OutputImage> listimage = new List<OutputImage>();
                foreach (var img in input.Images)
                {
                    OutputImage output = new OutputImage();
                    output.UrlImage = UploadFiles.SaveImage(img);
                    listimage.Add(output);
                }
                blog.UrlImage = System.Text.Json.JsonSerializer.Serialize(listimage);
                _context.Blogs.Add(blog);
                _context.SaveChanges();
                return RedirectToAction("DanhSachBaiViet", "QuanLyBaiViet", new { Areas = "Admin" });
            }
            else return View();
        }
        [Route("cap-nhat-bai-viet")]
        public IActionResult CapNhatBaiViet(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var item=_context.Blogs.FirstOrDefault(c=>c.IdBlog == id);
            return View(item);
        }
        [Route("cap-nhat-bai-viet")]
        [HttpPost]
        public IActionResult CapNhatBaiViet(string id, ThemBlog input)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            
                var blog = _context.Blogs.FirstOrDefault(c=>c.IdBlog==id);
                blog.Tieude = input.Tieude;
                blog.MoTa = input.MoTa;
                if (input.Images != null)
                {
                    if (blog.UrlImage != null)
                    {
                        var Urlcu = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(blog.UrlImage);
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
                    blog.UrlImage = System.Text.Json.JsonSerializer.Serialize(listimage);
                }
                _context.Blogs.Update(blog);
                _context.SaveChanges();
                return RedirectToAction("DanhSachBaiViet", "QuanLyBaiViet", new { Areas = "Admin" });
            
          
        }
    }
}

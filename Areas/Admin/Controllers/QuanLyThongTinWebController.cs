using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.ThemDuLieu;
using QuanLyKhachSan.Common;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyThongTinWebController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public QuanLyThongTinWebController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("danh-sach-thong-tin-web")]
        public IActionResult DanhSachThongTinWeb()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items=_context.ThongTinWebs.ToList();
            return View(items);
        }
        [Route("danh-sach-thong-tin-web")]
        [HttpPost]
        public IActionResult DanhSachThongTinWeb(string ten)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var items = _context.ThongTinWebs.ToList();
            if (ten != null)
            {
                items=_context.ThongTinWebs.Where(c=>c.LoaiThongTin.Contains((ten ?? "").ToLower())).ToList();
            }
            return View(items);
        }
        [Route("cap-nhat-thong-tin-web")]
        public IActionResult CapNhatThongTinWeb(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var item = _context.ThongTinWebs.FirstOrDefault(c => c.LoaiThongTin == id);
            return View(item);
        }
        [Route("cap-nhat-thong-tin-web")]
        [HttpPost]
        public IActionResult CapNhatThongTinWeb(CapNhatThongTinWeb input)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var item=_context.ThongTinWebs.FirstOrDefault(c=>c.LoaiThongTin==input.LoaiThongTin);
            item.MoTa=input.MoTa;
            if(input.Images!=null)
            {
                if (item.UrlImages != null)
                {
                    UploadFiles.RemoveImage(item.UrlImages);
                    
                }
                item.UrlImages = UploadFiles.SaveImage(input.Images);
            }    

            
           
            _context.ThongTinWebs.Update(item);
            _context.SaveChanges();
            return RedirectToAction("DanhSachThongTinWeb", "QuanLyThongTinWeb", new { Areas = "Admin" });
        }
    }
}

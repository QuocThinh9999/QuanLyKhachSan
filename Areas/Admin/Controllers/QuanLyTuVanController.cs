using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.ThongBao;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyTuVanController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public QuanLyTuVanController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("danh-sach-tu-van")]
        public IActionResult DanhSachTuVan()
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var tb=_context.ThongBaos.FirstOrDefault(c=>c.TenThongBao=="email");
           
           
            var tuvans = _context.TuVans.OrderByDescending(tv => tv.NgayGioNhan).ToList();
            List<ViewThongBao> vtbs= new List<ViewThongBao>();
            foreach(var tuvan in tuvans)
            {
                var vtb=new ViewThongBao();
                vtb.IdTuVan=tuvan.IdTuVan;
                vtb.LoiNhan=tuvan.LoiNhan;
                vtb.NgayGioNhan = tuvan.NgayGioNhan;
                vtb.Ten=tuvan.Ten;
                vtb.Email=tuvan.Email;
                vtb.SoThongBao = tb.Tttb;
                vtbs.Add(vtb);
            }
            tb.Tttb = 0;
            _context.ThongBaos.Update(tb);
            _context.SaveChanges();
            return View(vtbs);
        }
        [Route("danh-sach-tu-van")]
        [HttpPost]
        public IActionResult DanhSachTuVan(string ten)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            


            var tuvans = _context.TuVans.OrderByDescending(tv => tv.NgayGioNhan).ToList();
            if(ten!=null)
            {
                tuvans = _context.TuVans.OrderByDescending(tv => tv.NgayGioNhan).Where(tv => tv.Email.Contains((ten?? "").ToLower())).ToList();
            }
            List<ViewThongBao> vtbs = new List<ViewThongBao>();
            foreach (var tuvan in tuvans)
            {
                var vtb = new ViewThongBao();
                vtb.IdTuVan = tuvan.IdTuVan;
                vtb.LoiNhan = tuvan.LoiNhan;
                vtb.NgayGioNhan = tuvan.NgayGioNhan;
                vtb.Ten = tuvan.Ten;
                vtb.Email = tuvan.Email;
                vtb.SoThongBao = 0;
                vtbs.Add(vtb);
            }
            return View(vtbs);
        }
        [Route("xoa-tu-van")]
        public IActionResult XoaTuVan(string id)
        {
            IActionResult result = _xacThuc.CheckAuthentication();

            if (result != null)
            {
                return result;
            }
            var tuvan=_context.TuVans.FirstOrDefault(c=>c.IdTuVan==id);
            _context.TuVans.Remove(tuvan);
            _context.SaveChanges();
            return RedirectToAction("DanhSachTuVan", "QuanLyTuVan", new { Areas = "Admin" });
        }
        [Route("thong-bao")]
        public IActionResult ThongBaoEmail()
        {
            return PartialView("_ThongBaoPartial");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Controllers
{
    public class LienLacController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        public LienLacController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [Route("lien-lac")]
        public IActionResult LienLac(string email)
        {
            var tuvan = new ThemTuVan();
            tuvan.Email = email;
            if(email!=null)
            {

            return View(tuvan);
            }
            else return View();
        }
        [Route("lien-lac")]
        [HttpPost]
        public IActionResult LienLac(ThemTuVan input)
        {
            
            var tuvan=new TuVan();
            tuvan.IdTuVan=Guid.NewGuid().ToString();
            tuvan.NgayGioNhan=DateTime.Now;
            tuvan.LoiNhan=input.LoiNhan;
            tuvan.Ten=input.Ten;
            tuvan.Email=input.Email;
            var tb = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "email");
            tb.Tttb = 1;
            _context.TuVans.Add(tuvan);
            _context.ThongBaos.Update(tb);
            _context.SaveChanges();
            return View();
        }
    }
}

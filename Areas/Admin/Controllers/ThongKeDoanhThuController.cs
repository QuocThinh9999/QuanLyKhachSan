using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Areas.Admin.Models.ThongKe;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeDoanhThuController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        private readonly Auth _xacThuc;
        public ThongKeDoanhThuController(DbQuanLyKhachSanContext context, Auth xacThuc)
        {
            _context = context;
            _xacThuc = xacThuc;
        }
        [Route("thong-ke-doanh-thu")]
        public IActionResult ThongKeDoanhThu()
        {
            ThongKe thongke = new ThongKe();
            if (DateTime.Now.Month >= 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    thongke.thang[i] = DateTime.Now.AddMonths(-5 + i).Month;
                    thongke.tong[i] = 0;

                    DateTime startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 5 + i, 1);
                    DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                    var hoadon = _context.HoaDons
                        .Where(c => c.GioCheckin >= startOfMonth && c.GioCheckin <= endOfMonth && c.TrangThai == "Thành công")
                        .ToList();

                    foreach (var hd in hoadon)
                    {
                        thongke.tong[i] += hd.TongTien;
                    }
                }
            }

            return View(thongke);
        }
    }
}

using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Models.VeChungToi;
using static QuanLyKhachSan.Models.TrangChu.TrangChu;

namespace QuanLyKhachSan.Controllers
{
    public class VeChungToiController : Controller
    {
        private readonly DbQuanLyKhachSanContext _context;
        public VeChungToiController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [Route("ve-chung-toi")]
        public IActionResult DanhSachVeChungToi()
        {
            var phongs = _context.Phongs.OrderBy(x => Guid.NewGuid()).ToList();
            var vechungtoi = new VeChungToi();
            vechungtoi.phongVeChungTois = new List<VeChungToi.PhongVeChungToi>();
            vechungtoi.thongTinWebs = new List<ThongTinWeb>();
            var items=_context.ThongTinWebs.ToList();
            foreach (var item in items)
            {
                var vct = new ThongTinWeb();
                vct.LoaiThongTin = item.LoaiThongTin;
                vct.MoTa=item.MoTa;
                vct.UrlImages = item.UrlImages;
                vechungtoi.thongTinWebs.Add(vct);

            }
            for(int i = 0; i < 4; i++)
            {
                var phongvechungtoi = new VeChungToi.PhongVeChungToi();
                phongvechungtoi.id = phongs[i].Id;
                phongvechungtoi.Name = phongs[i].TenPhong;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phongs[i].UrlImage);
                phongvechungtoi.Url = ListUrl[0].UrlImage;
                vechungtoi.phongVeChungTois.Add(phongvechungtoi);
            }
            return View(vechungtoi);
        }
    }
}

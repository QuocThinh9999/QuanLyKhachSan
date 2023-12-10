using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Areas.Admin.Models;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Models.ViewHoaDon;
using static QuanLyKhachSan.Models.TrangChu.TrangChu;


namespace QuanLyKhachSan.Controllers
{
	public class HoaDonController : Controller
	{
		private readonly DbQuanLyKhachSanContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HoaDonController(DbQuanLyKhachSanContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}
		[HttpPost("mau-hoa-don")]
		public IActionResult MauHoaDon(ThemHoaDon input)
		{
			
			var httpcontext = _httpContextAccessor.HttpContext;
			var xacthuc = _context.NguoiDungs.FirstOrDefault(c =>  c.IdNguoiDung == httpcontext.Request.Cookies["Id"]);
			if(xacthuc == null)
			{
                return Redirect("/dang-nhap");
            }
			if (ModelState.IsValid)
			{
				if (input.GioCheckin > input.GioCheckout)
				{
					TempData["error"] = "Ngày check out phải sau check in!";
                    return RedirectToAction("ChiTietPhong", "Phong", new { id = input.IdPhong });
                }
                if (input.GioCheckin <= DateTime.Now)
                {
                    TempData["error"] = "Ngày check in phải từ ngày hiện tại!";
                    return RedirectToAction("ChiTietPhong", "Phong", new { id = input.IdPhong });
                }
                var phong = _context.Phongs.FirstOrDefault(c => c.Id == input.IdPhong);
				var checkphong = _context.HoaDons.Where(c => c.IdPhong == input.IdPhong && c.GioCheckout > DateTime.Now && c.TrangThai == "Chờ nhận phòng").ToList();
				int i = 0;
				foreach (var cp in checkphong)
				{
					if ((cp.GioCheckin <= input.GioCheckin && input.GioCheckin < cp.GioCheckout)
						|| (cp.GioCheckin < input.GioCheckout && input.GioCheckout <= cp.GioCheckout)
						|| (input.GioCheckin < cp.GioCheckin && cp.GioCheckout < input.GioCheckout))
					{
						i++;
					}
				}
				if (phong.SoLuong <= i)
				{
					TempData["error"] = "Phòng này đã đầy vào thời gian bạn chọn!";
					return RedirectToAction("ChiTietPhong","Phong", new {id=phong.Id});
				}
				var vhd = new ViewHoaDon();
				var ctp=_context.ChiTietPhongs.FirstOrDefault(c=>c.IdPhong==phong.Id);
				vhd.IdNguoiDung=xacthuc.IdNguoiDung;
				vhd.IdPhong = phong.Id;
				vhd.SoNguoiLon = ctp.SoNguoiLon;
				vhd.SoTreEm=ctp.SoTreEm;
				vhd.GioCheckin = input.GioCheckin;
				vhd.GioCheckout = input.GioCheckout;
				vhd.PhuThu = 0;
				vhd.Vat = 0.08m;
				vhd.TongThoiGian = (int)(input.GioCheckout - input.GioCheckin).TotalDays;
				vhd.TongTien = (vhd.TongThoiGian * phong.GiaSauGiam) * (1 + vhd.Vat) + vhd.PhuThu;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phong.UrlImage);
                vhd.url = ListUrl[0].UrlImage;
                return View(vhd);
			}
			TempData["error"] = "Vui lòng chọn đủ thông tin!";
			return RedirectToAction("ChiTietPhong", "Phong", new { id = input.IdPhong });
		}
		[Route("tao-hoa-don")]
		[HttpPost]
		public IActionResult TaoHoaDon(TaoHoaDon input)
		{
			var thongbao = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "hoadon");
			thongbao.Tttb++;
            var httpcontext = _httpContextAccessor.HttpContext;
            var xacthuc = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == httpcontext.Request.Cookies["Id"]);
            if (xacthuc == null)
            {
                return Redirect("/dang-nhap");
            }
			var phong = _context.Phongs.FirstOrDefault(c => c.Id == input.IdPhong);
			var ctp=_context.ChiTietPhongs.FirstOrDefault(c=>c.IdPhong==input.IdPhong);
			var hoadon = new HoaDon();
			hoadon.IdHoaDon=Guid.NewGuid().ToString();
			hoadon.IdNguoiDung = xacthuc.IdNguoiDung;
			hoadon.IdPhong=input.IdPhong;
			hoadon.SoTreEm = ctp.SoTreEm;
			hoadon.SoNguoiLon=ctp.SoNguoiLon;
			hoadon.GioCheckin=input.GioCheckin;
			hoadon.GioCheckout=input.GioCheckout;
			hoadon.PhuThu = 0;
			hoadon.Vat = 0.08m;
			hoadon.TongTien = (int)(input.GioCheckout - input.GioCheckin).TotalDays * phong.GiaSauGiam * 1.08m;
			hoadon.TrangThai = "Chờ nhận phòng";
			hoadon.YeuCau = input.yeucau;
			_context.ThongBaos.Update(thongbao);
			_context.HoaDons.Add(hoadon);
			_context.SaveChanges();
			return RedirectToAction("LichSuDatPhong", "HoaDon");
        }
		[Route("lich-su-dat-phong")]
		public IActionResult LichSuDatPhong()
		{
            var httpcontext = _httpContextAccessor.HttpContext;
            var xacthuc = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == httpcontext.Request.Cookies["Id"]);
            if (xacthuc == null)
            {
                return Redirect("/dang-nhap");
            }
			var hds=_context.HoaDons.Where(c=>c.IdNguoiDung==xacthuc.IdNguoiDung).ToList();
			List<lichsuhoadon> lshds=new List<lichsuhoadon>();
			foreach (var hd in hds)
			{
				var lshd = new lichsuhoadon();
				var phong = _context.Phongs.FirstOrDefault(c => c.Id == hd.IdPhong);
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phong.UrlImage);
                lshd.url = ListUrl[0].UrlImage;
				lshd.id = hd.IdHoaDon;
				lshd.GioCheckin = hd.GioCheckin;
				lshd.GioCheckout = hd.GioCheckout;
				lshd.tongtien = hd.TongTien;
				lshd.trangthai = hd.TrangThai;
				lshds.Add(lshd);

            }
            return View(lshds);
		}
		[Route("chi-tiet-lich-su-dat-phong")]
		public IActionResult LichSuChiTiet (string id)
		{
            var httpcontext = _httpContextAccessor.HttpContext;
            var xacthuc = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == httpcontext.Request.Cookies["Id"]);
            if (xacthuc == null)
            {
                return Redirect("/dang-nhap");
            }
			var hoadon = _context.HoaDons.FirstOrDefault(c => c.IdHoaDon == id);
			var phong = _context.Phongs.FirstOrDefault(c => c.Id == hoadon.IdPhong);
            var vhd = new ViewHoaDon();
            var ctp = _context.ChiTietPhongs.FirstOrDefault(c => c.IdPhong == phong.Id);
            vhd.IdNguoiDung = xacthuc.IdNguoiDung;
            vhd.IdPhong = phong.Id;
            vhd.SoNguoiLon = ctp.SoNguoiLon;
            vhd.SoTreEm = ctp.SoTreEm;
            vhd.GioCheckin = hoadon.GioCheckin;
            vhd.GioCheckout = hoadon.GioCheckout;
            vhd.PhuThu = 0;
            vhd.Vat = 0.08m;
			vhd.IdHoaDon = hoadon.IdHoaDon;
            vhd.TongThoiGian = (int)(hoadon.GioCheckout - hoadon.GioCheckin).TotalDays;
            vhd.TongTien = (vhd.TongThoiGian * phong.GiaSauGiam) * (1 + vhd.Vat) + vhd.PhuThu;
			vhd.trangthai = hoadon.TrangThai;
            var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phong.UrlImage);
            vhd.url = ListUrl[0].UrlImage;
            return View(vhd);
        }
		[Route("huy-dat-phong")]
		public IActionResult HuyDatPhong(string id)
		{
			var hoadon = _context.HoaDons.FirstOrDefault(c => c.IdHoaDon == id);
			hoadon.TrangThai = "Đã hủy";
			_context.Update(hoadon);
			_context.SaveChanges();
			return RedirectToAction("LichSuChiTiet","HoaDon", new {id=hoadon.IdHoaDon});
		}
	}
}



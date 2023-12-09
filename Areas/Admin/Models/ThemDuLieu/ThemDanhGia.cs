using System.ComponentModel.DataAnnotations;

namespace QuanLyKhachSan.Areas.Admin.Models
{
    public class ThemDanhGia
    {
        public string IdHoaDon { get; set; } = null!;
        public string? IdNguoiDung { get; set; }
        public int SoSao { get; set; }

        public string? NhanXet { get; set; }
    }
}

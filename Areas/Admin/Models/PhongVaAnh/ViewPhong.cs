using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Areas.Admin.Models.PhongVaAnh
{
    public class ViewPhong
    {
        public string Id { get; set; } = null!;

        public string? TenLoaiPhong { get; set; }
        public string? TenPhong { get; set; }
        public int? SoLuong { get; set; }
        public decimal GiaPhong { get; set; }
        public decimal GiaSauGiam { get; set; }
        public string? ThoiGianKhuyenMai { get; set; }
        public virtual ChiTietPhong? ChiTietPhong { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public List<string> UrlImages { get; set; }
      
    }
}

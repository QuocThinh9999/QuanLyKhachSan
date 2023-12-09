namespace QuanLyKhachSan.Areas.Admin.Models.HoaDonAdmin
{
    public class HoaDonAdmin
    {
        public string IdHoaDon {  get; set; }
        public string TenNguoiDung { get; set; }
        public DateTime  GioCheckin { get; set; }
        public DateTime GioCheckout { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai {  get; set; }
        public string yeucau { get; set; }
        public string email { get; set; }
        public int? thongbao {  get; set; }
    }
}

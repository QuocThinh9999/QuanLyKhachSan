namespace QuanLyKhachSan.Areas.Admin.Models.HoaDonAdmin
{
    public class HoaDonAdmin
    {
        public string IdHoaDon {  get; set; }
        public string TenNguoiDung { get; set; }
        public string TenPhong {  get; set; }
        public DateTime  GioCheckin { get; set; }
        public DateTime GioCheckout { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai {  get; set; }
        public string yeucau { get; set; }
        public string email { get; set; }
        public int? thongbao {  get; set; }
    }
    public class ThongBao
    {
        public int MaLoi { get; set; }
        public string NoiDung { get; set; }
        public ThongBao() { }
        public ThongBao(int maLoi, string noiDung)
        {
            MaLoi = maLoi;
            NoiDung = noiDung;
        }
    }
}

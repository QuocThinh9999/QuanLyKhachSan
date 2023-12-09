namespace QuanLyKhachSan.Models.TrangPhong
{
    public class TrangPhong
    {
        public string id { get; set; } = null!;
        public string TenPhong { get; set; }
        public decimal GiaSauGiam { get; set; }
        public decimal GiaPhong { get; set; }
        public int? SoNguoiLon { get; set; }
        public int? SoTreEm { get; set; }
        public int? DienTich { get; set; }
        public string MoTa { get; set; }
        public string Url { get; set; }
    }
}

namespace QuanLyKhachSan.Models.TrangPhong
{
    public class ChiTietTrangPhong
    {
        public string id { get; set; } = null!;
        public string TenPhong { get; set; }
        public string? IdLoaiPhong { get; set; }
        public decimal GiaPhong { get; set; }
        public decimal GiaSauGiam { get; set; }
        public int? SoNguoiLon { get; set; }
        public int? SoTreEm { get; set; }
        public int? DienTich { get; set; }
        public string MoTa { get; set; }
        public List<string> UrlImages { get; set; }
        public List<PhongKhaDung> phongKhaDungs { get; set; }
        public class PhongKhaDung
            {
            public string id { get; set; }
            public string TenPhong { get;set; }
            public decimal GiaPhong { get; set; }
            public decimal GiaSauGiam { get; set; }
            public string Url {  get; set; }
            }
    }
}

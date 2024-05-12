namespace QuanLyKhachSan.Models.TrangChu
{
    public class TrangChu
    {
        
       public List<ThongTinWeb> thongTinWeb { get; set; }
       public List<PhongTrangChu> phong{ get; set; }
       public List<Blog> blog { get; set; }
        public class PhongTrangChu
        {
            public string id { get; set; } = null!;
            public string TenPhong { get; set; }

            public decimal GiaSauGiam { get; set; }
            public decimal GiaPhong { get; set; }
            public int? SoNguoiLon { get; set; }
            public int? SoTreEm { get; set; }
            public int? DienTich { get; set; }
            public string MoTa { get; set; }
            public string Url {  get; set; }
        }
        public DateTime GioCheckin { get; set; }

        public DateTime GioCheckout { get; set; }
        public int? SoNguoiLon { get; set; }

        public int? SoTreEm { get; set; }

        public decimal GiaPhongMin { get; set; }
        public decimal GiaPhongMax { get; set; }
        public string NgayKhongKhaDung { get; set; }
    }
}

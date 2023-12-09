namespace QuanLyKhachSan.Areas.Admin.Models
{
    public class CheckPhong
    {
        public DateTime GioCheckin { get; set; }

        public DateTime GioCheckout { get; set; }
        public int? SoNguoiLon { get; set; }

        public int? SoTreEm { get; set; }

        public decimal GiaPhongMin { get; set; }
        public decimal GiaPhongMax { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyKhachSan.Areas.Admin.Models
{
    public class ThemHoaDon
    {
        public string? IdPhong { get; set; }

        public DateTime GioCheckin { get; set; }

        public DateTime GioCheckout { get; set; }
       

    }
}

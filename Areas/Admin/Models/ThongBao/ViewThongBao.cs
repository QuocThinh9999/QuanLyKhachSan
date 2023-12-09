using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhachSan.Areas.Admin.Models.ThongBao
{
    public class ViewThongBao
    {
        public string? Email { get; set; }

       
        public DateTime? NgayGioNhan { get; set; }

        
        public string IdTuVan { get; set; } = null!;

        
        public string? Ten { get; set; }

        public string? LoiNhan { get; set; }
        public int? SoThongBao { get; set; }
    }
}

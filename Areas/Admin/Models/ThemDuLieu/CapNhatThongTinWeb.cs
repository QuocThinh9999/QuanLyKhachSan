namespace QuanLyKhachSan.Areas.Admin.Models.ThemDuLieu
{
    public class CapNhatThongTinWeb
    {
        public IFormFile? Images { get; set; }
        public string LoaiThongTin { get; set; } = null!;

        public string? MoTa { get; set; }
    }
}

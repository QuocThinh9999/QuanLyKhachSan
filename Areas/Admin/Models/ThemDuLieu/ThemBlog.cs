using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhachSan.Areas.Admin.Models.ThemDuLieu
{
    public class ThemBlog
    {
        public IFormFileCollection Images { get; set; }
        public string? MoTa { get; set; }
        public string? Tieude { get; set; }
    }
}

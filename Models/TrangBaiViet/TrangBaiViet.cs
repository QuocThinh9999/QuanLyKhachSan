using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhachSan.Models.TrangBaiViet
{
    public class TrangBaiViet
    {
        public string IdBlog { get; set; } = null!;

        public string? UrlImage { get; set; }

        public string? MoTa { get; set; }
        public string? Tieude { get; set; }
    }
}

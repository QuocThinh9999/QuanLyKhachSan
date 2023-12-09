using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QuanLyKhachSan.Models.TrangBaiViet
{
    public class ChiTietTrangBaiViet
    {
        public string IdBlog { get; set; } = null!;

        public string? MoTa { get; set; }
        public string? Tieude { get; set; }
        public List<string> UrlImage {  get; set; }
        public List<BaiVietKhaDung> baiVietKhaDungs { get; set; }
        public class BaiVietKhaDung
        {
            public string IdBlog { get; set; } = null!;

            public string? MoTa { get; set; }
            public string? Tieude { get; set; }
            public string UrlImage { get; set; }
        }
    }
}

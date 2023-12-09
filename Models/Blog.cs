using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("Blog")]
public partial class Blog
{
    [Key]
    public string IdBlog { get; set; } = null!;

    public string? UrlImage { get; set; }

    public string? MoTa { get; set; }

    public string? Tieude { get; set; }
}

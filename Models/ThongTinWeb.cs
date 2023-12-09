using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("ThongTinWeb")]
public partial class ThongTinWeb
{
    [Key]
    [StringLength(100)]
    public string LoaiThongTin { get; set; } = null!;

    public string? MoTa { get; set; }

    public string? UrlImages { get; set; }
}

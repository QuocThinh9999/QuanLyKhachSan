using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("ChiTietPhong")]
public partial class ChiTietPhong
{
    [Key]
    public string IdPhong { get; set; } = null!;

    public int? SoNguoiLon { get; set; }

    public int? SoTreEm { get; set; }

    public int? DienTich { get; set; }

    public string? MoTa { get; set; }

    [ForeignKey("IdPhong")]
    [InverseProperty("ChiTietPhong")]
    public virtual Phong IdPhongNavigation { get; set; } = null!;
}

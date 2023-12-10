using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("NguoiDung")]
public partial class NguoiDung
{
    [Key]
    public string IdNguoiDung { get; set; } = null!;

    [StringLength(450)]
    public string? IdQuyen { get; set; }

    [StringLength(100)]
    public string? Ten { get; set; }

    [StringLength(15)]
    public string? SoDienThoai { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(100)]
    public string? MatKhau { get; set; }

    [StringLength(10)]
    public string? GioiTinh { get; set; }

    [Column(TypeName = "date")]
    public DateTime? NgaySinh { get; set; }

    public string? AnhDaiDien { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("LoaiPhong")]
public partial class LoaiPhong
{
    [Key]
    public string IdLoaiPhong { get; set; } = null!;

    [StringLength(100)]
    public string? TenLoaiPhong { get; set; }
}

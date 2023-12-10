using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("XacThuc")]
public partial class XacThuc
{
    [Key]
    public string IdNguoiDung { get; set; } = null!;

    public int? TrangThai { get; set; }

    [StringLength(50)]
    public string? MaXacThuc { get; set; }
}

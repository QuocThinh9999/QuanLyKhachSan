using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("TuVan")]
public partial class TuVan
{
    [StringLength(100)]
    public string? Email { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayGioNhan { get; set; }

    [Key]
    public string IdTuVan { get; set; } = null!;

    [StringLength(255)]
    public string? Ten { get; set; }

    public string? LoiNhan { get; set; }
}

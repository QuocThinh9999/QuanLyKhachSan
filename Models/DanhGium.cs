using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

public partial class DanhGium
{
    [Key]
    public string IdHoaDon { get; set; } = null!;

    [StringLength(450)]
    public string? IdNguoiDung { get; set; }

    public int? SoSao { get; set; }

    public string? NhanXet { get; set; }
}

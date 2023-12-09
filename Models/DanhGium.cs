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

    [ForeignKey("IdHoaDon")]
    [InverseProperty("DanhGium")]
    public virtual HoaDon IdHoaDonNavigation { get; set; } = null!;

    [ForeignKey("IdNguoiDung")]
    [InverseProperty("DanhGia")]
    public virtual NguoiDung? IdNguoiDungNavigation { get; set; }
}

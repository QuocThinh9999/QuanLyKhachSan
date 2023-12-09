using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("HoaDon")]
public partial class HoaDon
{
    [Key]
    public string IdHoaDon { get; set; } = null!;

    [StringLength(450)]
    public string? IdNguoiDung { get; set; }

    [StringLength(450)]
    public string? IdPhong { get; set; }

    public int? SoTreEm { get; set; }

    public int? SoNguoiLon { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime GioCheckin { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime GioCheckout { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PhuThu { get; set; }

    [Column("VAT", TypeName = "decimal(18, 2)")]
    public decimal Vat { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TongTien { get; set; }

    [StringLength(450)]
    public string? TrangThai { get; set; }

    [StringLength(450)]
    public string? YeuCau { get; set; }

    [InverseProperty("IdHoaDonNavigation")]
    public virtual DanhGium? DanhGium { get; set; }

    [ForeignKey("IdNguoiDung")]
    [InverseProperty("HoaDons")]
    public virtual NguoiDung? IdNguoiDungNavigation { get; set; }

    [ForeignKey("IdPhong")]
    [InverseProperty("HoaDons")]
    public virtual Phong? IdPhongNavigation { get; set; }
}

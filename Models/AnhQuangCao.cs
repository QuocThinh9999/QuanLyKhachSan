using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuanLyKhachSan.Models;

[Table("AnhQuangCao")]
public partial class AnhQuangCao
{
    [Key]
    public string IdAnh { get; set; } = null!;

    [Column("UrlImageQC")]
    public string? UrlImageQc { get; set; }
}

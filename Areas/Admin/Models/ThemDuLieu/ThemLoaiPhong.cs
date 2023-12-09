using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace QuanLyKhachSan.Areas.Admin.Models	
{
	public class ThemLoaiPhong
	{
		[Required(ErrorMessage = "Vui lòng nhập tên loại phòng!")]
		public string TenLoaiPhong { get; set; }
	}
}

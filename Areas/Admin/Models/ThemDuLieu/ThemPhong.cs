using System.ComponentModel.DataAnnotations;

namespace QuanLyKhachSan.Areas.Admin.Models
{
	public class ThemPhong
	{
		[Required(ErrorMessage ="Vui lòng nhập tên phòng!")]
		public string TenPhong { get; set; }
		public string? IdLoaiPhong { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập số lượng phòng!")]
		public int? SoLuong { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập giá phòng!")]
		public decimal GiaPhong { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập số người lớn tối đa có thể chứa!")]
		public int? SoNguoiLon { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập số trẻ em tối đa có thể chứa!")]
		public int? SoTreEm { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập diện tích của phòng!")]
		public int? DienTich { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập mô tả phòng!")]
		public string MoTa { get; set; }
        public IFormFileCollection Images { get; set; }
        
    }
}

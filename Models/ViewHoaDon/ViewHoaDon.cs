namespace QuanLyKhachSan.Models.ViewHoaDon
{
	public class ViewHoaDon
	{
		public string? IdNguoiDung { get; set; }
		public string IdHoaDon {  get; set; }
		public string? IdPhong { get; set; }
		public int? SoTreEm { get; set; }
		public int? SoNguoiLon { get; set; }

		public DateTime GioCheckin { get; set; }

		public DateTime GioCheckout { get; set; }
		public int TongThoiGian { get; set; }
		public decimal PhuThu { get; set; }
		public decimal Vat { get; set; }
		public decimal TongTien { get; set; }
		public string url {  get; set; }
		public string trangthai { get; set; }
	}
}

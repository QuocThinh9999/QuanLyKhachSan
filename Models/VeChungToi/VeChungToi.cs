namespace QuanLyKhachSan.Models.VeChungToi
{
    public class VeChungToi
    {
        
        public List<PhongVeChungToi> phongVeChungTois { get; set; }
        public List<ThongTinWeb> thongTinWebs { get; set; }
        public class PhongVeChungToi
        {
            public string id { get; set; }
            public string? Name { get; set; }
            public string? Url { get; set; }
        }

    }
}

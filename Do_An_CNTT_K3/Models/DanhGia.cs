namespace Do_An_CNTT_K3.Models
{
    public class DanhGia
    {
        public int IdDanhGia { get; set; }
        public string? LoaiDanhGia { get; set; }
        public string? Ten { get; set; } 
        public string? Email { get; set; }
        public string? BinhLuan { get; set; }
        public int InformationSPIdSP { get; set; }
        public InformationSP? InformationSP { get; set; }
    }
}

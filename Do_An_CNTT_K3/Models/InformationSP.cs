namespace Do_An_CNTT_K3.Models
{
    public class InformationSP
    {
        public int IdSP { get; set; }
        public string? TenSP { get; set; }
        public string? MoTa { get; set; }
        public int PTgiamgia { get; set; }
        public int GiamGia { get; set; }
        public int GiaTien { get; set; }
        public string? View { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? LoaiSP { get; set; }
        public List<DanhGia>? DanhGias { get; set; }
    }
}

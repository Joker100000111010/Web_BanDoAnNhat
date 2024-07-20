namespace Do_An_CNTT_K3.Models
{
    public class HomePage
    {
        public IEnumerable<InformationSP> Products { get; set; }
        public InformationSP mota { get; set; }
        public IEnumerable<BaiViet> Posters { get; set; }
        public IEnumerable<ShoppingSP> ShoppingSPList { get; set; }
        public IEnumerable<DanhGia> DanhGias { get; set; }
        public DanhGia BinhLuan { get; set; }
    }
}

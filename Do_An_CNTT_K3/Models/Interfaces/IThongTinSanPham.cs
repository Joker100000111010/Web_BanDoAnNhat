namespace Do_An_CNTT_K3.Models.Interfaces
{
    public interface IThongTinSanPham
    {
        IEnumerable<InformationSP> GetAllProducts();
        InformationSP GetProductDetail(int id);
        IEnumerable<InformationSP> GetAllSeafood();
        IEnumerable<BaiViet> Poster();
        BaiViet PosterDetail(int id);
        List<InformationSP> SearchByKey(string key);
        void AddProduct(InformationSP product);
        void UpdateProduct(InformationSP product);
        void DeleteProduct(int id);
        public void AddBinhLuan(DanhGia Comment);
        List<DanhGia> GetDanhGiasByProductId(int productId);
    }
}

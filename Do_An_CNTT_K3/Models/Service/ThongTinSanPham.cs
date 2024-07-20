using Do_An_CNTT_K3.Data;
using Do_An_CNTT_K3.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Do_An_CNTT_K3.Models.Service
{
    public class ThongTinSanPham : IThongTinSanPham
    {
        private FoodDbContext dbContext;
        public ThongTinSanPham(FoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<InformationSP> GetAllProducts()
        {
            return dbContext.informationSPs;
        }
        public InformationSP? GetProductDetail(int id)
        {
            var product = dbContext.informationSPs.FirstOrDefault(p => p.IdSP == id);
            Console.WriteLine("Product: " + (product != null ? product.TenSP : "null"));
            return product;
        }
        public void AddBinhLuan(DanhGia Comment)
        {
            dbContext.danhGias.Add(Comment);
            dbContext.SaveChanges();
        }

        public List<DanhGia> GetDanhGiasByProductId(int productId)
        {
            return dbContext.danhGias.Where(dg => dg.InformationSPIdSP == productId).ToList();
        }
        public IEnumerable<InformationSP> GetAllSeafood()
        {
            return dbContext.informationSPs;
        }

        public IEnumerable<BaiViet> Poster()
        {
            return dbContext.baiViets;
        }
        public BaiViet? PosterDetail(int id)
        {
            return dbContext.baiViets.FirstOrDefault(p => p.IdBV == id);
        }
        public List<InformationSP> SearchByKey(string key)
        {
            return dbContext.informationSPs.FromSqlRaw("select * from informationSPs where TenSP like N'%" + key + "%'").ToList();
        }
        public void AddProduct(InformationSP product) // Triển khai phương thức này
        {
            dbContext.informationSPs.Add(product);
            dbContext.SaveChanges();
        }
        public void UpdateProduct(InformationSP product)
        {
            var existingProduct = dbContext.informationSPs.Find(product.IdSP);
            if (existingProduct != null)
            {
                existingProduct.TenSP = product.TenSP;
                existingProduct.MoTa = product.MoTa;
                existingProduct.PTgiamgia = product.PTgiamgia;
                existingProduct.GiamGia = product.GiamGia;
                existingProduct.GiaTien = product.GiaTien;
                existingProduct.View = product.View;
                existingProduct.Image1 = product.Image1;
                existingProduct.Image2 = product.Image2;
                existingProduct.LoaiSP = product.LoaiSP;

                dbContext.Update(existingProduct);
                dbContext.SaveChanges();
            }
        }



        public void DeleteProduct(int id)
        {
            var product = dbContext.informationSPs.Find(id);
            if (product != null)
            {
                dbContext.informationSPs.Remove(product);
                dbContext.SaveChanges();
            }
        }
    }


    
}

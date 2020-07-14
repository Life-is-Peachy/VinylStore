using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VinylStore.Models
{
    /// <summary>
    /// Данный класс реализует интерфейс IProductRepository для получения данных с применением EF.
    /// </summary>
    public class EFProductRepository : IProductRepository
    {
        private ProductDbContext DbContext;

        public EFProductRepository(ProductDbContext ctx)
            => this.DbContext = ctx;

        public IEnumerable<Product> Products
            => DbContext.Products;

        public void SaveProduct(Product product, ProductViewModel pvm)
        {
            Product Product = new Product
            {
                Album = pvm.Album,
                Artist = pvm.Artist,
                Description = pvm.Description,
                Genre = pvm.Genre,
                Year = pvm.Year,
                Price = pvm.Price
            };
            if (pvm.Logo != null)
            {
                byte[] imageData = null;
                using (var Reader = new BinaryReader(pvm.Logo.OpenReadStream()))
                {
                    imageData = Reader.ReadBytes((int)pvm.Logo.Length);
                }
                Product.Logo = imageData;
            }
            else return;
            if (product.ProductID == 0)
                DbContext.Products.Add(Product);
            else
            {
                Product dbEntry = DbContext.Products
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                dbEntry.Album = product.Album;
                dbEntry.Artist = product.Artist;
                dbEntry.Description = product.Description;
                dbEntry.Genre = product.Genre;
                dbEntry.Year = product.Year;
                dbEntry.Price = product.Price;
                dbEntry.Logo = Product.Logo;
            }
            DbContext.SaveChanges();
        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = DbContext.Products
                .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                DbContext.Products.Remove(dbEntry);
                DbContext.SaveChanges();
            }
            return dbEntry;
        }
    }
}
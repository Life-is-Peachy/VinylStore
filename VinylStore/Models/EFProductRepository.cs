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


    }
}

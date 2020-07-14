using System.Collections.Generic;

namespace VinylStore.Models
{
    /// <summary>
    /// Этот интерфейс использует IEnumerabe<T>, чтобы позволить вызывающему коду получать последовательность объектов Product,
    /// Ничего не сообщая о том,как или где хранятся либо извлекаются данные. Класс, зависящий от интерфейса IProductRepository,
    /// Может получать объекты Product, ничего не зная о том, откуда они поступают или каким образом класс реализации будет их доставлять.
    /// </summary>
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product, ProductViewModel pvm);
        Product DeleteProduct(int productID);
    }
}

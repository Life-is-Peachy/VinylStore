using System.Collections.Generic;
using System.Linq;

namespace VinylStore.Models
{
    public class Cart
    {
        private List<CartItem> CartCollection = new List<CartItem>();
        public virtual void AddItem(Product product, int quantity)
        {
            // Проверяtт Cart на наличие в нем конкретного продукта. Если не найден, присваивает null
            CartItem item = CartCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            // Если null то добавляет в Cart, если не null, то лишь добавляет новое кол-во
            if (item == null)
            {
                CartCollection.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            { item.Quantity += quantity; }
        }
        public virtual void RemoveItem(Product product)
            => CartCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        public virtual decimal ComputeTotalValue()
            => CartCollection.Sum(e => e.Product.Price * e.Quantity);
        public virtual void ClearCart()
            => CartCollection.Clear();
        public virtual IEnumerable<CartItem> Items
            => CartCollection;
    }
    public class CartItem
    {
        public int CartItemID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

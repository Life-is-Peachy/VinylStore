using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace VinylStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ProductDbContext context;

        public EFOrderRepository(ProductDbContext ctx)
            => this.context = ctx;

        public IEnumerable<Order> Orders => this.context.Orders
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Items.Select(i => i.Product));
            if (order.OrderID == 0)
            { context.Orders.Add(order); }
            context.SaveChanges();
        }
    }
}

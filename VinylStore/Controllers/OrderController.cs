using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Models;
using System.Linq;

namespace VinylStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repoService, Cart cartService)
        {
            this.repository = repoService;
            this.cart = cartService;
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Items.Count() == 0)
            { ModelState.AddModelError("", "Sorry, your cart is empty"); }
            if (ModelState.IsValid)
            {
                order.Items = cart.Items.ToArray();
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            { return View(order); }
        }

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders
                .FirstOrDefault(o => o.OrderID == orderID);
            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Completed()
        {
            cart.ClearCart();
            return View();
        }

        [Authorize]
        public ViewResult List()
            => View(repository.Orders.Where(o => !o.Shipped));

        public ViewResult Checkout()
            => View(new Order());



    }
}

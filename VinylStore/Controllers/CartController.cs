using Microsoft.AspNetCore.Mvc;
using VinylStore.Models;
using VinylStore.Models.ViewModels;
using System.Linq;

namespace VinylStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repository;
        private readonly Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            this.repository = repo;
            this.cart = cartService;
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            { cart.AddItem(product, 1); }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            { cart.RemoveItem(product); }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(string returnUrl)
            => View(new CartIndexViewModel
            {
                Cart = this.cart,
                ReturnUrl = returnUrl
            });
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylStore.Models;
using System.Linq;

namespace VinylStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
            => this.repository = repo;

        public ViewResult Index()
            => View(repository.Products);

        public ViewResult Edit(int productId)
            => View(repository.Products.FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product, ProductViewModel pvm)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product, pvm);
                TempData["message"] = $"The {product.Album} album by {product.Artist} has been saved!";
                return RedirectToAction("Index");
            }
            else
                return View(product);
        }

        public ViewResult Create()
            => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
                TempData["message"] = $"The {deletedProduct.Album} album was deleted!";
            return RedirectToAction("Index");
        }
    }
}

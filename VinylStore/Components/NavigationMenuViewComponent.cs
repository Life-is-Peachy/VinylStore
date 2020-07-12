using Microsoft.AspNetCore.Mvc;
using VinylStore.Models;
using System.Linq;

namespace VinylStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;
        public NavigationMenuViewComponent(IProductRepository repo)
            => this.repository = repo;
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenre = RouteData?.Values["genre"];
            return View(repository.Products
                .Select(x => x.Genre)
                .Distinct()
                .OrderBy(x => x)
                );
        }
    }
}

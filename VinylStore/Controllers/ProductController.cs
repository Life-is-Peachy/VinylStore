using Microsoft.AspNetCore.Mvc;
using VinylStore.Models;
using VinylStore.Models.ViewModels;
using System.Linq;

namespace VinylStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 9;
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
            => this.repository = repo;

        /// <summary>
        /// Этот метод помимо передачи объекта представлению задает логику разбиения на страницы.
        /// Задается сортировка по ID, берет количество элементов из переменной PageSize (Take),
        /// А Skip() пропускает количество выведенных на первой странице элементов, чтобы
        /// они не выводились на второй и т.д
        /// </summary>
        public ViewResult List(string genre, int page = 1)
            => View(new ProductListViewModel
            {
                Products = repository.Products
                .Where(p => genre == null || p.Genre == genre)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = genre == null
                    ? repository.Products.Count()
                    : repository.Products
                    .Where(g => g.Genre == genre)
                    .Count()
                },
                CurrentGenre = genre
            });
    }
}

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VinylStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter an artist name")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Please enter an album name")]
        public string Album { get; set; }

        [Required(ErrorMessage = "Please enter a genre of current album")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Please enter an year of relise")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please enter any description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a possitive price")]
        public decimal Price { get; set; }
        public byte[] Logo { get; set; }
    }
    public class ProductViewModel
    {
        public string Artist { get; set; }

        public string Album { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public IFormFile Logo { get; set; }
    }
}

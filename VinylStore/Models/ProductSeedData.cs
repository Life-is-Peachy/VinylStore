using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace VinylStore.Models
{
    /// <summary>
    /// Статический метод InitialBaseProductData() получает аргумент типа IServiceProvider, который является классом, используемым
    /// В методе Configure() класса Startup при регистрации классов промежуточного программного обеспечения для обработки НTТР-запросов;
    /// Именно здесь будет обеспечиваться наличие содержимого в базе данных.
    /// </summary>
    public class ProductSeedData
    {
        /// <summary>
        /// Метод InitialBaseProductData() получает объект ServiceProvider посредством интерфейса IServiceProvider и применяет его для проверки,
        /// Присутствуютли в базе данных какие-нибудь объекты Product.Если объектов нет, то база данных наполняется с использованием коллекции
        /// Объектов Product и метода AddRange(), после чего сохраняется с помощью метода SaveChanges().
        /// </summary>
        public static void InitialBaseProductData(IServiceProvider appBuilder)
        {
            ProductDbContext ProductDb = appBuilder.GetRequiredService<ProductDbContext>();
            if (!ProductDb.Products.Any())
            {
                ProductDb.Products.AddRange(
                new Product
                {
                    Album = "Life Is Peachy",
                    Artist = "KoRn",
                    Genre = "Heavy",
                    Year = 1996,
                    Description = "Write Latter",
                    Price = 1399
                },
                new Product
                {
                    Album = "Mezzanine",
                    Artist = "Massive Attack",
                    Genre = "Jaz",
                    Year = 1998,
                    Description = "Write Latter",
                    Price = 1299
                },
                new Product
                {
                    Album = "Meteora",
                    Artist = "Linkin Park",
                    Genre = "Heavy",
                    Year = 2001,
                    Description = "Write Latter",
                    Price = 1799
                },
                new Product
                {
                    Album = "IOWA",
                    Artist = "SlipKnot",
                    Year = 1999,
                    Genre = "Heavy",
                    Description = "Write Latter",
                    Price = 1599
                },
                new Product
                {
                    Album = "The Fat Of The Land",
                    Artist = "Prodigy",
                    Genre = "Electro",
                    Year = 1999,
                    Description = "Write Latter",
                    Price = 999
                },
                new Product
                {
                    Album = "Abbey Road",
                    Artist = "The Beatless",
                    Genre = "Rock",
                    Year = 1969,
                    Description = "Write Latter",
                    Price = 700
                },
                new Product
                {
                    Album = "Warriors Of The World",
                    Artist = "Manowar",
                    Genre = "Heavy",
                    Year = 1996,
                    Description = "Write Latter",
                    Price = 1199
                },
                new Product
                {
                    Album = "Blue Lines",
                    Artist = "Massive attack",
                    Genre = "Hip-Hop",
                    Year = 1996,
                    Description = "Write Latter",
                    Price = 1499
                },
                new Product
                {
                    Album = "Encore",
                    Artist = "Eminem",
                    Genre = "Rap",
                    Year = 2004,
                    Description = "Write Latter",
                    Price = 1699
                },
                new Product
                {
                    Album = "Fearless",
                    Artist = "Taylor Swift",
                    Genre = "Pop",
                    Year = 2008,
                    Description = "Write Latter",
                    Price = 1399
                });
                ProductDb.SaveChanges();
            }
        }
    }
}


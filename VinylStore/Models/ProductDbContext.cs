﻿using Microsoft.EntityFrameworkCore;

namespace VinylStore.Models
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options) { }
    }
}

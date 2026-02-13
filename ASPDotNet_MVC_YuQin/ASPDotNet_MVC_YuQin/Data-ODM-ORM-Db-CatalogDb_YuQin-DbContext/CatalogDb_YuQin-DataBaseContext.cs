using System;
using System.ComponentModel.DataAnnotations.Schema;
using CatalogDb_YuQin.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogDb_YuQin.DB.Data
{
    public class CatalogDb_YuQinDbContext : DbContext
    {
        public CatalogDb_YuQinDbContext(DbContextOptions<CatalogDb_YuQinDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Orders> OrdersDbSet { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>().ToTable("Orders").HasKey(o => o.OrderID);
        }
    }
}
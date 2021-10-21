using System;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Entities;

namespace ShopBridge.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options):base (options) 
        {

        }

        public DbSet<Inventory> Inventories { get; set; }
    }
}

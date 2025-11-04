using Microsoft.EntityFrameworkCore;
using GrocyApi.Models;

namespace GrocyApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<ShoppingList> ShoppingLists => Set<ShoppingList>();
        public DbSet<Item> Items => Set<Item>(); // <-- HIER!
    }
}
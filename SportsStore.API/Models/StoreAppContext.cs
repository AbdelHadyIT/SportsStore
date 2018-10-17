using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Models {
    public class StoreAppContext: DbContext {
        protected StoreAppContext(){}
        public StoreAppContext(DbContextOptions<StoreAppContext> options):base(options) {}
        public DbSet<Product> Products {get; set;}
        public DbSet<Rating> Ratings  { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}

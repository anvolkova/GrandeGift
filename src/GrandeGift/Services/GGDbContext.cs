//...
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GrandeGift.Models;

namespace GrandeGift.Services
{
    public class GGDbContext : IdentityDbContext
    {
        public DbSet<Profile> TblProfile { get; set; }
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<Hamper> TblHamper { get; set; }
        public DbSet<Address> TblAddress { get; set; }
        public DbSet<OrderLine> TblOrderLine { get; set; }
        public DbSet<Order> TblOrder { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=GGDatabase;Trusted_Connection=True");
        }
    }
}

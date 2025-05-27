using AgricultureSmart.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.DbAgriContext
{
    public class AgricultureSmartDbContext : DbContext
    {
        public AgricultureSmartDbContext(DbContextOptions<AgricultureSmartDbContext> options)
             : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        // Define DbSet properties for your entities
        public DbSet<Users> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SqlStreamStoreTests.Sql.Models;

namespace SqlStreamStoreTests.Sql.Infrastructure
{
    public class ReadContext : DbContext
    {
        public ReadContext(DbContextOptions<ReadContext> options) : base (options)
        {
            
        }

        public DbSet<ReadModel> ReadModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ReadModel>(e =>
            {
                
            });
        }
    }
}

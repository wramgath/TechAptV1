// Copyright © 2025 Always Active Technologies PTY Ltd

using Microsoft.EntityFrameworkCore;
using TechAptV1.Client.Models;

namespace TechAptV1.Client.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Number> Numbers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Number>().HasNoKey();
        }
    }
}

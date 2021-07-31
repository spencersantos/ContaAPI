using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bufunfa.Models;
using Microsoft.EntityFrameworkCore;

namespace Bufunfa.Repositories
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Conta> Conta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>(
                c =>
                {
                    c.ToTable("conta");
                    c.Property(e => e.NumeroConta).HasColumnName("numero_conta");

                });
        }
    }
}

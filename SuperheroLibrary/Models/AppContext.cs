using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SuperheroLibrary.Models
{
    public class AppContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Superability> Abilities { get; set; }
        public DbSet<Superhero> Heroes { get; set; }

        public AppContext(): base("DefaultConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Superhero>().HasMany(h => h.Abilities)
                .WithMany(a => a.Heroes)
                .Map(t => t.MapLeftKey("SuperheroId")
                .MapRightKey("SuperabilityId")
                .ToTable("SuperheroSuperability"));
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Rodrigo.Tech.Repository.Tables.Context;

namespace Rodrigo.Tech.Respository.Context
{
    public class FitnessDatabase : DbContext
    {
        public FitnessDatabase(DbContextOptions<FitnessDatabase> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExcerciseTypeIcon>()
                .HasIndex(c => c.ExcerciseTypeId)
                .IsUnique();
        }

        #region Tables
        public virtual DbSet<File> Files { get; set; }

        public virtual DbSet<Cache> Caches { get; set; }

        public virtual DbSet<Excercise> Excercises { get; set; }

        public virtual DbSet<ExcerciseTypeIcon> ExcerciseTypeMappings { get; set; }
        #endregion
    }
}
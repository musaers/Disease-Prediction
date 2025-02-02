using Microsoft.EntityFrameworkCore;
using HeartDiseaseAnalysis.Models;
using System.Reflection;



namespace HeartDiseaseAnalysis.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<HealthData>().HasNoKey();
            modelBuilder.Entity<HealthData>()
                .Property(e => e.heartdisease)
                .HasConversion<bool>(); // Bit to bool conversion

            modelBuilder.Entity<HealthData>()
                .Property(e => e.agecategory)
                .HasColumnType("character varying"); // Ensure string type mapping

            modelBuilder.Entity<StHealthData>()
                .Property(e => e.gender)
                .HasColumnType("character varying");


            // modelBuilder.Entity<HeartRiskAnalysisData>()
            //     .Property(e => e.diabetic)
            //     .HasConversion(
            //         v => v ? 1 : 0,
            //         v => v == 1);

            // base.OnModelCreating(modelBuilder);

            
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<HealthData> healthdata { get; set; }
        public DbSet <StHealthData> stroke {get; set;}

        public DbSet<DeathData> deaths { get; set; }
        public DbSet<CancerData> lungcancer{get;set;}
        public DbSet<RiskAnalysisViewModel> heartrisk{get;set;}
    }

}

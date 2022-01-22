using EdApp.AutoFill.DAL.Extensions;
using EdApp.AutoFill.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace EdApp.AutoFill.DAL
{
    public  class AutoFillContext : DbContext
    {
        public AutoFillContext(DbContextOptions<AutoFillContext> options)
            : base(options)
        {
        }

        public DbSet<AttributeDto> Attribute { get; set; }

        public DbSet<AttributesForSimocalc> AttributesForSimocalc { get; set; }

        public DbSet<CalculationType> CalculationType { get; set; }

        public DbSet<ModelType> ModelType { get; set; }

        public DbSet<Parameter> Parameter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.JoinOneToMany<AttributesForSimocalc, AttributeDto>(
                p => p.AttributesForSimocalc,
                d => d.Attributes,
                d => d.CalculationTypeId);

            modelBuilder.JoinOneToMany<CalculationType, AttributesForSimocalc>(
                p => p.CalculationType,
                d => d.AttributesForSimocalcs,
                d => d.CalculationTypeId);

            modelBuilder.JoinOneToMany<CalculationType, Parameter>(
                p => p.CalculationType,
                d => d.Parameters,
                d => d.CalculationTypeId);

            modelBuilder.JoinOneToMany<CalculationType, AttributeDto>(
                p => p.CalculationType,
                d => d.Attributes,
                d => d.CalculationTypeId);

            modelBuilder.JoinOneToMany<ModelType, Parameter>(
                p => p.ModelType,
                d => d.Parameters,
                d => d.ModelTypeId);

            // AttributeDto required constraints.
            modelBuilder.Entity<AttributeDto>()
                .Property(mt => mt.Name)
                .IsRequired();

            modelBuilder.Entity<AttributeDto>()
                .Property(mt => mt.AttributesForSimocalcId)
                .IsRequired();

            modelBuilder.Entity<AttributeDto>()
                .Property(mt => mt.CalculationTypeId)
                .IsRequired();

            // AttributesForSimocalc model required constraints.
            modelBuilder.Entity<AttributesForSimocalc>()
                .Property(mt => mt.CalculationTypeId)
                .IsRequired();

            // ModelType model required constraints.
            modelBuilder.Entity<ModelType>()
                .Property(mt => mt.Name)
                .IsRequired();

            // CalculationType model required constraints.
            modelBuilder.Entity<CalculationType>()
                .Property(ct => ct.Name)
                .IsRequired();

            // Parameter model required constraints.
            modelBuilder.Entity<Parameter>()
                .Property(p => p.ModelTypeId)
                .IsRequired();

            modelBuilder.Entity<Parameter>()
                .Property(p => p.CalculationTypeId)
                .IsRequired();

            modelBuilder.Entity<Parameter>()
                .Property(p => p.DataType)
                .IsRequired();

            modelBuilder.Entity<Parameter>()
                .Property(p => p.Field)
                .IsRequired();

            modelBuilder.Entity<Parameter>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<Parameter>()
                .Property(p => p.VariableName)
                .IsRequired();

            modelBuilder.Entity<Parameter>()
                .Property(p => p.RelevantForHash)
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Entity<Parameter>()
                .Property(p => p.Unit)
                .IsRequired();

            modelBuilder.Entity<Parameter>()
                .HasKey(p => p.Id);
        }
    }
}

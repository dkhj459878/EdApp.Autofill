﻿// <auto-generated />
using EdApp.AutoFill.DAL;
using EntityFrameworkCore.Jet.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EdApp.AutoFill.DAL.Migrations
{
    [DbContext(typeof(AutoFillContext))]
    partial class AutoFillContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EdApp.AutoFill.DAL.Model.AttributeDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttributesForSimocalcId")
                        .HasColumnType("integer");

                    b.Property<int>("CalculationTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("longchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longchar");

                    b.Property<string>("Unit")
                        .HasColumnType("longchar");

                    b.Property<string>("Value")
                        .HasColumnType("longchar");

                    b.HasKey("Id");

                    b.HasIndex("CalculationTypeId");

                    b.ToTable("Attribute");
                });

            modelBuilder.Entity("EdApp.AutoFill.DAL.Model.AttributesForSimocalc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CalculationTypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CalculationTypeId");

                    b.ToTable("AttributesForSimocalc");
                });

            modelBuilder.Entity("EdApp.AutoFill.DAL.Model.CalculationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longchar");

                    b.HasKey("Id");

                    b.ToTable("CalculationType");
                });

            modelBuilder.Entity("EdApp.AutoFill.DAL.Model.ModelType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longchar");

                    b.HasKey("Id");

                    b.ToTable("ModelType");
                });

            modelBuilder.Entity("EdApp.AutoFill.DAL.Model.Parameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Jet:ValueGenerationStrategy", JetValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CalculationTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("longchar");

                    b.Property<string>("DescriptionEn")
                        .HasColumnType("longchar");

                    b.Property<string>("ExampleFlatDoubleCadge")
                        .HasColumnType("longchar");

                    b.Property<string>("ExampleFlatRotorSingleCadge")
                        .HasColumnType("longchar");

                    b.Property<string>("ExampleRoundDoubleCadge")
                        .HasColumnType("longchar");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("longchar");

                    b.Property<bool>("MandatoryParameter")
                        .HasColumnType("smallint");

                    b.Property<bool>("MandatoryValue")
                        .HasColumnType("smallint");

                    b.Property<int>("ModelTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longchar");

                    b.Property<string>("ParentEntity")
                        .HasColumnType("longchar");

                    b.Property<bool>("RelevantForHash")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue(false);

                    b.Property<string>("UIName")
                        .HasColumnType("longchar");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("longchar");

                    b.Property<string>("VariableName")
                        .IsRequired()
                        .HasColumnType("longchar");

                    b.HasKey("Id");

                    b.HasIndex("CalculationTypeId");

                    b.HasIndex("ModelTypeId");

                    b.ToTable("Parameter");
                });

            modelBuilder.Entity("EdApp.AutoFill.DAL.Model.AttributeDto", b =>
                {
                    b.HasOne("EdApp.AutoFill.DAL.Model.AttributesForSimocalc", "AttributesForSimocalc")
                        .WithMany("Attributes")
                        .HasForeignKey("CalculationTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EdApp.AutoFill.DAL.Model.CalculationType", "CalculationType")
                        .WithMany("Attributes")
                        .HasForeignKey("CalculationTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EdApp.AutoFill.DAL.Model.AttributesForSimocalc", b =>
                {
                    b.HasOne("EdApp.AutoFill.DAL.Model.CalculationType", "CalculationType")
                        .WithMany("AttributesForSimocalcs")
                        .HasForeignKey("CalculationTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("EdApp.AutoFill.DAL.Model.Parameter", b =>
                {
                    b.HasOne("EdApp.AutoFill.DAL.Model.CalculationType", "CalculationType")
                        .WithMany("Parameters")
                        .HasForeignKey("CalculationTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EdApp.AutoFill.DAL.Model.ModelType", "ModelType")
                        .WithMany("Parameters")
                        .HasForeignKey("ModelTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

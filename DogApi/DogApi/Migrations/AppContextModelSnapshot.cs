﻿// <auto-generated />
using DogApi.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DogApi.Migrations
{
    [DbContext(typeof(DogApi.DataBase.AppContext))]
    partial class AppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DogApi.Models.Dog", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("name");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("color");

                    b.Property<decimal>("TailLength")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("tail_length");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("weight");

                    b.HasKey("Name");

                    b.ToTable("Dogs");
                });
#pragma warning restore 612, 618
        }
    }
}

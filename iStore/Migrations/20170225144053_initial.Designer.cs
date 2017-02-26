using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using iStore.Models;

namespace iStore.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20170225144053_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("iStore.Models.StoreItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("ID");

                    b.ToTable("StoreItems");
                });
        }
    }
}

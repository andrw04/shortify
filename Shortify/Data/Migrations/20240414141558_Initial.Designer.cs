﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shortify.Data;

#nullable disable

namespace Shortify.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240414141558_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shortify.Data.Entities.Link", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("id");

                    b.Property<int>("ClickCount")
                        .HasColumnType("int")
                        .HasColumnName("click_count");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created");

                    b.Property<string>("LongURL")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("long_url");

                    b.HasKey("Id");

                    b.ToTable("links");
                });
#pragma warning restore 612, 618
        }
    }
}

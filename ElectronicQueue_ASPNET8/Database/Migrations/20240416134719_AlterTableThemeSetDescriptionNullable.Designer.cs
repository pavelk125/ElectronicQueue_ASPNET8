﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcApp.Models;

#nullable disable

namespace ElectronicQueue.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240416134719_AlterTableThemeSetDescriptionNullable")]
    partial class AlterTableThemeSetDescriptionNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("ElectronicQueue.Database.Models.QueueItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CallTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndProcessTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("QueueNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("StartProcessTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("StatusId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ThemeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("ThemeId");

                    b.ToTable("QueueItems");
                });

            modelBuilder.Entity("ElectronicQueue.Database.Models.Status", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Number");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("ElectronicQueue.Database.Models.Theme", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ThemeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("ElectronicQueue.Database.Models.QueueItem", b =>
                {
                    b.HasOne("ElectronicQueue.Database.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElectronicQueue.Database.Models.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("Theme");
                });
#pragma warning restore 612, 618
        }
    }
}
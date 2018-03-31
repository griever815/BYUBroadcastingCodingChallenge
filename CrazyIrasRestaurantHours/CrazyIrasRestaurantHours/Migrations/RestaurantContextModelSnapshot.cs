﻿// <auto-generated />
using CrazyIrasRestaurantHours.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CrazyIrasRestaurantHours.Migrations
{
    [DbContext(typeof(RestaurantContext))]
    partial class RestaurantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrazyIrasRestaurantHours.Models.Restaurant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("CrazyIrasRestaurantHours.Models.RestaurantHasTime", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DayOfWeekInt");

                    b.Property<string>("DayOfWeekString");

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("RestaurantID");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("ID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("RestaurantHasTime");
                });

            modelBuilder.Entity("CrazyIrasRestaurantHours.Models.RestaurantHasTime", b =>
                {
                    b.HasOne("CrazyIrasRestaurantHours.Models.Restaurant")
                        .WithMany("RestaurantHasTimes")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using EFCore.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20221103113459_Migration3_GPS")]
    partial class Migration3_GPS
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EFCore.Model.Facility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FacItems")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacRules")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("GPS_lat")
                        .HasColumnType("float");

                    b.Property<double>("GPS_lon")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("EFCore.Model.MaintenanceIntervention", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("FacilityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TechnicianName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.ToTable("MaintenanceInterventions");
                });

            modelBuilder.Entity("EFCore.Model.Participant", b =>
                {
                    b.Property<long>("CPRNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("CPRNumber");

                    b.HasIndex("ReservationId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("EFCore.Model.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<int>("FacilityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<long>("UserCPRNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FacilityId");

                    b.HasIndex("UserCPRNumber");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("EFCore.Model.User", b =>
                {
                    b.Property<long>("CPRNumber")
                        .HasColumnType("bigint");

                    b.Property<int?>("CVR")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("CPRNumber");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("EFCore.Model.BusinessUser", b =>
                {
                    b.HasBaseType("EFCore.Model.User");

                    b.Property<int>("BusinessCVR")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("BusinessUser");
                });

            modelBuilder.Entity("EFCore.Model.MaintenanceIntervention", b =>
                {
                    b.HasOne("EFCore.Model.Facility", "Facility")
                        .WithMany("MaintenanceHistory")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facility");
                });

            modelBuilder.Entity("EFCore.Model.Participant", b =>
                {
                    b.HasOne("EFCore.Model.Reservation", "Reservation")
                        .WithMany("Participants")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("EFCore.Model.Reservation", b =>
                {
                    b.HasOne("EFCore.Model.Facility", "Facility")
                        .WithMany("Reservations")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCore.Model.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserCPRNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Facility");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EFCore.Model.Facility", b =>
                {
                    b.Navigation("MaintenanceHistory");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("EFCore.Model.Reservation", b =>
                {
                    b.Navigation("Participants");
                });

            modelBuilder.Entity("EFCore.Model.User", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}

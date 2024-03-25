﻿// <auto-generated />
using System;
using Healthcare.Api.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    [DbContext(typeof(HealthcareDbContext))]
    partial class HealthcareDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Healthcare")
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(true)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Description");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Number");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("PhoneNumber");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Street");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Address", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdState")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("IdState");

                    b.ToTable("City", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Country", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdAddress")
                        .HasColumnType("int");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAddress");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Doctor", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.DoctorHealthInsurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("HealthInsuranceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HealthInsuranceId");

                    b.HasIndex("DoctorId", "HealthInsuranceId")
                        .IsUnique();

                    b.ToTable("DoctorHealthInsurance", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.DoctorSpeciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("SpecialityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("DoctorId", "SpecialityId")
                        .IsUnique();

                    b.ToTable("DoctorSpeciality", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.HealthInsurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("HealthInsurance", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.HealthPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HealthInsuranceId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("HealthInsuranceId");

                    b.ToTable("HealthPlan", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.LaboratoryDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Basofilos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BilirrubinaDirecta")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BilirrubinaIndirecta")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BilirrubinaTotal")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CHCM")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ColesterolHdl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ColesterolTotal")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Creatininemia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Eosinofilos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Eritrosedimentacion1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Eritrosedimentacion2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FosfatasaAlcalina")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GlobulosBlancos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GlobulosRojos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Glucemia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HCM")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Hematocrito")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Hemoglobina")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Linfocitos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Monocitos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NeutrofilosCayados")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NeutrofilosSegmentados")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Plaquetas")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TirotrofinaPlamatica")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TransaminasaGlutamicoOxalac")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TransaminasaGlutamicoPiruvic")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Trigliceridos")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Uremia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Uricemia")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VCM")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LaboratoryDetail", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CUIL")
                        .HasColumnType("longtext");

                    b.Property<int>("IdAddress")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdAddress");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Patient", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.PatientHealthPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HealthPlanId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HealthPlanId");

                    b.HasIndex("PatientId", "HealthPlanId")
                        .IsUnique();

                    b.ToTable("PatientHealthPlan", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Speciality", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdCountry")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .IsUnicode(false)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("IdCountry");

                    b.ToTable("State", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Study", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LocationS3")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Note")
                        .HasColumnType("longtext");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("StudyTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("StudyTypeId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("StudyTypeId");

                    b.HasIndex("StudyTypeId1");

                    b.ToTable("Study", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.StudyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("StudyType", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastActivityDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", "Healthcare");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", "Healthcare");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", "Healthcare");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", "Healthcare");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", "Healthcare");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", "Healthcare");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Address", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.City", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("IdState")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Doctor", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("IdAddress")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthcare.Api.Core.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Healthcare.Api.Core.Entities.Doctor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.DoctorHealthInsurance", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthcare.Api.Core.Entities.HealthInsurance", "HealthInsurance")
                        .WithMany()
                        .HasForeignKey("HealthInsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("HealthInsurance");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.DoctorSpeciality", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.Doctor", "Doctor")
                        .WithMany("DoctorSpecialities")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthcare.Api.Core.Entities.Speciality", "Speciality")
                        .WithMany("DoctorSpecialities")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.HealthPlan", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.HealthInsurance", "HealthInsurance")
                        .WithMany("HealthPlans")
                        .HasForeignKey("HealthInsuranceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HealthInsurance");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Patient", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("IdAddress")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthcare.Api.Core.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Healthcare.Api.Core.Entities.Patient", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.PatientHealthPlan", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.HealthPlan", "HealthPlan")
                        .WithMany()
                        .HasForeignKey("HealthPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthcare.Api.Core.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HealthPlan");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.State", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("IdCountry")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Study", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.Patient", "Patient")
                        .WithMany("Studies")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthcare.Api.Core.Entities.StudyType", "StudyType")
                        .WithMany()
                        .HasForeignKey("StudyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthcare.Api.Core.Entities.StudyType", null)
                        .WithMany("Studies")
                        .HasForeignKey("StudyTypeId1");

                    b.Navigation("Patient");

                    b.Navigation("StudyType");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Healthcare.Api.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Healthcare.Api.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.City", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Country", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Doctor", b =>
                {
                    b.Navigation("DoctorSpecialities");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.HealthInsurance", b =>
                {
                    b.Navigation("HealthPlans");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Patient", b =>
                {
                    b.Navigation("Studies");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.Speciality", b =>
                {
                    b.Navigation("DoctorSpecialities");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.State", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Healthcare.Api.Core.Entities.StudyType", b =>
                {
                    b.Navigation("Studies");
                });
#pragma warning restore 612, 618
        }
    }
}

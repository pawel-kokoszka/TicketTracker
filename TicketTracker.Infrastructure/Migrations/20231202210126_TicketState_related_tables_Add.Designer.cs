﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketTracker.Infrastructure.DataBaseContext;

#nullable disable

namespace TicketTracker.Infrastructure.Migrations
{
    [DbContext(typeof(TicketTrackerDbContext))]
    [Migration("20231202210126_TicketState_related_tables_Add")]
    partial class TicketState_related_tables_Add
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Environment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EnvironmentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Environments");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.EnvironmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EnvironmentTypes");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.ProjectConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EnvironmentId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProjectConfigurations");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AssignedToUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEdited")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EditedByUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectConfigurationId")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketServiceId")
                        .HasColumnType("int");

                    b.Property<int>("TicketSlaId")
                        .HasColumnType("int");

                    b.Property<int>("TicketStateId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketPrioritiesGonfiguration", b =>
                {
                    b.Property<int>("TicketTypeConfigurationId")
                        .HasColumnType("int");

                    b.Property<int>("TicketPriorityId")
                        .HasColumnType("int");

                    b.Property<int>("PriorityOrderValue")
                        .HasColumnType("int");

                    b.Property<int>("TicketSlaConfigurationId")
                        .HasColumnType("int");

                    b.HasKey("TicketTypeConfigurationId", "TicketPriorityId");

                    b.ToTable("TicketPrioritiesGonfiguration");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketPriority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PriorityValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TicketPriorities");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketSla", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("SlaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("SlaValue")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("TicketSLAs");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketSlaConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TicketSlaConfigurationId")
                        .HasColumnType("int");

                    b.Property<int>("TicketSlaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TicketSlaConfigurations");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TicketStates");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketStatesGonfiguration", b =>
                {
                    b.Property<int>("TicketTypeConfigurationId")
                        .HasColumnType("int");

                    b.Property<int>("TicketStateId")
                        .HasColumnType("int");

                    b.Property<int>("TicketStateOrderValue")
                        .HasColumnType("int");

                    b.HasKey("TicketTypeConfigurationId", "TicketStateId");

                    b.ToTable("TicketStatesGonfigurations");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TicketTypes");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketTypeConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProjectConfigurationId")
                        .HasColumnType("int");

                    b.Property<int>("TicketTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TicketTypeConfigurations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Comment", b =>
                {
                    b.HasOne("TicketTracker.Domain.Entities.Ticket", "Ticket")
                        .WithMany("Comments")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Environment", b =>
                {
                    b.HasOne("TicketTracker.Domain.Entities.EnvironmentType", "EnvironmentType")
                        .WithOne("Environment")
                        .HasForeignKey("TicketTracker.Domain.Entities.Environment", "EnvironmentTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EnvironmentType");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.ProjectConfiguration", b =>
                {
                    b.HasOne("TicketTracker.Domain.Entities.Environment", "Environment")
                        .WithOne("ProjectConfiguration")
                        .HasForeignKey("TicketTracker.Domain.Entities.ProjectConfiguration", "EnvironmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TicketTracker.Domain.Entities.Project", "Project")
                        .WithOne("ProjectConfiguration")
                        .HasForeignKey("TicketTracker.Domain.Entities.ProjectConfiguration", "ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Environment");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Ticket", b =>
                {
                    b.HasOne("TicketTracker.Domain.Entities.ProjectConfiguration", "ProjectConfiguration")
                        .WithOne("Ticket")
                        .HasForeignKey("TicketTracker.Domain.Entities.Ticket", "ProjectConfigurationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TicketTracker.Domain.Entities.TicketType", "TicketType")
                        .WithOne("Ticket")
                        .HasForeignKey("TicketTracker.Domain.Entities.Ticket", "TypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ProjectConfiguration");

                    b.Navigation("TicketType");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketPrioritiesGonfiguration", b =>
                {
                    b.HasOne("TicketTracker.Domain.Entities.TicketPriority", "TicketPriority")
                        .WithOne("TicketPrioritiesGonfiguration")
                        .HasForeignKey("TicketTracker.Domain.Entities.TicketPrioritiesGonfiguration", "TicketPriorityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TicketTracker.Domain.Entities.TicketSlaConfiguration", "TicketSlaConfiguration")
                        .WithOne("TicketPrioritiesGonfiguration")
                        .HasForeignKey("TicketTracker.Domain.Entities.TicketPrioritiesGonfiguration", "TicketSlaConfigurationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TicketTracker.Domain.Entities.TicketTypeConfiguration", "TicketTypeConfiguration")
                        .WithOne("TicketPrioritiesGonfiguration")
                        .HasForeignKey("TicketTracker.Domain.Entities.TicketPrioritiesGonfiguration", "TicketTypeConfigurationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TicketPriority");

                    b.Navigation("TicketSlaConfiguration");

                    b.Navigation("TicketTypeConfiguration");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketSlaConfiguration", b =>
                {
                    b.HasOne("TicketTracker.Domain.Entities.TicketSla", "TicketSla")
                        .WithOne("TicketSlaConfiguration")
                        .HasForeignKey("TicketTracker.Domain.Entities.TicketSlaConfiguration", "TicketSlaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TicketSla");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketTypeConfiguration", b =>
                {
                    b.HasOne("TicketTracker.Domain.Entities.ProjectConfiguration", "ProjectConfiguration")
                        .WithOne("TicketTypeConfiguration")
                        .HasForeignKey("TicketTracker.Domain.Entities.TicketTypeConfiguration", "ProjectConfigurationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TicketTracker.Domain.Entities.TicketType", "TicketType")
                        .WithOne("TicTicketTypeConfiguration")
                        .HasForeignKey("TicketTracker.Domain.Entities.TicketTypeConfiguration", "TicketTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ProjectConfiguration");

                    b.Navigation("TicketType");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Environment", b =>
                {
                    b.Navigation("ProjectConfiguration");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.EnvironmentType", b =>
                {
                    b.Navigation("Environment");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Project", b =>
                {
                    b.Navigation("ProjectConfiguration");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.ProjectConfiguration", b =>
                {
                    b.Navigation("Ticket");

                    b.Navigation("TicketTypeConfiguration");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.Ticket", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketPriority", b =>
                {
                    b.Navigation("TicketPrioritiesGonfiguration");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketSla", b =>
                {
                    b.Navigation("TicketSlaConfiguration");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketSlaConfiguration", b =>
                {
                    b.Navigation("TicketPrioritiesGonfiguration");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketType", b =>
                {
                    b.Navigation("TicTicketTypeConfiguration");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("TicketTracker.Domain.Entities.TicketTypeConfiguration", b =>
                {
                    b.Navigation("TicketPrioritiesGonfiguration");
                });
#pragma warning restore 612, 618
        }
    }
}

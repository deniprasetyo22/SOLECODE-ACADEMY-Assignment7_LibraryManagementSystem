﻿// <auto-generated />
using System;
using Assignment5.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Assignment7.Persistence.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Assignment5.Domain.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Assignment5.Domain.Models.Book", b =>
                {
                    b.Property<int>("bookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("bookId"));

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("author")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("language")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<double?>("price")
                        .IsRequired()
                        .HasColumnType("double precision");

                    b.Property<string>("publisher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("purchaseDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("reason")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("status")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("totalBook")
                        .HasColumnType("integer");

                    b.HasKey("bookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Assignment5.Domain.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("userId"));

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("libraryCardNumber")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("notes")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("position")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("privilage")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("userId");

                    b.HasIndex("AppUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Assignment7.Domain.Models.Borrow", b =>
                {
                    b.Property<int>("Borrowid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("borrowid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Borrowid"));

                    b.Property<int>("Bookid")
                        .HasColumnType("integer")
                        .HasColumnName("bookid");

                    b.Property<DateOnly?>("Dateofborrow")
                        .HasColumnType("date")
                        .HasColumnName("dateofborrow");

                    b.Property<DateOnly?>("Dateofreturn")
                        .HasColumnType("date")
                        .HasColumnName("dateofreturn");

                    b.Property<DateOnly?>("Deadlinereturn")
                        .HasColumnType("date")
                        .HasColumnName("deadlinereturn");

                    b.Property<string>("Penalty")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("penalty");

                    b.Property<string>("Userid")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("userid");

                    b.HasKey("Borrowid");

                    b.HasIndex("Bookid");

                    b.HasIndex("Userid");

                    b.ToTable("borrow");
                });

            modelBuilder.Entity("Assignment7.Domain.Models.Nextsteprule", b =>
                {
                    b.Property<int>("Ruleid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ruleid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Ruleid"));

                    b.Property<string>("Conditiontype")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("conditiontype");

                    b.Property<string>("Conditionvalue")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("conditionvalue");

                    b.Property<int?>("Currentstepid")
                        .HasColumnType("integer")
                        .HasColumnName("currentstepid");

                    b.Property<int?>("Nextstepid")
                        .HasColumnType("integer")
                        .HasColumnName("nextstepid");

                    b.HasKey("Ruleid");

                    b.HasIndex("Currentstepid");

                    b.HasIndex("Nextstepid");

                    b.ToTable("nextsteprules");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Bookrequest", b =>
                {
                    b.Property<int>("Requestid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("requestid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Requestid"));

                    b.Property<string>("Author")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("author");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("Enddate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("enddate");

                    b.Property<string>("Isbn")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("isbn");

                    b.Property<int?>("Processid")
                        .HasColumnType("integer")
                        .HasColumnName("processid");

                    b.Property<string>("Publisher")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("publisher");

                    b.Property<string>("Requestname")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("requestname");

                    b.Property<DateTime?>("Startdate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("startdate");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("title");

                    b.HasKey("Requestid");

                    b.HasIndex("Processid");

                    b.ToTable("bookrequest");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Process", b =>
                {
                    b.Property<int>("Processid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("processid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Processid"));

                    b.Property<int?>("Currentstepid")
                        .HasColumnType("integer")
                        .HasColumnName("currentstepid");

                    b.Property<DateTime?>("Requestdate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("requestdate");

                    b.Property<string>("Requesterid")
                        .HasColumnType("character varying")
                        .HasColumnName("requesterid");

                    b.Property<string>("Requesttype")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("requesttype");

                    b.Property<string>("Status")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("status");

                    b.Property<int?>("Workflowid")
                        .HasColumnType("integer")
                        .HasColumnName("workflowid");

                    b.HasKey("Processid");

                    b.HasIndex("Currentstepid");

                    b.HasIndex("Requesterid");

                    b.HasIndex("Workflowid");

                    b.ToTable("process");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Workflow", b =>
                {
                    b.Property<int>("Workflowid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("workflowid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Workflowid"));

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Workflowname")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("workflowname");

                    b.HasKey("Workflowid");

                    b.ToTable("workflow");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Workflowaction", b =>
                {
                    b.Property<int>("Actionid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("actionid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Actionid"));

                    b.Property<string>("Action")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("action");

                    b.Property<DateTime?>("Actiondate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("actiondate");

                    b.Property<string>("Actorid")
                        .HasColumnType("character varying")
                        .HasColumnName("actorid");

                    b.Property<string>("Comments")
                        .HasColumnType("text")
                        .HasColumnName("comments");

                    b.Property<int?>("Requestid")
                        .HasColumnType("integer")
                        .HasColumnName("requestid");

                    b.Property<int?>("Stepid")
                        .HasColumnType("integer")
                        .HasColumnName("stepid");

                    b.HasKey("Actionid");

                    b.HasIndex("Actorid");

                    b.HasIndex("Requestid");

                    b.HasIndex("Stepid");

                    b.ToTable("workflowactions");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Workflowsequence", b =>
                {
                    b.Property<int>("Stepid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("stepid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Stepid"));

                    b.Property<string>("Requiredrole")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("requiredrole");

                    b.Property<string>("Stepname")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("stepname");

                    b.Property<int?>("Steporder")
                        .HasColumnType("integer")
                        .HasColumnName("steporder");

                    b.Property<int?>("Workflowid")
                        .HasColumnType("integer")
                        .HasColumnName("workflowid");

                    b.HasKey("Stepid");

                    b.HasIndex("Requiredrole");

                    b.HasIndex("Workflowid");

                    b.ToTable("workflowsequences");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Assignment5.Domain.Models.User", b =>
                {
                    b.HasOne("Assignment5.Domain.Models.AppUser", "AppUser")
                        .WithMany("Users")
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("Assignment7.Domain.Models.Borrow", b =>
                {
                    b.HasOne("Assignment5.Domain.Models.Book", "Book")
                        .WithMany("Borrows")
                        .HasForeignKey("Bookid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment5.Domain.Models.AppUser", "User")
                        .WithMany("Borrows")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Assignment7.Domain.Models.Nextsteprule", b =>
                {
                    b.HasOne("Assignment7.Persistence.Models.Workflowsequence", "Currentstep")
                        .WithMany("NextstepruleCurrentsteps")
                        .HasForeignKey("Currentstepid");

                    b.HasOne("Assignment7.Persistence.Models.Workflowsequence", "Nextstep")
                        .WithMany("NextstepruleNextsteps")
                        .HasForeignKey("Nextstepid");

                    b.Navigation("Currentstep");

                    b.Navigation("Nextstep");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Bookrequest", b =>
                {
                    b.HasOne("Assignment7.Persistence.Models.Process", "Process")
                        .WithMany("Bookrequests")
                        .HasForeignKey("Processid");

                    b.Navigation("Process");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Process", b =>
                {
                    b.HasOne("Assignment7.Persistence.Models.Workflowsequence", "Currentstep")
                        .WithMany("Processes")
                        .HasForeignKey("Currentstepid");

                    b.HasOne("Assignment5.Domain.Models.AppUser", "Requester")
                        .WithMany("Processes")
                        .HasForeignKey("Requesterid");

                    b.HasOne("Assignment7.Persistence.Models.Workflow", "Workflow")
                        .WithMany("Processes")
                        .HasForeignKey("Workflowid");

                    b.Navigation("Currentstep");

                    b.Navigation("Requester");

                    b.Navigation("Workflow");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Workflowaction", b =>
                {
                    b.HasOne("Assignment5.Domain.Models.AppUser", "Actor")
                        .WithMany("Workflowactions")
                        .HasForeignKey("Actorid");

                    b.HasOne("Assignment7.Persistence.Models.Process", "Request")
                        .WithMany("Workflowactions")
                        .HasForeignKey("Requestid");

                    b.HasOne("Assignment7.Persistence.Models.Workflowsequence", "Step")
                        .WithMany("Workflowactions")
                        .HasForeignKey("Stepid");

                    b.Navigation("Actor");

                    b.Navigation("Request");

                    b.Navigation("Step");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Workflowsequence", b =>
                {
                    b.HasOne("Assignment5.Domain.Models.AppUser", "RequiredroleNavigation")
                        .WithMany("Workflowsequences")
                        .HasForeignKey("Requiredrole");

                    b.HasOne("Assignment7.Persistence.Models.Workflow", "Workflow")
                        .WithMany("Workflowsequences")
                        .HasForeignKey("Workflowid");

                    b.Navigation("RequiredroleNavigation");

                    b.Navigation("Workflow");
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
                    b.HasOne("Assignment5.Domain.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Assignment5.Domain.Models.AppUser", null)
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

                    b.HasOne("Assignment5.Domain.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Assignment5.Domain.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Assignment5.Domain.Models.AppUser", b =>
                {
                    b.Navigation("Borrows");

                    b.Navigation("Processes");

                    b.Navigation("Users");

                    b.Navigation("Workflowactions");

                    b.Navigation("Workflowsequences");
                });

            modelBuilder.Entity("Assignment5.Domain.Models.Book", b =>
                {
                    b.Navigation("Borrows");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Process", b =>
                {
                    b.Navigation("Bookrequests");

                    b.Navigation("Workflowactions");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Workflow", b =>
                {
                    b.Navigation("Processes");

                    b.Navigation("Workflowsequences");
                });

            modelBuilder.Entity("Assignment7.Persistence.Models.Workflowsequence", b =>
                {
                    b.Navigation("NextstepruleCurrentsteps");

                    b.Navigation("NextstepruleNextsteps");

                    b.Navigation("Processes");

                    b.Navigation("Workflowactions");
                });
#pragma warning restore 612, 618
        }
    }
}

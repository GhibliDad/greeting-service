﻿// <auto-generated />
using System;
using GreetingService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GreetingService.Infrastructure.Migrations
{
    [DbContext(typeof(GreetingDbContext))]
    [Migration("20220301153759_CreatedInvoiceTable4")]
    partial class CreatedInvoiceTable4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GreetingService.Core.Entities.Greeting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("From");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("To");

                    b.ToTable("Greetings");
                });

            modelBuilder.Entity("GreetingService.Core.Entities.Invoice", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<decimal>("CostPerGreeting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SenderEmail");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("GreetingService.Core.Entities.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GreetingService.Core.Entities.Greeting", b =>
                {
                    b.HasOne("GreetingService.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("From")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("GreetingService.Core.Entities.Invoice", null)
                        .WithMany("Greetings")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("GreetingService.Core.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("To")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GreetingService.Core.Entities.Invoice", b =>
                {
                    b.HasOne("GreetingService.Core.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("GreetingService.Core.Entities.Invoice", b =>
                {
                    b.Navigation("Greetings");
                });
#pragma warning restore 612, 618
        }
    }
}

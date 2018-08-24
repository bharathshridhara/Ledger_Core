﻿// <auto-generated />
using LedgerCore.Data.Constants;
using LedgerCore.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LedgerCore.Data.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LedgerCore.Data.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("CurrencyId");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<float>("InterestRate");

                    b.Property<DateTime>("Modified")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("ModifiedBy");

                    b.Property<string>("Name");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LedgerCore.Data.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("Modified")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("ModifiedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Symbol");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("LedgerCore.Data.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AccountId");

                    b.Property<int>("Amount");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Description");

                    b.Property<Guid>("FromAccountId");

                    b.Property<DateTime>("Modified")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("ModifiedBy");

                    b.Property<string>("Name");

                    b.Property<string>("Recipient");

                    b.Property<Guid>("ToAccountId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("LedgerCore.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("DOB");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("Modified")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<Guid>("ModifiedBy");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LedgerCore.Data.Entities.Account", b =>
                {
                    b.HasOne("LedgerCore.Data.Entities.Currency", "Currency")
                        .WithMany("Accounts")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LedgerCore.Data.Entities.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LedgerCore.Data.Entities.Transaction", b =>
                {
                    b.HasOne("LedgerCore.Data.Entities.Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId");
                });
#pragma warning restore 612, 618
        }
    }
}

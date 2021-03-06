﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SqlStreamStoreTests.Sql.Infrastructure;
using System;

namespace SqlStreamStoreTests.Sql.Infrastructure.Migrations
{
    [DbContext(typeof(ReadContext))]
    [Migration("20180318151332_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SqlStreamStoreTests.Sql.Models.ReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("LastModifiedOn");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("ReadModels");
                });
#pragma warning restore 612, 618
        }
    }
}

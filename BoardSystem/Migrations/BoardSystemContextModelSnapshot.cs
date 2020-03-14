﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BoardSystem.DataContext;

namespace BoardSystem.Migrations
{
    [DbContext(typeof(BoardSystemContext))]
    partial class BoardSystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoardSystem.Models.Board", b =>
                {
                    b.Property<string>("BoardNum")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BoardContents")
                        .IsRequired();

                    b.Property<DateTime>("BoardDate");

                    b.Property<string>("BoardTitle")
                        .IsRequired();

                    b.Property<int>("BoardViews");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("BoardNum");

                    b.HasIndex("UserId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("BoardSystem.Models.User", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("UserDate");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("UserPassword")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BoardSystem.Models.Board", b =>
                {
                    b.HasOne("BoardSystem.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
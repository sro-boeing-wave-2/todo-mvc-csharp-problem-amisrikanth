﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoLists.Models;

namespace TodoLists.Migrations
{
    [DbContext(typeof(TodoListsContext))]
    [Migration("20180803040454_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TodoLists.Models.CheckList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Item");

                    b.Property<int?>("NoteId");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("CheckList");
                });

            modelBuilder.Entity("TodoLists.Models.Labels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Label");

                    b.Property<int?>("NoteId");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("TodoLists.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("TodoLists.Models.CheckList", b =>
                {
                    b.HasOne("TodoLists.Models.Note")
                        .WithMany("checkLists")
                        .HasForeignKey("NoteId");
                });

            modelBuilder.Entity("TodoLists.Models.Labels", b =>
                {
                    b.HasOne("TodoLists.Models.Note")
                        .WithMany("Labels")
                        .HasForeignKey("NoteId");
                });
#pragma warning restore 612, 618
        }
    }
}

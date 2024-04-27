﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace NardBackEnd.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Battle", b =>
                {
                    b.Property<int>("BattleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BattleId"));

                    b.Property<int>("BattleStatus")
                        .HasColumnType("int");

                    b.Property<string>("P1Moves")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P1StatBlock")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P2Moves")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("P2StatBlock")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PokemonId1")
                        .HasColumnType("int");

                    b.Property<int>("PokemonId2")
                        .HasColumnType("int");

                    b.Property<string>("Winner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("battlePhase")
                        .HasColumnType("int");

                    b.HasKey("BattleId");

                    b.ToTable("Battles");
                });

            modelBuilder.Entity("Models.Leaderboard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WinLoss")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Leaderboards");
                });

            modelBuilder.Entity("Models.Move", b =>
                {
                    b.Property<int>("MoveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MoveId"));

                    b.Property<int?>("Acc")
                        .HasColumnType("int");

                    b.Property<string>("DamageClass")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Power")
                        .HasColumnType("int");

                    b.Property<int?>("Pp")
                        .HasColumnType("int");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MoveId");

                    b.ToTable("Move");
                });

            modelBuilder.Entity("Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<string>("MovePool")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Satk")
                        .HasColumnType("int");

                    b.Property<int>("Sdef")
                        .HasColumnType("int");

                    b.Property<int>("Spd")
                        .HasColumnType("int");

                    b.Property<string>("Sprite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Types")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("Models.Track", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackId"));

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrackId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Bug")
                        .HasColumnType("real");

                    b.Property<float>("Dark")
                        .HasColumnType("real");

                    b.Property<float>("Dragon")
                        .HasColumnType("real");

                    b.Property<float>("Electric")
                        .HasColumnType("real");

                    b.Property<float>("Fairy")
                        .HasColumnType("real");

                    b.Property<float>("Fighting")
                        .HasColumnType("real");

                    b.Property<float>("Fire")
                        .HasColumnType("real");

                    b.Property<float>("Flying")
                        .HasColumnType("real");

                    b.Property<float>("Ghost")
                        .HasColumnType("real");

                    b.Property<float>("Grass")
                        .HasColumnType("real");

                    b.Property<float>("Ground")
                        .HasColumnType("real");

                    b.Property<float>("Ice")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Normal")
                        .HasColumnType("real");

                    b.Property<float>("Poison")
                        .HasColumnType("real");

                    b.Property<float>("Psychic")
                        .HasColumnType("real");

                    b.Property<float>("Rock")
                        .HasColumnType("real");

                    b.Property<float>("Shadow")
                        .HasColumnType("real");

                    b.Property<float>("Steel")
                        .HasColumnType("real");

                    b.Property<float>("Unknown")
                        .HasColumnType("real");

                    b.Property<float>("Water")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });
#pragma warning restore 612, 618
        }
    }
}

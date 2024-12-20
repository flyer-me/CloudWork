﻿// <auto-generated />
using System;
using CloudWork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CloudWork.Migrations
{
    [DbContext(typeof(Data.AppContext))]
    [Migration("20241218102534_DB")]
    partial class DB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("CloudWork.Models.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("CloudWork.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Language")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProblemId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SubmittedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.HasIndex("UserId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("CloudWork.Models.SubmissionEvaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EvaluatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

                    b.Property<bool>("IsPassed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SubmissionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionId")
                        .IsUnique();

                    b.ToTable("SubmissionEvaluations");
                });

            modelBuilder.Entity("CloudWork.Models.TestCase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExpectedOutput")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProblemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.ToTable("TestCases");
                });

            modelBuilder.Entity("CloudWork.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CloudWork.Models.Submission", b =>
                {
                    b.HasOne("CloudWork.Models.Problem", "Problem")
                        .WithMany("Submissions")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CloudWork.Models.User", "User")
                        .WithMany("Submissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Problem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CloudWork.Models.SubmissionEvaluation", b =>
                {
                    b.HasOne("CloudWork.Models.Submission", "Submission")
                        .WithOne("Evaluation")
                        .HasForeignKey("CloudWork.Models.SubmissionEvaluation", "SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("CloudWork.Models.TestCase", b =>
                {
                    b.HasOne("CloudWork.Models.Problem", "Problem")
                        .WithMany("TestCases")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("CloudWork.Models.Problem", b =>
                {
                    b.Navigation("Submissions");

                    b.Navigation("TestCases");
                });

            modelBuilder.Entity("CloudWork.Models.Submission", b =>
                {
                    b.Navigation("Evaluation")
                        .IsRequired();
                });

            modelBuilder.Entity("CloudWork.Models.User", b =>
                {
                    b.Navigation("Submissions");
                });
#pragma warning restore 612, 618
        }
    }
}

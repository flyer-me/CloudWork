﻿// <auto-generated />
using System;
using CloudWork.Common.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CloudWork.Migrations
{
    [DbContext(typeof(CloudWorkDbContext))]
    partial class CloudWorkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("CloudWork.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPublic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Questions");
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

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("CloudWork.Models.SubmissionEvaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EvaluatedAt")
                        .HasColumnType("TEXT");

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

                    b.Property<int>("QuestionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

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
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CloudWork.Models.Submission", b =>
                {
                    b.HasOne("CloudWork.Models.Question", "Question")
                        .WithMany("Submissions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CloudWork.Models.User", "User")
                        .WithMany("Submissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Question");

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
                    b.HasOne("CloudWork.Models.Question", "Question")
                        .WithMany("TestCases")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("CloudWork.Models.Question", b =>
                {
                    b.Navigation("Submissions");

                    b.Navigation("TestCases");
                });

            modelBuilder.Entity("CloudWork.Models.Submission", b =>
                {
                    b.Navigation("Evaluation");
                });

            modelBuilder.Entity("CloudWork.Models.User", b =>
                {
                    b.Navigation("Submissions");
                });
#pragma warning restore 612, 618
        }
    }
}

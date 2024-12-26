﻿using CloudWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CloudWork.Data
{
    public class CloudWorkDbContext : DbContext
    {
#pragma warning disable CS8618
        public CloudWorkDbContext(DbContextOptions<CloudWorkDbContext> options) : base(options)
#pragma warning restore CS8618
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<SubmissionEvaluation> SubmissionEvaluations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(System.Console.WriteLine, LogLevel.Information);
            // optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 模型约束和关系
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(p => p.IsPublic).HasDefaultValue(true);
                entity.HasIndex(entity => entity.Title).IsUnique();
            });

            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasOne(s => s.User)
                      .WithMany(u => u.Submissions)
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Question)
                      .WithMany(p => p.Submissions)
                      .HasForeignKey(s => s.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Evaluation)
                      .WithOne(se => se.Submission)
                      .HasForeignKey<SubmissionEvaluation>(se => se.SubmissionId);
            });

            modelBuilder.Entity<TestCase>(entity =>
            {
                entity.HasOne(tc => tc.Question)
                      .WithMany(p => p.TestCases)
                      .HasForeignKey(tc => tc.QuestionId);
            });

        }
    }
}
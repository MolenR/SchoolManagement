using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.MVC.Data;

public partial class SchoolManagementDbContext : DbContext
{
    public SchoolManagementDbContext(DbContextOptions<SchoolManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Lecture> Lectures { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC072F9CE6FA");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Classes__CourseI__3F466844");

            entity.HasOne(d => d.Lecture).WithMany(p => p.Classes)
                .HasForeignKey(d => d.LectureId)
                .HasConstraintName("FK__Classes__Lecture__3E52440B");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC07CC9D416E");

            entity.HasIndex(e => e.Code, "UQ__Courses__A25C5AA78A87537D").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(5);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Enrollme__3214EC078CB6F953");

            entity.Property(e => e.Grade).HasMaxLength(2);

            entity.HasOne(d => d.Class).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Enrollmen__Class__4316F928");

            entity.HasOne(d => d.Students).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentsId)
                .HasConstraintName("FK__Enrollmen__Stude__4222D4EF");
        });

        modelBuilder.Entity<Lecture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lectures__3214EC07D082EC1B");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC07AE7B99D0");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

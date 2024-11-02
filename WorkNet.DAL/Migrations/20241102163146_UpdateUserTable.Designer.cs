﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkNet.DAL.Data;

#nullable disable

namespace WorkNet.DAL.Migrations
{
    [DbContext(typeof(WorkNetDbContext))]
    [Migration("20241102163146_UpdateUserTable")]
    partial class UpdateUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CandidateSkill", b =>
                {
                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("CandidateId", "SkillId")
                        .HasName("PK__Candidat__B2A99284BEC366C6");

                    b.HasIndex("SkillId");

                    b.ToTable("CandidateSkills", (string)null);
                });

            modelBuilder.Entity("WorkNet.DAL.Models.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CandidateId"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<int?>("Experience")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ResumePath")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Skills")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CandidateId")
                        .HasName("PK__Candidat__DF539B9CCFBE8ABE");

                    b.HasIndex("UserId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.Employer", b =>
                {
                    b.Property<int>("EmployerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployerId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Industry")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EmployerId");

                    b.HasIndex("UserId");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.JobApplication", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationId"));

                    b.Property<DateTime?>("ApplicationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("ApplicationId")
                        .HasName("PK__JobAppli__C93A4C99CEA07194");

                    b.HasIndex("CandidateId");

                    b.HasIndex("JobId");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.JobCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CategoryId")
                        .HasName("PK__JobCateg__19093A0BC8C85DCE");

                    b.HasIndex(new[] { "CategoryName" }, "UQ__JobCateg__8517B2E0D80132D1")
                        .IsUnique();

                    b.ToTable("JobCategories");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.JobPosting", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("EmployerId")
                        .HasColumnType("int");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("JobRole")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("JobType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Openings")
                        .HasColumnType("int");

                    b.Property<DateOnly>("PostedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("SalaryRange")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("JobId")
                        .HasName("PK__JobPosti__056690C23EFFD224");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EmployerId");

                    b.ToTable("JobPostings");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("SkillId")
                        .HasName("PK__Skills__DFA09187FBB345A7");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CandidateSkill", b =>
                {
                    b.HasOne("WorkNet.DAL.Models.Candidate", null)
                        .WithMany()
                        .HasForeignKey("CandidateId")
                        .IsRequired()
                        .HasConstraintName("FK__Candidate__Candi__1EA48E88");

                    b.HasOne("WorkNet.DAL.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .IsRequired()
                        .HasConstraintName("FK__Candidate__Skill__1F98B2C1");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.Candidate", b =>
                {
                    b.HasOne("WorkNet.DAL.Models.User", "User")
                        .WithMany("Candidates")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Candidates_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.Employer", b =>
                {
                    b.HasOne("WorkNet.DAL.Models.User", "User")
                        .WithMany("Employers")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Employers_Users");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.JobApplication", b =>
                {
                    b.HasOne("WorkNet.DAL.Models.Candidate", "Candidate")
                        .WithMany("JobApplications")
                        .HasForeignKey("CandidateId")
                        .IsRequired()
                        .HasConstraintName("FK_JobApplications_Candidates");

                    b.HasOne("WorkNet.DAL.Models.JobPosting", "Job")
                        .WithMany("JobApplications")
                        .HasForeignKey("JobId")
                        .IsRequired()
                        .HasConstraintName("FK_JobApplications_JobPostings");

                    b.Navigation("Candidate");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.JobPosting", b =>
                {
                    b.HasOne("WorkNet.DAL.Models.JobCategory", "Category")
                        .WithMany("JobPostings")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_JobPostings_JobCategories");

                    b.HasOne("WorkNet.DAL.Models.Employer", "Employer")
                        .WithMany("JobPostings")
                        .HasForeignKey("EmployerId")
                        .IsRequired()
                        .HasConstraintName("FK_JobPostings_Employers");

                    b.Navigation("Category");

                    b.Navigation("Employer");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.Candidate", b =>
                {
                    b.Navigation("JobApplications");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.Employer", b =>
                {
                    b.Navigation("JobPostings");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.JobCategory", b =>
                {
                    b.Navigation("JobPostings");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.JobPosting", b =>
                {
                    b.Navigation("JobApplications");
                });

            modelBuilder.Entity("WorkNet.DAL.Models.User", b =>
                {
                    b.Navigation("Candidates");

                    b.Navigation("Employers");
                });
#pragma warning restore 612, 618
        }
    }
}

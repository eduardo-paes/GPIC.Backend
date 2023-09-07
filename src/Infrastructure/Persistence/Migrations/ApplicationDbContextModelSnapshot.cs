﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Activity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActivityTypeId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("Limits")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<double?>("Points")
                        .IsRequired()
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.ToTable("Activities", "public");
                });

            modelBuilder.Entity("Domain.Entities.ActivityType", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid?>("NoticeId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<string>("Unity")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.HasIndex("NoticeId");

                    b.ToTable("ActivityTypes", "public");
                });

            modelBuilder.Entity("Domain.Entities.Area", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("MainAreaId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("MainAreaId");

                    b.ToTable("Areas", "public");
                });

            modelBuilder.Entity("Domain.Entities.AssistanceType", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("AssistanceTypes", "public");
                });

            modelBuilder.Entity("Domain.Entities.Campus", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("Campuses", "public");
                });

            modelBuilder.Entity("Domain.Entities.Course", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("Courses", "public");
                });

            modelBuilder.Entity("Domain.Entities.MainArea", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("MainAreas", "public");
                });

            modelBuilder.Entity("Domain.Entities.Notice", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("AppealEndDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("AppealStartDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CreatedAt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("DocUrl")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime?>("EvaluationEndDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("EvaluationStartDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("FinalReportDeadline")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("PartialReportDeadline")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("RegistrationEndDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("RegistrationStartDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("SendingDocsEndDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("SendingDocsStartDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("SuspensionYears")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Notices", "public");
                });

            modelBuilder.Entity("Domain.Entities.Professor", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("IdentifyLattes")
                        .HasColumnType("bigint");

                    b.Property<string>("SIAPEEnrollment")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)");

                    b.Property<DateTime?>("SuspensionEndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Professors", "public");
                });

            modelBuilder.Entity("Domain.Entities.ProgramType", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.HasKey("Id");

                    b.ToTable("ProgramTypes", "public");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ActivitiesExecutionSchedule")
                        .HasMaxLength(1500)
                        .HasColumnType("character varying(1500)");

                    b.Property<DateTime?>("AppealDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AppealObservation")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CancellationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CancellationReason")
                        .HasColumnType("text");

                    b.Property<string>("CertificateUrl")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExpectedResults")
                        .HasMaxLength(1500)
                        .HasColumnType("character varying(1500)");

                    b.Property<bool>("IsScholarshipCandidate")
                        .HasColumnType("boolean");

                    b.Property<string>("KeyWord1")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("KeyWord2")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("KeyWord3")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Methodology")
                        .HasMaxLength(1500)
                        .HasColumnType("character varying(1500)");

                    b.Property<Guid?>("NoticeId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<string>("Objective")
                        .HasMaxLength(1500)
                        .HasColumnType("character varying(1500)");

                    b.Property<Guid?>("ProfessorId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProgramTypeId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StatusDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SubAreaId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("SubmissionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NoticeId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("ProgramTypeId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubAreaId");

                    b.ToTable("Projects", "public");
                });

            modelBuilder.Entity("Domain.Entities.ProjectActivity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActivityId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("FoundActivities")
                        .HasColumnType("integer");

                    b.Property<int?>("InformedActivities")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<Guid?>("ProjectId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectActivities", "public");
                });

            modelBuilder.Entity("Domain.Entities.ProjectEvaluation", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("APIndex")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValue(0.0);

                    b.Property<int>("AcademicScientificProductionCoherence")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("AppealEvaluationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AppealEvaluationDescription")
                        .HasColumnType("text");

                    b.Property<int?>("AppealEvaluationStatus")
                        .HasColumnType("integer");

                    b.Property<Guid?>("AppealEvaluatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DocumentsEvaluationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DocumentsEvaluationDescription")
                        .HasColumnType("text");

                    b.Property<Guid?>("DocumentsEvaluatorId")
                        .HasColumnType("uuid");

                    b.Property<int>("EffectiveContributionToResearch")
                        .HasColumnType("integer");

                    b.Property<double>("FinalScore")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValue(0.0);

                    b.Property<bool?>("IsProductivityFellow")
                        .IsRequired()
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ProjectId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<int>("ProjectProposalObjectives")
                        .HasColumnType("integer");

                    b.Property<int>("ProposalMethodologyAdaptation")
                        .HasColumnType("integer");

                    b.Property<int>("Qualification")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("SubmissionEvaluationDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SubmissionEvaluationDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SubmissionEvaluationStatus")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SubmissionEvaluatorId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppealEvaluatorId");

                    b.HasIndex("DocumentsEvaluatorId");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.HasIndex("SubmissionEvaluatorId");

                    b.ToTable("ProjectEvaluations", "public");
                });

            modelBuilder.Entity("Domain.Entities.ProjectFinalReport", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ProjectId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<string>("ReportUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("SendDate")
                        .IsRequired()
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectFinalReports", "public");
                });

            modelBuilder.Entity("Domain.Entities.ProjectPartialReport", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("text");

                    b.Property<int>("CurrentDevelopmentStage")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ProjectId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<int>("ScholarPerformance")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectPartialReports", "public");
                });

            modelBuilder.Entity("Domain.Entities.Student", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssistanceTypeId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("CEP")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("CampusId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<long?>("CellPhone")
                        .HasColumnType("bigint");

                    b.Property<int?>("CellPhoneDDD")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid?>("CourseId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DispatchDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("HomeAddress")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("IssuingAgency")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long?>("Phone")
                        .HasColumnType("bigint");

                    b.Property<int?>("PhoneDDD")
                        .HasColumnType("integer");

                    b.Property<long>("RG")
                        .HasColumnType("bigint");

                    b.Property<int>("Race")
                        .HasColumnType("integer");

                    b.Property<string>("RegistrationCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("StartYear")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character(2)")
                        .IsFixedLength();

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AssistanceTypeId");

                    b.HasIndex("CampusId");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Students", "public");
                });

            modelBuilder.Entity("Domain.Entities.StudentDocuments", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AccountOpeningProof")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AgencyNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("IdentityDocument")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ParentalAuthorization")
                        .HasColumnType("text");

                    b.Property<string>("Photo3x4")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ProjectId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<string>("ScholarCommitmentAgreement")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SchoolHistory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("StudentDocuments", "public");
                });

            modelBuilder.Entity("Domain.Entities.SubArea", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AreaId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("SubAreas", "public");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCoordinator")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("ResetPasswordToken")
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("ValidationCode")
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.HasKey("Id");

                    b.ToTable("Users", "public");
                });

            modelBuilder.Entity("Domain.Entities.Activity", b =>
                {
                    b.HasOne("Domain.Entities.ActivityType", "ActivityType")
                        .WithMany("Activities")
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActivityType");
                });

            modelBuilder.Entity("Domain.Entities.ActivityType", b =>
                {
                    b.HasOne("Domain.Entities.Notice", "Notice")
                        .WithMany()
                        .HasForeignKey("NoticeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notice");
                });

            modelBuilder.Entity("Domain.Entities.Area", b =>
                {
                    b.HasOne("Domain.Entities.MainArea", "MainArea")
                        .WithMany()
                        .HasForeignKey("MainAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainArea");
                });

            modelBuilder.Entity("Domain.Entities.Professor", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Professor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Project", b =>
                {
                    b.HasOne("Domain.Entities.Notice", "Notice")
                        .WithMany()
                        .HasForeignKey("NoticeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ProgramType", "ProgramType")
                        .WithMany()
                        .HasForeignKey("ProgramTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("Domain.Entities.SubArea", "SubArea")
                        .WithMany()
                        .HasForeignKey("SubAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Notice");

                    b.Navigation("Professor");

                    b.Navigation("ProgramType");

                    b.Navigation("Student");

                    b.Navigation("SubArea");
                });

            modelBuilder.Entity("Domain.Entities.ProjectActivity", b =>
                {
                    b.HasOne("Domain.Entities.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.ProjectEvaluation", b =>
                {
                    b.HasOne("Domain.Entities.User", "AppealEvaluator")
                        .WithMany()
                        .HasForeignKey("AppealEvaluatorId");

                    b.HasOne("Domain.Entities.User", "DocumentsEvaluator")
                        .WithMany()
                        .HasForeignKey("DocumentsEvaluatorId");

                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.ProjectEvaluation", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "SubmissionEvaluator")
                        .WithMany()
                        .HasForeignKey("SubmissionEvaluatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppealEvaluator");

                    b.Navigation("DocumentsEvaluator");

                    b.Navigation("Project");

                    b.Navigation("SubmissionEvaluator");
                });

            modelBuilder.Entity("Domain.Entities.ProjectFinalReport", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.ProjectPartialReport", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Student", b =>
                {
                    b.HasOne("Domain.Entities.AssistanceType", "AssistanceType")
                        .WithMany()
                        .HasForeignKey("AssistanceTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Campus", "Campus")
                        .WithMany()
                        .HasForeignKey("CampusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Student", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssistanceType");

                    b.Navigation("Campus");

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.StudentDocuments", b =>
                {
                    b.HasOne("Domain.Entities.Project", "Project")
                        .WithOne()
                        .HasForeignKey("Domain.Entities.StudentDocuments", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Domain.Entities.SubArea", b =>
                {
                    b.HasOne("Domain.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Domain.Entities.ActivityType", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}

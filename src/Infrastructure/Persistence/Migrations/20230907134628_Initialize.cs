using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "AssistanceTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campuses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainAreas",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EvaluationStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EvaluationEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AppealStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AppealEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SendingDocsStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SendingDocsEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PartialReportDeadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinalReportDeadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SuspensionYears = table.Column<int>(type: "integer", nullable: false),
                    DocUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'"),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProgramTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Password = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    CPF = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    ValidationCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    IsCoordinator = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MainAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_MainAreas_MainAreaId",
                        column: x => x.MainAreaId,
                        principalSchema: "public",
                        principalTable: "MainAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Unity = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    NoticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityTypes_Notices_NoticeId",
                        column: x => x.NoticeId,
                        principalSchema: "public",
                        principalTable: "Notices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SIAPEEnrollment = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    IdentifyLattes = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SuspensionEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professors_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RG = table.Column<long>(type: "bigint", nullable: false),
                    IssuingAgency = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DispatchDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Race = table.Column<int>(type: "integer", nullable: false),
                    HomeAddress = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UF = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: false),
                    CEP = table.Column<long>(type: "bigint", nullable: false),
                    PhoneDDD = table.Column<int>(type: "integer", nullable: true),
                    Phone = table.Column<long>(type: "bigint", nullable: true),
                    CellPhoneDDD = table.Column<int>(type: "integer", nullable: true),
                    CellPhone = table.Column<long>(type: "bigint", nullable: true),
                    CampusId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartYear = table.Column<string>(type: "text", nullable: false),
                    AssistanceTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AssistanceTypes_AssistanceTypeId",
                        column: x => x.AssistanceTypeId,
                        principalSchema: "public",
                        principalTable: "AssistanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalSchema: "public",
                        principalTable: "Campuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "public",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubAreas",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAreas_Areas_AreaId",
                        column: x => x.AreaId,
                        principalSchema: "public",
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Points = table.Column<double>(type: "double precision", nullable: false),
                    Limits = table.Column<double>(type: "double precision", nullable: true),
                    ActivityTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalSchema: "public",
                        principalTable: "ActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    KeyWord1 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    KeyWord2 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    KeyWord3 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsScholarshipCandidate = table.Column<bool>(type: "boolean", nullable: false),
                    Objective = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    Methodology = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    ExpectedResults = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    ActivitiesExecutionSchedule = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true),
                    ProgramTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfessorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubAreaId = table.Column<Guid>(type: "uuid", nullable: false),
                    NoticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StatusDescription = table.Column<string>(type: "text", nullable: false),
                    AppealObservation = table.Column<string>(type: "text", nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AppealDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancellationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancellationReason = table.Column<string>(type: "text", nullable: true),
                    CertificateUrl = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Notices_NoticeId",
                        column: x => x.NoticeId,
                        principalSchema: "public",
                        principalTable: "Notices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalSchema: "public",
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProgramTypes_ProgramTypeId",
                        column: x => x.ProgramTypeId,
                        principalSchema: "public",
                        principalTable: "ProgramTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "public",
                        principalTable: "Students",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_SubAreas_SubAreaId",
                        column: x => x.SubAreaId,
                        principalSchema: "public",
                        principalTable: "SubAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActivities",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InformedActivities = table.Column<int>(type: "integer", nullable: false),
                    FoundActivities = table.Column<int>(type: "integer", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "public",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectActivities_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectEvaluations",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsProductivityFellow = table.Column<bool>(type: "boolean", nullable: false),
                    SubmissionEvaluatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubmissionEvaluationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SubmissionEvaluationStatus = table.Column<int>(type: "integer", nullable: false),
                    SubmissionEvaluationDescription = table.Column<string>(type: "text", nullable: false),
                    AppealEvaluatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    AppealEvaluationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AppealEvaluationStatus = table.Column<int>(type: "integer", nullable: true),
                    AppealEvaluationDescription = table.Column<string>(type: "text", nullable: true),
                    DocumentsEvaluatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocumentsEvaluationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DocumentsEvaluationDescription = table.Column<string>(type: "text", nullable: true),
                    APIndex = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    FinalScore = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    Qualification = table.Column<int>(type: "integer", nullable: false),
                    ProjectProposalObjectives = table.Column<int>(type: "integer", nullable: false),
                    AcademicScientificProductionCoherence = table.Column<int>(type: "integer", nullable: false),
                    ProposalMethodologyAdaptation = table.Column<int>(type: "integer", nullable: false),
                    EffectiveContributionToResearch = table.Column<int>(type: "integer", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectEvaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectEvaluations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectEvaluations_Users_AppealEvaluatorId",
                        column: x => x.AppealEvaluatorId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectEvaluations_Users_DocumentsEvaluatorId",
                        column: x => x.DocumentsEvaluatorId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectEvaluations_Users_SubmissionEvaluatorId",
                        column: x => x.SubmissionEvaluatorId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFinalReports",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportUrl = table.Column<string>(type: "text", nullable: false),
                    SendDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFinalReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFinalReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectFinalReports_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPartialReports",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentDevelopmentStage = table.Column<int>(type: "integer", nullable: false),
                    ScholarPerformance = table.Column<int>(type: "integer", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPartialReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPartialReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectPartialReports_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentDocuments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    IdentityDocument = table.Column<string>(type: "text", nullable: false),
                    CPF = table.Column<string>(type: "text", nullable: false),
                    Photo3x4 = table.Column<string>(type: "text", nullable: false),
                    SchoolHistory = table.Column<string>(type: "text", nullable: false),
                    ScholarCommitmentAgreement = table.Column<string>(type: "text", nullable: false),
                    ParentalAuthorization = table.Column<string>(type: "text", nullable: true),
                    AgencyNumber = table.Column<string>(type: "text", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: false),
                    AccountOpeningProof = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentDocuments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityTypeId",
                schema: "public",
                table: "Activities",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTypes_NoticeId",
                schema: "public",
                table: "ActivityTypes",
                column: "NoticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_MainAreaId",
                schema: "public",
                table: "Areas",
                column: "MainAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_UserId",
                schema: "public",
                table: "Professors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_ActivityId",
                schema: "public",
                table: "ProjectActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivities_ProjectId",
                schema: "public",
                table: "ProjectActivities",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluations_AppealEvaluatorId",
                schema: "public",
                table: "ProjectEvaluations",
                column: "AppealEvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluations_DocumentsEvaluatorId",
                schema: "public",
                table: "ProjectEvaluations",
                column: "DocumentsEvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluations_ProjectId",
                schema: "public",
                table: "ProjectEvaluations",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectEvaluations_SubmissionEvaluatorId",
                schema: "public",
                table: "ProjectEvaluations",
                column: "SubmissionEvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFinalReports_ProjectId",
                schema: "public",
                table: "ProjectFinalReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFinalReports_UserId",
                schema: "public",
                table: "ProjectFinalReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPartialReports_ProjectId",
                schema: "public",
                table: "ProjectPartialReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPartialReports_UserId",
                schema: "public",
                table: "ProjectPartialReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_NoticeId",
                schema: "public",
                table: "Projects",
                column: "NoticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProfessorId",
                schema: "public",
                table: "Projects",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProgramTypeId",
                schema: "public",
                table: "Projects",
                column: "ProgramTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_StudentId",
                schema: "public",
                table: "Projects",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SubAreaId",
                schema: "public",
                table: "Projects",
                column: "SubAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDocuments_ProjectId",
                schema: "public",
                table: "StudentDocuments",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_AssistanceTypeId",
                schema: "public",
                table: "Students",
                column: "AssistanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CampusId",
                schema: "public",
                table: "Students",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseId",
                schema: "public",
                table: "Students",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                schema: "public",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubAreas_AreaId",
                schema: "public",
                table: "SubAreas",
                column: "AreaId");

            Persistence.Seeds.Seeder.Execute(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectActivities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProjectEvaluations",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProjectFinalReports",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProjectPartialReports",
                schema: "public");

            migrationBuilder.DropTable(
                name: "StudentDocuments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ActivityTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Professors",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProgramTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Students",
                schema: "public");

            migrationBuilder.DropTable(
                name: "SubAreas",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Notices",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AssistanceTypes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Campuses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Areas",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MainAreas",
                schema: "public");
        }
    }
}

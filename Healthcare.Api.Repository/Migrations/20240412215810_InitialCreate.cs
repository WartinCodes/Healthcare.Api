using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Healthcare");

            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HealthInsurance",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthInsurance", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Speciality",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudyType",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyType", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Support",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    Priority = table.Column<string>(type: "longtext", nullable: false),
                    Module = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ResolutionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Support", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Healthcare",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "State",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Country_IdCountry",
                        column: x => x.IdCountry,
                        principalSchema: "Healthcare",
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HealthPlan",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    HealthInsuranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthPlan_HealthInsurance_HealthInsuranceId",
                        column: x => x.HealthInsuranceId,
                        principalSchema: "Healthcare",
                        principalTable: "HealthInsurance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "City",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    IdState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_State_IdState",
                        column: x => x.IdState,
                        principalSchema: "Healthcare",
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "Healthcare",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Photo = table.Column<string>(type: "longtext", nullable: false),
                    LastActivityDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "longtext", nullable: true),
                    IdAddress = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Address_IdAddress",
                        column: x => x.IdAddress,
                        principalSchema: "Healthcare",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Healthcare",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "Healthcare",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Healthcare",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "Healthcare",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Healthcare",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Healthcare",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "Healthcare",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Healthcare",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Doctor",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Matricula = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Healthcare",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patient",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CUIL = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Healthcare",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DoctorHealthInsurance",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    HealthInsuranceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorHealthInsurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorHealthInsurance_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Healthcare",
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorHealthInsurance_HealthInsurance_HealthInsuranceId",
                        column: x => x.HealthInsuranceId,
                        principalSchema: "Healthcare",
                        principalTable: "HealthInsurance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DoctorSpeciality",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    SpecialityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSpeciality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSpeciality_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "Healthcare",
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorSpeciality_Speciality_SpecialityId",
                        column: x => x.SpecialityId,
                        principalSchema: "Healthcare",
                        principalTable: "Speciality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PatientHealthPlan",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    HealthPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientHealthPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientHealthPlan_HealthPlan_HealthPlanId",
                        column: x => x.HealthPlanId,
                        principalSchema: "Healthcare",
                        principalTable: "HealthPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientHealthPlan_Patient_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Healthcare",
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Study",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LocationS3 = table.Column<string>(type: "longtext", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    StudyTypeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Study_Patient_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Healthcare",
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Study_StudyType_StudyTypeId",
                        column: x => x.StudyTypeId,
                        principalSchema: "Healthcare",
                        principalTable: "StudyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LaboratoryDetail",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdStudy = table.Column<int>(type: "int", nullable: false),
                    GlobulosRojos = table.Column<string>(type: "longtext", nullable: false),
                    GlobulosBlancos = table.Column<string>(type: "longtext", nullable: false),
                    Hemoglobina = table.Column<string>(type: "longtext", nullable: false),
                    Hematocrito = table.Column<string>(type: "longtext", nullable: false),
                    VCM = table.Column<string>(type: "longtext", nullable: false),
                    HCM = table.Column<string>(type: "longtext", nullable: false),
                    CHCM = table.Column<string>(type: "longtext", nullable: false),
                    NeutrofilosCayados = table.Column<string>(type: "longtext", nullable: false),
                    NeutrofilosSegmentados = table.Column<string>(type: "longtext", nullable: false),
                    Eosinofilos = table.Column<string>(type: "longtext", nullable: false),
                    Basofilos = table.Column<string>(type: "longtext", nullable: false),
                    Linfocitos = table.Column<string>(type: "longtext", nullable: false),
                    Monocitos = table.Column<string>(type: "longtext", nullable: false),
                    Eritrosedimentacion1 = table.Column<string>(type: "longtext", nullable: false),
                    Eritrosedimentacion2 = table.Column<string>(type: "longtext", nullable: false),
                    Plaquetas = table.Column<string>(type: "longtext", nullable: false),
                    Glucemia = table.Column<string>(type: "longtext", nullable: false),
                    Uremia = table.Column<string>(type: "longtext", nullable: false),
                    Creatininemia = table.Column<string>(type: "longtext", nullable: false),
                    ColesterolTotal = table.Column<string>(type: "longtext", nullable: false),
                    ColesterolHdl = table.Column<string>(type: "longtext", nullable: false),
                    Trigliceridos = table.Column<string>(type: "longtext", nullable: false),
                    Uricemia = table.Column<string>(type: "longtext", nullable: false),
                    BilirrubinaDirecta = table.Column<string>(type: "longtext", nullable: false),
                    BilirrubinaIndirecta = table.Column<string>(type: "longtext", nullable: false),
                    BilirrubinaTotal = table.Column<string>(type: "longtext", nullable: false),
                    TransaminasaGlutamicoOxalac = table.Column<string>(type: "longtext", nullable: false),
                    TransaminasaGlutamicoPiruvic = table.Column<string>(type: "longtext", nullable: false),
                    FosfatasaAlcalina = table.Column<string>(type: "longtext", nullable: false),
                    TirotrofinaPlamatica = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryDetail_Study_IdStudy",
                        column: x => x.IdStudy,
                        principalSchema: "Healthcare",
                        principalTable: "Study",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityId",
                schema: "Healthcare",
                table: "Address",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Healthcare",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Healthcare",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Healthcare",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Healthcare",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Healthcare",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Healthcare",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdAddress",
                schema: "Healthcare",
                table: "AspNetUsers",
                column: "IdAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Healthcare",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_IdState",
                schema: "Healthcare",
                table: "City",
                column: "IdState");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_UserId",
                schema: "Healthcare",
                table: "Doctor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorHealthInsurance_DoctorId_HealthInsuranceId",
                schema: "Healthcare",
                table: "DoctorHealthInsurance",
                columns: new[] { "DoctorId", "HealthInsuranceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorHealthInsurance_HealthInsuranceId",
                schema: "Healthcare",
                table: "DoctorHealthInsurance",
                column: "HealthInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpeciality_DoctorId_SpecialityId",
                schema: "Healthcare",
                table: "DoctorSpeciality",
                columns: new[] { "DoctorId", "SpecialityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpeciality_SpecialityId",
                schema: "Healthcare",
                table: "DoctorSpeciality",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthPlan_HealthInsuranceId",
                schema: "Healthcare",
                table: "HealthPlan",
                column: "HealthInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryDetail_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                column: "IdStudy");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_UserId",
                schema: "Healthcare",
                table: "Patient",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientHealthPlan_HealthPlanId",
                schema: "Healthcare",
                table: "PatientHealthPlan",
                column: "HealthPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientHealthPlan_PatientId_HealthPlanId",
                schema: "Healthcare",
                table: "PatientHealthPlan",
                columns: new[] { "PatientId", "HealthPlanId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_IdCountry",
                schema: "Healthcare",
                table: "State",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Study_PatientId",
                schema: "Healthcare",
                table: "Study",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Study_StudyTypeId",
                schema: "Healthcare",
                table: "Study",
                column: "StudyTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "DoctorHealthInsurance",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "DoctorSpeciality",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "LaboratoryDetail",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "PatientHealthPlan",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Support",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Doctor",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Speciality",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Study",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "HealthPlan",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Patient",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "StudyType",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "HealthInsurance",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "City",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "State",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "Healthcare");
        }
    }
}

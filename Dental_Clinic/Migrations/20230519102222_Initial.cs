using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dental_Clinic.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diagnosis",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    diagnosisName = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnosis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    recipeCheck = table.Column<bool>(type: "boolean", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MedServices",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedServices", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    surName = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    middleName = table.Column<string>(type: "text", nullable: false),
                    birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    gender = table.Column<short>(type: "smallint", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    postName = table.Column<string>(type: "text", nullable: false),
                    salary = table.Column<int>(type: "integer", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MedTreatments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Diagnosid = table.Column<int>(type: "integer", nullable: false),
                    treatmentName = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedTreatments", x => x.id);
                    table.ForeignKey(
                        name: "FK_MedTreatments_Diagnosis_Diagnosid",
                        column: x => x.Diagnosid,
                        principalTable: "Diagnosis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    surName = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    middleName = table.Column<string>(type: "text", nullable: false),
                    Postid = table.Column<int>(type: "integer", nullable: false),
                    birthday = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Doctors_Posts_Postid",
                        column: x => x.Postid,
                        principalTable: "Posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListPrepforTreatments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MedTreatmentid = table.Column<int>(type: "integer", nullable: false),
                    Medicationid = table.Column<int>(type: "integer", nullable: false),
                    amountMedications = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListPrepforTreatments", x => x.id);
                    table.ForeignKey(
                        name: "FK_ListPrepforTreatments_MedTreatments_MedTreatmentid",
                        column: x => x.MedTreatmentid,
                        principalTable: "MedTreatments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListPrepforTreatments_Medications_Medicationid",
                        column: x => x.Medicationid,
                        principalTable: "Medications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Patientid = table.Column<int>(type: "integer", nullable: false),
                    MedTreatmentid = table.Column<int>(type: "integer", nullable: false),
                    Doctorid = table.Column<int>(type: "integer", nullable: false),
                    dateVisit = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.id);
                    table.ForeignKey(
                        name: "FK_Visits_Doctors_Doctorid",
                        column: x => x.Doctorid,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_MedTreatments_MedTreatmentid",
                        column: x => x.MedTreatmentid,
                        principalTable: "MedTreatments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visits_Patients_Patientid",
                        column: x => x.Patientid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesProvideds",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Visitid = table.Column<int>(type: "integer", nullable: false),
                    MedServiceid = table.Column<int>(type: "integer", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesProvideds", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServicesProvideds_MedServices_MedServiceid",
                        column: x => x.MedServiceid,
                        principalTable: "MedServices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesProvideds_Visits_Visitid",
                        column: x => x.Visitid,
                        principalTable: "Visits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Postid",
                table: "Doctors",
                column: "Postid");

            migrationBuilder.CreateIndex(
                name: "IX_ListPrepforTreatments_Medicationid",
                table: "ListPrepforTreatments",
                column: "Medicationid");

            migrationBuilder.CreateIndex(
                name: "IX_ListPrepforTreatments_MedTreatmentid",
                table: "ListPrepforTreatments",
                column: "MedTreatmentid");

            migrationBuilder.CreateIndex(
                name: "IX_MedTreatments_Diagnosid",
                table: "MedTreatments",
                column: "Diagnosid");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesProvideds_MedServiceid",
                table: "ServicesProvideds",
                column: "MedServiceid");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesProvideds_Visitid",
                table: "ServicesProvideds",
                column: "Visitid");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_Doctorid",
                table: "Visits",
                column: "Doctorid");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_MedTreatmentid",
                table: "Visits",
                column: "MedTreatmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_Patientid",
                table: "Visits",
                column: "Patientid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListPrepforTreatments");

            migrationBuilder.DropTable(
                name: "ServicesProvideds");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "MedServices");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "MedTreatments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Diagnosis");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CovidTestingServer.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tlkpGenders",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    gender = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tlkpGenders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tlkpSpecimen",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    type = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tlkpSpecimen", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tlkpTestMethods",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    methodname = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tlkpTestMethods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tblBiodata",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    Fullname = table.Column<string>(unicode: false, maxLength: 200, nullable: false),
                    Legal_gardian_name = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    dateofbirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    gender = table.Column<int>(nullable: false),
                    epid_no = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    home_phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    local_phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    residential_address = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    transfer_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBiodata", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblBiodata_tlkpGenders",
                        column: x => x.gender,
                        principalTable: "tlkpGenders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tlkpTestIndicators",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    method = table.Column<int>(nullable: false),
                    indicator_name = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tlkpTestIndicators", x => x.id);
                    table.UniqueConstraint("AK_tlkpTestIndicators_id_method", x => new { x.id, x.method });
                    table.ForeignKey(
                        name: "FK_tlkpTestIndicators_tlkpTestMethods",
                        column: x => x.method,
                        principalTable: "tlkpTestMethods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblLabTests",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    Biodata = table.Column<int>(nullable: false),
                    method = table.Column<int>(nullable: false),
                    interpretation = table.Column<int>(nullable: false),
                    testing_date = table.Column<DateTime>(type: "date", nullable: true),
                    testing_time = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    reporting_date = table.Column<DateTime>(type: "date", nullable: true),
                    reporting_time = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    transfer_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLabTests", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblLabTests_tblBiodata",
                        column: x => x.Biodata,
                        principalTable: "tblBiodata",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblLabTests_tlkpTestMethods",
                        column: x => x.method,
                        principalTable: "tlkpTestMethods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblLabTestsIndicatorsValues",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    labtest = table.Column<int>(nullable: false),
                    indicator = table.Column<int>(nullable: false),
                    method = table.Column<int>(nullable: false),
                    indicator_value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLabTestsIndicatorsValues", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblLabTestsIndicatorsValues_tblLabTests",
                        column: x => x.labtest,
                        principalTable: "tblLabTests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblLabTestsIndicatorsValues_tlkpTestIndicators",
                        columns: x => new { x.indicator, x.method },
                        principalTable: "tlkpTestIndicators",
                        principalColumns: new[] { "id", "method" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblLabTestsSpecimen",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    labtest = table.Column<int>(nullable: false),
                    specimen = table.Column<int>(nullable: false),
                    specimen_other = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    @checked = table.Column<bool>(name: "checked", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLabTestsSpecimen", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblLabTestsSpecimen_tblLabTests",
                        column: x => x.labtest,
                        principalTable: "tblLabTests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblLabTestsSpecimen_tlkpSpecimen",
                        column: x => x.specimen,
                        principalTable: "tlkpSpecimen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBiodata",
                table: "tblBiodata",
                column: "epid_no",
                unique: true,
                filter: "[epid_no] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblBiodata_gender",
                table: "tblBiodata",
                column: "gender");

            migrationBuilder.CreateIndex(
                name: "IX_tblLabTests_Biodata",
                table: "tblLabTests",
                column: "Biodata");

            migrationBuilder.CreateIndex(
                name: "IX_tblLabTests_method",
                table: "tblLabTests",
                column: "method");

            migrationBuilder.CreateIndex(
                name: "IX_tblLabTestsIndicatorsValues_labtest",
                table: "tblLabTestsIndicatorsValues",
                column: "labtest");

            migrationBuilder.CreateIndex(
                name: "IX_tblLabTestsIndicatorsValues_indicator_method",
                table: "tblLabTestsIndicatorsValues",
                columns: new[] { "indicator", "method" });

            migrationBuilder.CreateIndex(
                name: "IX_tblLabTestsSpecimen_specimen",
                table: "tblLabTestsSpecimen",
                column: "specimen");

            migrationBuilder.CreateIndex(
                name: "IX_tblLabTestsSpecimen",
                table: "tblLabTestsSpecimen",
                columns: new[] { "labtest", "specimen" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tlkpTestIndicators_method",
                table: "tlkpTestIndicators",
                column: "method");

            migrationBuilder.CreateIndex(
                name: "IX_tlkpTestIndicators",
                table: "tlkpTestIndicators",
                columns: new[] { "id", "method" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblLabTestsIndicatorsValues");

            migrationBuilder.DropTable(
                name: "tblLabTestsSpecimen");

            migrationBuilder.DropTable(
                name: "tlkpTestIndicators");

            migrationBuilder.DropTable(
                name: "tblLabTests");

            migrationBuilder.DropTable(
                name: "tlkpSpecimen");

            migrationBuilder.DropTable(
                name: "tblBiodata");

            migrationBuilder.DropTable(
                name: "tlkpTestMethods");

            migrationBuilder.DropTable(
                name: "tlkpGenders");
        }
    }
}

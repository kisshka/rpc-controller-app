using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpcControllerClient.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DevicesId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceName = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceStatus = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DevicesId);
                });

            migrationBuilder.CreateTable(
                name: "Pupils",
                columns: table => new
                {
                    PupilsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SurnamePupil = table.Column<string>(type: "TEXT", nullable: false),
                    NamePupil = table.Column<string>(type: "TEXT", nullable: false),
                    PatronymicPupil = table.Column<string>(type: "TEXT", nullable: false),
                    ClassNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CardValidityPeriod = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CardNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pupils", x => x.PupilsId);
                });

            migrationBuilder.CreateTable(
                name: "Visitings",
                columns: table => new
                {
                    VisitingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitingDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Direction = table.Column<bool>(type: "INTEGER", nullable: false),
                    DevicesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitings", x => x.VisitingId);
                    table.ForeignKey(
                        name: "FK_Visitings_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "DevicesId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitingPupils",
                columns: table => new
                {
                    VisitingPupilsId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PupilsId = table.Column<int>(type: "INTEGER", nullable: false),
                    VisitingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitingPupils", x => x.VisitingPupilsId);
                    table.ForeignKey(
                        name: "FK_VisitingPupils_Pupils_PupilsId",
                        column: x => x.PupilsId,
                        principalTable: "Pupils",
                        principalColumn: "PupilsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitingPupils_Visitings_VisitingId",
                        column: x => x.VisitingId,
                        principalTable: "Visitings",
                        principalColumn: "VisitingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitingPupils_PupilsId",
                table: "VisitingPupils",
                column: "PupilsId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitingPupils_VisitingId",
                table: "VisitingPupils",
                column: "VisitingId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitings_DevicesId",
                table: "Visitings",
                column: "DevicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitingPupils");

            migrationBuilder.DropTable(
                name: "Pupils");

            migrationBuilder.DropTable(
                name: "Visitings");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}

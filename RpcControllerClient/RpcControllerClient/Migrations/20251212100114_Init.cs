using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpcControllerClient.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceName = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceStatus = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "Pupils",
                columns: table => new
                {
                    PupilId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SurnamePupil = table.Column<string>(type: "TEXT", nullable: false),
                    NamePupil = table.Column<string>(type: "TEXT", nullable: false),
                    PatronymicPupil = table.Column<string>(type: "TEXT", nullable: true),
                    ClassNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CardValidityPeriod = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CardNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pupils", x => x.PupilId);
                });

            migrationBuilder.CreateTable(
                name: "Visitings",
                columns: table => new
                {
                    VisitingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VisitingDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Direction = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    PupilId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitings", x => x.VisitingId);
                    table.ForeignKey(
                        name: "FK_Visitings_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visitings_Pupils_PupilId",
                        column: x => x.PupilId,
                        principalTable: "Pupils",
                        principalColumn: "PupilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visitings_DeviceId",
                table: "Visitings",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Visitings_PupilId",
                table: "Visitings",
                column: "PupilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visitings");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Pupils");
        }
    }
}

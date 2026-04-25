using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireEarlyWarningSystem.Migrations
{
    /// <inheritdoc />
    public partial class Database1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserPhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeTown = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CameraName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Battery = table.Column<double>(type: "float", nullable: false),
                    FireSmokeImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YoloDetection = table.Column<int>(type: "int", nullable: false),
                    TemperatureValue = table.Column<double>(type: "float", nullable: false),
                    SmokeValue = table.Column<double>(type: "float", nullable: false),
                    CameraState = table.Column<int>(type: "int", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cameras_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarningHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CameraId = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    YoloDetection = table.Column<int>(type: "int", nullable: false),
                    TemperatureValue = table.Column<double>(type: "float", nullable: false),
                    SmokeValue = table.Column<double>(type: "float", nullable: false),
                    CameraState = table.Column<int>(type: "int", nullable: false),
                    WarningTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarningHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarningHistories_Cameras_CameraId",
                        column: x => x.CameraId,
                        principalTable: "Cameras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_UserId",
                table: "Cameras",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WarningHistories_CameraId",
                table: "WarningHistories",
                column: "CameraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarningHistories");

            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

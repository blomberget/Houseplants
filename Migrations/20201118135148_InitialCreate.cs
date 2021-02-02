using Microsoft.EntityFrameworkCore.Migrations;

namespace Houseplants.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Care",
                columns: table => new
                {
                    CareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WaterNeed = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    LightNeed = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    NutritionNeed = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Care", x => x.CareId);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    FamilyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamilyName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    FamilyLatin = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.FamilyId);
                });

            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    PlantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Latin = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Level = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Family = table.Column<int>(type: "int", nullable: false),
                    Care = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.PlantId);
                    table.ForeignKey(
                        name: "FK_Care",
                        column: x => x.Care,
                        principalTable: "Care",
                        principalColumn: "CareId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Family",
                        column: x => x.Family,
                        principalTable: "Family",
                        principalColumn: "FamilyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Family__3B1353D64089D866",
                table: "Family",
                column: "FamilyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Family__D64B1C7939073458",
                table: "Family",
                column: "FamilyLatin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plant_Care",
                table: "Plant",
                column: "Care");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_Family",
                table: "Plant",
                column: "Family");

            migrationBuilder.CreateIndex(
                name: "UQ__Plant__3B758BC2AD04B938",
                table: "Plant",
                column: "PlantName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Plant__E71B2F54976CE9C8",
                table: "Plant",
                column: "Latin",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "Care");

            migrationBuilder.DropTable(
                name: "Family");
        }
    }
}

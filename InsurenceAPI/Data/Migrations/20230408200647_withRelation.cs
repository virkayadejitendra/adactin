using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurenceAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class withRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatingFactors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Factor = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingFactors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_RatingId",
                table: "Occupations",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Occupations_RatingFactors_RatingId",
                table: "Occupations",
                column: "RatingId",
                principalTable: "RatingFactors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Occupations_RatingFactors_RatingId",
                table: "Occupations");

            migrationBuilder.DropTable(
                name: "RatingFactors");

            migrationBuilder.DropIndex(
                name: "IX_Occupations_RatingId",
                table: "Occupations");
        }
    }
}

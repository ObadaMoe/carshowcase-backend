using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShowcase.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameCarMediaKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarMedia_Cars_CarId",
                table: "CarMedia");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "CarMedia",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarMedia_Cars_Id",
                table: "CarMedia",
                column: "Id",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarMedia_Cars_Id",
                table: "CarMedia");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CarMedia",
                newName: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarMedia_Cars_CarId",
                table: "CarMedia",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

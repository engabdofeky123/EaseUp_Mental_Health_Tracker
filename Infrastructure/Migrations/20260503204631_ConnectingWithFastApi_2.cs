using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConnectingWithFastApi_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MentalHealthRecords_StudentId",
                table: "MentalHealthRecords",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MentalHealthRecords_Students_StudentId",
                table: "MentalHealthRecords",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MentalHealthRecords_Students_StudentId",
                table: "MentalHealthRecords");

            migrationBuilder.DropIndex(
                name: "IX_MentalHealthRecords_StudentId",
                table: "MentalHealthRecords");
        }
    }
}

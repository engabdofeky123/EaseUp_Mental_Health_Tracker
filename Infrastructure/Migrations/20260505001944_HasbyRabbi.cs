using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HasbyRabbi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_Choices_ChoiceId",
                table: "QuestionResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionResponses_Questions_QuestionId",
                table: "QuestionResponses");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResponses_ChoiceId",
                table: "QuestionResponses");

            migrationBuilder.DropIndex(
                name: "IX_QuestionResponses_QuestionId",
                table: "QuestionResponses");

            migrationBuilder.DropColumn(
                name: "SubmissionValue",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "SurveyResponses");

            migrationBuilder.DropColumn(
                name: "ChoiceId",
                table: "QuestionResponses");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "QuestionResponses");

            migrationBuilder.RenameColumn(
                name: "SurveyId",
                table: "SurveyResponses",
                newName: "PredictionValue");

            migrationBuilder.RenameColumn(
                name: "SubmisiionLabel",
                table: "SurveyResponses",
                newName: "PredictionLabel");

            migrationBuilder.AlterColumn<int>(
                name: "SurveyResponseId",
                table: "QuestionResponses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "QuestionAnswer",
                table: "QuestionResponses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionTitle",
                table: "QuestionResponses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionAnswer",
                table: "QuestionResponses");

            migrationBuilder.DropColumn(
                name: "QuestionTitle",
                table: "QuestionResponses");

            migrationBuilder.RenameColumn(
                name: "PredictionValue",
                table: "SurveyResponses",
                newName: "SurveyId");

            migrationBuilder.RenameColumn(
                name: "PredictionLabel",
                table: "SurveyResponses",
                newName: "SubmisiionLabel");

            migrationBuilder.AddColumn<double>(
                name: "SubmissionValue",
                table: "SurveyResponses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalScore",
                table: "SurveyResponses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "SurveyResponseId",
                table: "QuestionResponses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChoiceId",
                table: "QuestionResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "QuestionResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_ChoiceId",
                table: "QuestionResponses",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_QuestionId",
                table: "QuestionResponses",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_Choices_ChoiceId",
                table: "QuestionResponses",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionResponses_Questions_QuestionId",
                table: "QuestionResponses",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}

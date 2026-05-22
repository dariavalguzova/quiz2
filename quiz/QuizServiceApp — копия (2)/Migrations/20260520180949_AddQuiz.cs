using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizServiceApp.Migrations
{
    /// <inheritdoc />
    public partial class AddQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "PassingScore",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ShowCorrectAnswers",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Quizzes",
                newName: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_TeacherId",
                table: "Quizzes",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_Users_TeacherId",
                table: "Quizzes",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Users_TeacherId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_TeacherId",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Quizzes",
                newName: "StartDate");

            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "Quizzes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "Quizzes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Quizzes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PassingScore",
                table: "Quizzes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ShowCorrectAnswers",
                table: "Quizzes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lemmo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_Username",
                schema: "lemmo",
                table: "users");

            migrationBuilder.DropColumn(
                name: "TelegramConfirmed",
                schema: "lemmo",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Username",
                schema: "lemmo",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "lemmo",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "lemmo",
                table: "users",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_users_PhoneNumber",
                schema: "lemmo",
                table: "users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_TelegramId",
                schema: "lemmo",
                table: "users",
                column: "TelegramId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_PhoneNumber",
                schema: "lemmo",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_TelegramId",
                schema: "lemmo",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "lemmo",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "lemmo",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TelegramConfirmed",
                schema: "lemmo",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "lemmo",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                schema: "lemmo",
                table: "users",
                column: "Username",
                unique: true);
        }
    }
}

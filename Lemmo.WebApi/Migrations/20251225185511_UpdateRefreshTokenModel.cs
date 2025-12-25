using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lemmo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRefreshTokenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                schema: "lemmo",
                table: "RefreshTokens",
                newName: "TokenHash");

            migrationBuilder.AlterColumn<string>(
                name: "Device",
                schema: "lemmo",
                table: "RefreshTokens",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenHash",
                schema: "lemmo",
                table: "RefreshTokens",
                newName: "Token");

            migrationBuilder.AlterColumn<string>(
                name: "Device",
                schema: "lemmo",
                table: "RefreshTokens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}

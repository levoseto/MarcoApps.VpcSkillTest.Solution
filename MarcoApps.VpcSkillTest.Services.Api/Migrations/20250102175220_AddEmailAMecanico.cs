using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcoApps.VpcSkillTest.Services.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailAMecanico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Mecanicos",
                type: "TEXT",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Mecanicos",
                type: "TEXT",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Mecanicos",
                type: "TEXT",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Mecanicos");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Mecanicos");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Mecanicos");
        }
    }
}
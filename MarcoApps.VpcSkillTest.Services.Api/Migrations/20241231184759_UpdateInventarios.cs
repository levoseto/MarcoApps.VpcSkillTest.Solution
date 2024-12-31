using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcoApps.VpcSkillTest.Services.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInventarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Envios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RefaccionId",
                table: "Envios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TallerProveedorId",
                table: "Envios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TallerSolicitanteId",
                table: "Envios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Envios_RefaccionId",
                table: "Envios",
                column: "RefaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_TallerProveedorId",
                table: "Envios",
                column: "TallerProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_TallerSolicitanteId",
                table: "Envios",
                column: "TallerSolicitanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Envios_Refacciones_RefaccionId",
                table: "Envios",
                column: "RefaccionId",
                principalTable: "Refacciones",
                principalColumn: "RefaccionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Envios_Talleres_TallerProveedorId",
                table: "Envios",
                column: "TallerProveedorId",
                principalTable: "Talleres",
                principalColumn: "TallerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Envios_Talleres_TallerSolicitanteId",
                table: "Envios",
                column: "TallerSolicitanteId",
                principalTable: "Talleres",
                principalColumn: "TallerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Refacciones_RefaccionId",
                table: "Envios");

            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Talleres_TallerProveedorId",
                table: "Envios");

            migrationBuilder.DropForeignKey(
                name: "FK_Envios_Talleres_TallerSolicitanteId",
                table: "Envios");

            migrationBuilder.DropIndex(
                name: "IX_Envios_RefaccionId",
                table: "Envios");

            migrationBuilder.DropIndex(
                name: "IX_Envios_TallerProveedorId",
                table: "Envios");

            migrationBuilder.DropIndex(
                name: "IX_Envios_TallerSolicitanteId",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "RefaccionId",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "TallerProveedorId",
                table: "Envios");

            migrationBuilder.DropColumn(
                name: "TallerSolicitanteId",
                table: "Envios");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcoApps.VpcSkillTest.Services.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstalacionDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instalaciones_Mecanicos_MecanicoInstalaId",
                table: "Instalaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Instalaciones_Solicitudes_SolicitudId",
                table: "Instalaciones");

            migrationBuilder.DropIndex(
                name: "IX_Instalaciones_SolicitudId",
                table: "Instalaciones");

            migrationBuilder.RenameColumn(
                name: "MecanicoInstalaId",
                table: "Instalaciones",
                newName: "VehiculoId");

            migrationBuilder.RenameIndex(
                name: "IX_Instalaciones_MecanicoInstalaId",
                table: "Instalaciones",
                newName: "IX_Instalaciones_VehiculoId");

            migrationBuilder.AddColumn<int>(
                name: "MecanicoId",
                table: "Instalaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RefaccionId",
                table: "Instalaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TallerId",
                table: "Instalaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Instalaciones_MecanicoId",
                table: "Instalaciones",
                column: "MecanicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Instalaciones_RefaccionId",
                table: "Instalaciones",
                column: "RefaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Instalaciones_TallerId",
                table: "Instalaciones",
                column: "TallerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instalaciones_Mecanicos_MecanicoId",
                table: "Instalaciones",
                column: "MecanicoId",
                principalTable: "Mecanicos",
                principalColumn: "MecanicoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instalaciones_Refacciones_RefaccionId",
                table: "Instalaciones",
                column: "RefaccionId",
                principalTable: "Refacciones",
                principalColumn: "RefaccionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instalaciones_Talleres_TallerId",
                table: "Instalaciones",
                column: "TallerId",
                principalTable: "Talleres",
                principalColumn: "TallerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instalaciones_Vehiculos_VehiculoId",
                table: "Instalaciones",
                column: "VehiculoId",
                principalTable: "Vehiculos",
                principalColumn: "VehiculoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instalaciones_Mecanicos_MecanicoId",
                table: "Instalaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Instalaciones_Refacciones_RefaccionId",
                table: "Instalaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Instalaciones_Talleres_TallerId",
                table: "Instalaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Instalaciones_Vehiculos_VehiculoId",
                table: "Instalaciones");

            migrationBuilder.DropIndex(
                name: "IX_Instalaciones_MecanicoId",
                table: "Instalaciones");

            migrationBuilder.DropIndex(
                name: "IX_Instalaciones_RefaccionId",
                table: "Instalaciones");

            migrationBuilder.DropIndex(
                name: "IX_Instalaciones_TallerId",
                table: "Instalaciones");

            migrationBuilder.DropColumn(
                name: "MecanicoId",
                table: "Instalaciones");

            migrationBuilder.DropColumn(
                name: "RefaccionId",
                table: "Instalaciones");

            migrationBuilder.DropColumn(
                name: "TallerId",
                table: "Instalaciones");

            migrationBuilder.RenameColumn(
                name: "VehiculoId",
                table: "Instalaciones",
                newName: "MecanicoInstalaId");

            migrationBuilder.RenameIndex(
                name: "IX_Instalaciones_VehiculoId",
                table: "Instalaciones",
                newName: "IX_Instalaciones_MecanicoInstalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Instalaciones_SolicitudId",
                table: "Instalaciones",
                column: "SolicitudId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instalaciones_Mecanicos_MecanicoInstalaId",
                table: "Instalaciones",
                column: "MecanicoInstalaId",
                principalTable: "Mecanicos",
                principalColumn: "MecanicoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Instalaciones_Solicitudes_SolicitudId",
                table: "Instalaciones",
                column: "SolicitudId",
                principalTable: "Solicitudes",
                principalColumn: "SolicitudId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

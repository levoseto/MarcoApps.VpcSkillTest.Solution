using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcoApps.VpcSkillTest.Services.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Refacciones",
                columns: table => new
                {
                    RefaccionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refacciones", x => x.RefaccionId);
                });

            migrationBuilder.CreateTable(
                name: "Talleres",
                columns: table => new
                {
                    TallerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Latitud = table.Column<double>(type: "REAL", nullable: false),
                    Longitud = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Talleres", x => x.TallerId);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    VehiculoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VIN = table.Column<string>(type: "TEXT", maxLength: 17, nullable: false),
                    Marca = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Anio = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.VehiculoId);
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    InventarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TallerId = table.Column<int>(type: "INTEGER", nullable: false),
                    RefaccionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.InventarioId);
                    table.ForeignKey(
                        name: "FK_Inventarios_Refacciones_RefaccionId",
                        column: x => x.RefaccionId,
                        principalTable: "Refacciones",
                        principalColumn: "RefaccionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventarios_Talleres_TallerId",
                        column: x => x.TallerId,
                        principalTable: "Talleres",
                        principalColumn: "TallerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mecanicos",
                columns: table => new
                {
                    MecanicoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TallerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mecanicos", x => x.MecanicoId);
                    table.ForeignKey(
                        name: "FK_Mecanicos_Talleres_TallerId",
                        column: x => x.TallerId,
                        principalTable: "Talleres",
                        principalColumn: "TallerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Solicitudes",
                columns: table => new
                {
                    SolicitudId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TallerSolicitanteId = table.Column<int>(type: "INTEGER", nullable: false),
                    MecanicoSolicitanteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TallerProveedorId = table.Column<int>(type: "INTEGER", nullable: false),
                    RefaccionId = table.Column<int>(type: "INTEGER", nullable: false),
                    VehiculoId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LatitudSolicitante = table.Column<double>(type: "REAL", nullable: false),
                    LongitudSolicitante = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitudes", x => x.SolicitudId);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Mecanicos_MecanicoSolicitanteId",
                        column: x => x.MecanicoSolicitanteId,
                        principalTable: "Mecanicos",
                        principalColumn: "MecanicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Refacciones_RefaccionId",
                        column: x => x.RefaccionId,
                        principalTable: "Refacciones",
                        principalColumn: "RefaccionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Talleres_TallerProveedorId",
                        column: x => x.TallerProveedorId,
                        principalTable: "Talleres",
                        principalColumn: "TallerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Talleres_TallerSolicitanteId",
                        column: x => x.TallerSolicitanteId,
                        principalTable: "Talleres",
                        principalColumn: "TallerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Solicitudes_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Envios",
                columns: table => new
                {
                    EnvioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SolicitudId = table.Column<int>(type: "INTEGER", nullable: false),
                    MecanicoEnviaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envios", x => x.EnvioId);
                    table.ForeignKey(
                        name: "FK_Envios_Mecanicos_MecanicoEnviaId",
                        column: x => x.MecanicoEnviaId,
                        principalTable: "Mecanicos",
                        principalColumn: "MecanicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Envios_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instalaciones",
                columns: table => new
                {
                    InstalacionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SolicitudId = table.Column<int>(type: "INTEGER", nullable: false),
                    MecanicoInstalaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaInstalacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LatitudInstalacion = table.Column<double>(type: "REAL", nullable: false),
                    LongitudInstalacion = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalaciones", x => x.InstalacionId);
                    table.ForeignKey(
                        name: "FK_Instalaciones_Mecanicos_MecanicoInstalaId",
                        column: x => x.MecanicoInstalaId,
                        principalTable: "Mecanicos",
                        principalColumn: "MecanicoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Instalaciones_Solicitudes_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitudes",
                        principalColumn: "SolicitudId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Envios_MecanicoEnviaId",
                table: "Envios",
                column: "MecanicoEnviaId");

            migrationBuilder.CreateIndex(
                name: "IX_Envios_SolicitudId",
                table: "Envios",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Instalaciones_MecanicoInstalaId",
                table: "Instalaciones",
                column: "MecanicoInstalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Instalaciones_SolicitudId",
                table: "Instalaciones",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_RefaccionId",
                table: "Inventarios",
                column: "RefaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_TallerId",
                table: "Inventarios",
                column: "TallerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mecanicos_TallerId",
                table: "Mecanicos",
                column: "TallerId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_MecanicoSolicitanteId",
                table: "Solicitudes",
                column: "MecanicoSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_RefaccionId",
                table: "Solicitudes",
                column: "RefaccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_TallerProveedorId",
                table: "Solicitudes",
                column: "TallerProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_TallerSolicitanteId",
                table: "Solicitudes",
                column: "TallerSolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_VehiculoId",
                table: "Solicitudes",
                column: "VehiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Envios");

            migrationBuilder.DropTable(
                name: "Instalaciones");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Solicitudes");

            migrationBuilder.DropTable(
                name: "Mecanicos");

            migrationBuilder.DropTable(
                name: "Refacciones");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Talleres");
        }
    }
}

﻿// <auto-generated />
using System;
using MarcoApps.VpcSkillTest.Services.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MarcoApps.VpcSkillTest.Services.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Envio", b =>
                {
                    b.Property<int>("EnvioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaEnvio")
                        .HasColumnType("TEXT");

                    b.Property<int>("MecanicoEnviaId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RefaccionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SolicitudId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TallerProveedorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TallerSolicitanteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EnvioId");

                    b.HasIndex("MecanicoEnviaId");

                    b.HasIndex("RefaccionId");

                    b.HasIndex("SolicitudId");

                    b.HasIndex("TallerProveedorId");

                    b.HasIndex("TallerSolicitanteId");

                    b.ToTable("Envios");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Instalacion", b =>
                {
                    b.Property<int>("InstalacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaInstalacion")
                        .HasColumnType("TEXT");

                    b.Property<double>("LatitudInstalacion")
                        .HasColumnType("REAL");

                    b.Property<double>("LongitudInstalacion")
                        .HasColumnType("REAL");

                    b.Property<int>("MecanicoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RefaccionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SolicitudId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TallerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InstalacionId");

                    b.HasIndex("MecanicoId");

                    b.HasIndex("RefaccionId");

                    b.HasIndex("TallerId");

                    b.HasIndex("VehiculoId");

                    b.ToTable("Instalaciones");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Inventario", b =>
                {
                    b.Property<int>("InventarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cantidad")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RefaccionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TallerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InventarioId");

                    b.HasIndex("RefaccionId");

                    b.HasIndex("TallerId");

                    b.ToTable("Inventarios");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Mecanico", b =>
                {
                    b.Property<int>("MecanicoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TallerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MecanicoId");

                    b.HasIndex("TallerId");

                    b.ToTable("Mecanicos");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Refaccion", b =>
                {
                    b.Property<int>("RefaccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("RefaccionId");

                    b.ToTable("Refacciones");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Solicitud", b =>
                {
                    b.Property<int>("SolicitudId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaSolicitud")
                        .HasColumnType("TEXT");

                    b.Property<double>("LatitudSolicitante")
                        .HasColumnType("REAL");

                    b.Property<double>("LongitudSolicitante")
                        .HasColumnType("REAL");

                    b.Property<int>("MecanicoSolicitanteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RefaccionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TallerProveedorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TallerSolicitanteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("SolicitudId");

                    b.HasIndex("MecanicoSolicitanteId");

                    b.HasIndex("RefaccionId");

                    b.HasIndex("TallerProveedorId");

                    b.HasIndex("TallerSolicitanteId");

                    b.HasIndex("VehiculoId");

                    b.ToTable("Solicitudes");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Taller", b =>
                {
                    b.Property<int>("TallerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<double>("Latitud")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitud")
                        .HasColumnType("REAL");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("TallerId");

                    b.ToTable("Talleres");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Vehiculo", b =>
                {
                    b.Property<int>("VehiculoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Anio")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("TEXT");

                    b.HasKey("VehiculoId");

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Envio", b =>
                {
                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Mecanico", "MecanicoEnvia")
                        .WithMany()
                        .HasForeignKey("MecanicoEnviaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Refaccion", "Refaccion")
                        .WithMany()
                        .HasForeignKey("RefaccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Solicitud", "Solicitud")
                        .WithMany()
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Taller", "TallerProveedor")
                        .WithMany()
                        .HasForeignKey("TallerProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Taller", "TallerSolicitante")
                        .WithMany()
                        .HasForeignKey("TallerSolicitanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MecanicoEnvia");

                    b.Navigation("Refaccion");

                    b.Navigation("Solicitud");

                    b.Navigation("TallerProveedor");

                    b.Navigation("TallerSolicitante");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Instalacion", b =>
                {
                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Mecanico", "Mecanico")
                        .WithMany()
                        .HasForeignKey("MecanicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Refaccion", "Refaccion")
                        .WithMany()
                        .HasForeignKey("RefaccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Taller", "Taller")
                        .WithMany()
                        .HasForeignKey("TallerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Vehiculo", "Vehiculo")
                        .WithMany()
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mecanico");

                    b.Navigation("Refaccion");

                    b.Navigation("Taller");

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Inventario", b =>
                {
                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Refaccion", "Refaccion")
                        .WithMany()
                        .HasForeignKey("RefaccionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Taller", "Taller")
                        .WithMany()
                        .HasForeignKey("TallerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Refaccion");

                    b.Navigation("Taller");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Mecanico", b =>
                {
                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Taller", "Taller")
                        .WithMany()
                        .HasForeignKey("TallerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Taller");
                });

            modelBuilder.Entity("MarcoApps.VpcSkillTest.Services.Api.Models.Solicitud", b =>
                {
                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Mecanico", "MecanicoSolicitante")
                        .WithMany()
                        .HasForeignKey("MecanicoSolicitanteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Refaccion", "Refaccion")
                        .WithMany()
                        .HasForeignKey("RefaccionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Taller", "TallerProveedor")
                        .WithMany()
                        .HasForeignKey("TallerProveedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Taller", "TallerSolicitante")
                        .WithMany()
                        .HasForeignKey("TallerSolicitanteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MarcoApps.VpcSkillTest.Services.Api.Models.Vehiculo", "Vehiculo")
                        .WithMany()
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MecanicoSolicitante");

                    b.Navigation("Refaccion");

                    b.Navigation("TallerProveedor");

                    b.Navigation("TallerSolicitante");

                    b.Navigation("Vehiculo");
                });
#pragma warning restore 612, 618
        }
    }
}

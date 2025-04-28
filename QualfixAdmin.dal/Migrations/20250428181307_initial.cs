using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QualfixAdmin.dal.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    CiudadID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(90)", unicode: false, maxLength: 90, nullable: true),
                    EstadoID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.CiudadID);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionCFDI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nameFileCer = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    fileCer = table.Column<string>(type: "text", nullable: true),
                    fileKey = table.Column<string>(type: "text", nullable: true),
                    filePasswordKey = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    nameFileKey = table.Column<string>(type: "character varying(200)", unicode: false, maxLength: 200, nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Cuenta = table.Column<string>(type: "text", nullable: true),
                    PasswordUsuario = table.Column<string>(type: "text", nullable: true),
                    Usuario = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    EstadoID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(90)", unicode: false, maxLength: 90, nullable: true),
                    PaisID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoId", x => x.EstadoID);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    PaisId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", unicode: false, nullable: true),
                    TipoMoneda = table.Column<string>(type: "text", unicode: false, nullable: true),
                    SimboloMoneda = table.Column<string>(type: "text", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Descripcion = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegimenFiscal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodeRegimenFiscal = table.Column<int>(type: "integer", nullable: true),
                    Descripcion = table.Column<string>(type: "character varying(250)", unicode: false, maxLength: 250, nullable: true),
                    Fisica = table.Column<bool>(type: "boolean", nullable: true),
                    Moral = table.Column<bool>(type: "boolean", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegimenFiscal_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    Numero_Celular = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Fecha_Solicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fecha_Recibido = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporte", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeLicencia",
                columns: table => new
                {
                    TipoLicenciaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Tiempo = table.Column<int>(type: "integer", nullable: false),
                    Costo = table.Column<double>(type: "double precision", nullable: false),
                    Llave = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeLicencia", x => x.TipoLicenciaId);
                });

            migrationBuilder.CreateTable(
                name: "InformacionFIscal",
                columns: table => new
                {
                    InformacionFiscalID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RazonSocial = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    RFC = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: true),
                    Direccion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CP = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CiudadID = table.Column<int>(type: "integer", nullable: true),
                    EstadoID = table.Column<int>(type: "integer", nullable: true),
                    PaisID = table.Column<int>(type: "integer", nullable: true),
                    NumeroInterior = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    NumeroExterior = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Colonia = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Localidad = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Referencia = table.Column<string>(type: "text", nullable: true),
                    Municipio = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegFiscalID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegimenFIscal", x => x.InformacionFiscalID);
                    table.ForeignKey(
                        name: "FK_InformacionFIscal_Ciudad",
                        column: x => x.CiudadID,
                        principalTable: "Ciudad",
                        principalColumn: "CiudadID");
                    table.ForeignKey(
                        name: "FK_InformacionFIscal_Estado",
                        column: x => x.EstadoID,
                        principalTable: "Estado",
                        principalColumn: "EstadoID");
                    table.ForeignKey(
                        name: "FK_InformacionFIscal_Pais",
                        column: x => x.PaisID,
                        principalTable: "Pais",
                        principalColumn: "PaisId");
                    table.ForeignKey(
                        name: "FK_InformacionFIscal_RegimenFiscal",
                        column: x => x.RegFiscalID,
                        principalTable: "RegimenFiscal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ruta = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Tamaño = table.Column<long>(type: "bigint", nullable: true),
                    ReporteId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagen_Reporte_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reporte",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ClienteID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Direccion = table.Column<string>(type: "text", unicode: false, nullable: true),
                    NombreComercial = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    CP = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Telefonos = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    Fax = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    InformacionFiscalID = table.Column<int>(type: "integer", nullable: true),
                    CiudadID = table.Column<int>(type: "integer", nullable: true),
                    EstadoID = table.Column<int>(type: "integer", nullable: true),
                    PaisID = table.Column<int>(type: "integer", nullable: true),
                    Calle = table.Column<string>(type: "text", nullable: true),
                    NumeroExterior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NumeroInterior = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Cruzamientos = table.Column<string>(type: "text", nullable: true),
                    Colonia = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ClienteID);
                    table.ForeignKey(
                        name: "FK_Cliente_Ciudad",
                        column: x => x.CiudadID,
                        principalTable: "Ciudad",
                        principalColumn: "CiudadID");
                    table.ForeignKey(
                        name: "FK_Cliente_Estado",
                        column: x => x.EstadoID,
                        principalTable: "Estado",
                        principalColumn: "EstadoID");
                    table.ForeignKey(
                        name: "FK_Cliente_InformacionFIscal",
                        column: x => x.InformacionFiscalID,
                        principalTable: "InformacionFIscal",
                        principalColumn: "InformacionFiscalID");
                    table.ForeignKey(
                        name: "FK_Cliente_Pais",
                        column: x => x.PaisID,
                        principalTable: "Pais",
                        principalColumn: "PaisId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CiudadID",
                table: "Cliente",
                column: "CiudadID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_EstadoID",
                table: "Cliente",
                column: "EstadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_InformacionFiscalID",
                table: "Cliente",
                column: "InformacionFiscalID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_PaisID",
                table: "Cliente",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_ReporteId",
                table: "Imagen",
                column: "ReporteId");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFIscal_CiudadID",
                table: "InformacionFIscal",
                column: "CiudadID");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFIscal_EstadoID",
                table: "InformacionFIscal",
                column: "EstadoID");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFIscal_PaisID",
                table: "InformacionFIscal",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_InformacionFIscal_RegFiscalID",
                table: "InformacionFIscal",
                column: "RegFiscalID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "ConfiguracionCFDI");

            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "TipoDeLicencia");

            migrationBuilder.DropTable(
                name: "InformacionFIscal");

            migrationBuilder.DropTable(
                name: "Reporte");

            migrationBuilder.DropTable(
                name: "Ciudad");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "RegimenFiscal");
        }
    }
}

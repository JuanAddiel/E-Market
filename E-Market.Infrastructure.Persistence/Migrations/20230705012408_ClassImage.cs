using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Market.Infrastructure.Persistence.Migrations
{
    public partial class ClassImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Anuncio");

            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idAnuncio = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagen_Anuncio_idAnuncio",
                        column: x => x.idAnuncio,
                        principalTable: "Anuncio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagen_idAnuncio",
                table: "Imagen",
                column: "idAnuncio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Anuncio",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

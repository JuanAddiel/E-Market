using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Market.Infrastructure.Persistence.Migrations
{
    public partial class AddTableIMG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Imagen",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Imagen");
        }
    }
}

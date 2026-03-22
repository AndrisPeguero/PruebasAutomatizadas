using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaBook.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadPersona",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Mesa",
                table: "Reservas");

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdRestaurante",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestauranteId",
                table: "Menus",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_RestauranteId",
                table: "Menus",
                column: "RestauranteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Restaurantes_RestauranteId",
                table: "Menus",
                column: "RestauranteId",
                principalTable: "Restaurantes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Restaurantes_RestauranteId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_RestauranteId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "IdRestaurante",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "RestauranteId",
                table: "Menus");

            migrationBuilder.AddColumn<int>(
                name: "CantidadPersona",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Mesa",
                table: "Reservas",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp3.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Rooms",
                newName: "RoomType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "Rooms",
                newName: "Type");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addedRentItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CarLists_RentRefId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_RentRefId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "RentRefId",
                table: "Bookings");

            migrationBuilder.CreateTable(
                name: "RentItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarListId = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentItems_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentItems_CarLists_CarListId",
                        column: x => x.CarListId,
                        principalTable: "CarLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentItems_BookingId",
                table: "RentItems",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_RentItems_CarListId",
                table: "RentItems",
                column: "CarListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentItems");

            migrationBuilder.AddColumn<int>(
                name: "RentRefId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RentRefId",
                table: "Bookings",
                column: "RentRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CarLists_RentRefId",
                table: "Bookings",
                column: "RentRefId",
                principalTable: "CarLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

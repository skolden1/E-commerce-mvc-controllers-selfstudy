using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTRLE_Handel_oev_identity.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddednewtblOrderInfoView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderInfoViewModelId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderInfoViewModels",
                columns: table => new
                {
                    OrderInfoViewModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInfoViewModels", x => x.OrderInfoViewModelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_OrderInfoViewModelId",
                table: "Cart",
                column: "OrderInfoViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_OrderInfoViewModels_OrderInfoViewModelId",
                table: "Cart",
                column: "OrderInfoViewModelId",
                principalTable: "OrderInfoViewModels",
                principalColumn: "OrderInfoViewModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_OrderInfoViewModels_OrderInfoViewModelId",
                table: "Cart");

            migrationBuilder.DropTable(
                name: "OrderInfoViewModels");

            migrationBuilder.DropIndex(
                name: "IX_Cart_OrderInfoViewModelId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "OrderInfoViewModelId",
                table: "Cart");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CTRLE_Handel_oev_identity.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class updtCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Cart");
        }
    }
}

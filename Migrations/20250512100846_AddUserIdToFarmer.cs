using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAPD7111_Agri_Energy_Connect.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToFarmer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Farmers");
        }
    }
}

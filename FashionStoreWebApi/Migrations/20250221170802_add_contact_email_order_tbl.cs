using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class add_contact_email_order_tbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Orders");
        }
    }
}

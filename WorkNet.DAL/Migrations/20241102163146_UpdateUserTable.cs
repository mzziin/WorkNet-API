using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkNet.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "Username",
            table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "Username",
            table: "Users",
            type: "varchar(100)",
            unicode: false,
            maxLength: 100,
            nullable: false);
        }
    }
}

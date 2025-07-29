using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Data.Migrations
{
    /// <inheritdoc />
    public partial class rolechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_fk_role_id",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "master_role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_is_editable = table.Column<bool>(type: "bit", nullable: false),
                    role_is_super = table.Column<bool>(type: "bit", nullable: false),
                    role_permission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_created_by = table.Column<int>(type: "int", nullable: false),
                    role_created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    role_updated_by = table.Column<int>(type: "int", nullable: false),
                    role_updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_master_role", x => x.role_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "master_role");

            migrationBuilder.DropColumn(
                name: "user_fk_role_id",
                table: "AspNetUsers");
        }
    }
}

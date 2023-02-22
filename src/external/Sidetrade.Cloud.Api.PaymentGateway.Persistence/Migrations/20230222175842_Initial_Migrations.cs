using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vendor_account",
                columns: table => new
                {
                    member_id = table.Column<int>(type: "integer", nullable: false),
                    meta_member_id = table.Column<int>(type: "integer", nullable: false),
                    api_secret_key = table.Column<string>(type: "text", nullable: false),
                    api_public_key = table.Column<string>(type: "text", nullable: false),
                    is_activated = table.Column<bool>(type: "boolean", nullable: false),
                    date_created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    date_updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendor_account", x => x.member_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vendor_account");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sidetrade.Cloud.Api.PaymentGateway.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vendor_account",
                columns: table => new
                {
                    memberid = table.Column<int>(name: "member_id", type: "integer", nullable: false),
                    metamemberid = table.Column<int>(name: "meta_member_id", type: "integer", nullable: false),
                    apisecretkey = table.Column<string>(name: "api_secret_key", type: "text", nullable: false),
                    apipublickey = table.Column<string>(name: "api_public_key", type: "text", nullable: false),
                    isactivated = table.Column<bool>(name: "is_activated", type: "boolean", nullable: false),
                    datecreated = table.Column<DateTimeOffset>(name: "date_created", type: "timestamp with time zone", nullable: false),
                    dateupdated = table.Column<DateTimeOffset>(name: "date_updated", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendor_account", x => x.memberid);
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

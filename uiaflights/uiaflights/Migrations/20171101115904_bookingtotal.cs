using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace uiaflights.Migrations
{
    public partial class bookingtotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Bookings",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Bookings");
        }
    }
}

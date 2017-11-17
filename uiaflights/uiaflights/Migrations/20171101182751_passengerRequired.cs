using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace uiaflights.Migrations
{
    public partial class passengerRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PassportNo",
                table: "Passengers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Passengers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PassportNo",
                table: "Passengers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Passengers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

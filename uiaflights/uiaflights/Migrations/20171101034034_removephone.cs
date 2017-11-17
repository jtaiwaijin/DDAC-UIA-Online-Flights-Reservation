using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace uiaflights.Migrations
{
    public partial class removephone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_PassengerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_PassengerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "Tickets");

            migrationBuilder.AddColumn<int>(
                name: "PassengerId",
                table: "Tickets",
                nullable: false
                );

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PassengerId",
                table: "Tickets",
                column: "PassengerId");

            //migrationBuilder.AlterColumn<int>(
            //    name: "PassengerId",
            //    table: "Tickets",
            //    nullable: false,
            //    oldClrType: typeof(Guid));

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DOB = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(maxLength: 1, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Nationality = table.Column<string>(nullable: true),
                    PassportNo = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Passengers_PassengerId",
                table: "Tickets",
                column: "PassengerId",
                principalTable: "Passengers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Passengers_PassengerId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.AlterColumn<Guid>(
                name: "PassengerId",
                table: "Tickets",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_PassengerId",
                table: "Tickets",
                column: "PassengerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

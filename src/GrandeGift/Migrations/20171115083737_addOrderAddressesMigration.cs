using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGift.Migrations
{
    public partial class addOrderAddressesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "TblAddress",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblAddress_OrderId",
                table: "TblAddress",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblAddress_TblOrder_OrderId",
                table: "TblAddress",
                column: "OrderId",
                principalTable: "TblOrder",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblAddress_TblOrder_OrderId",
                table: "TblAddress");

            migrationBuilder.DropIndex(
                name: "IX_TblAddress_OrderId",
                table: "TblAddress");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "TblAddress");
        }
    }
}

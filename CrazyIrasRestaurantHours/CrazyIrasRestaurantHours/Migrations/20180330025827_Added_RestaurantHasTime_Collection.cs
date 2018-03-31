using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrazyIrasRestaurantHours.Migrations
{
    public partial class Added_RestaurantHasTime_Collection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RestaurantHasTime_RestaurantID",
                table: "RestaurantHasTime",
                column: "RestaurantID");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantHasTime_Restaurant_RestaurantID",
                table: "RestaurantHasTime",
                column: "RestaurantID",
                principalTable: "Restaurant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantHasTime_Restaurant_RestaurantID",
                table: "RestaurantHasTime");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantHasTime_RestaurantID",
                table: "RestaurantHasTime");
        }
    }
}

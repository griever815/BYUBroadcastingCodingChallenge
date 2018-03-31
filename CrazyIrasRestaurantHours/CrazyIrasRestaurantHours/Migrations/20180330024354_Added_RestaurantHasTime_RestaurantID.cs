using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrazyIrasRestaurantHours.Migrations
{
    public partial class Added_RestaurantHasTime_RestaurantID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantID",
                table: "RestaurantHasTime",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RestaurantID",
                table: "RestaurantHasTime");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrazyIrasRestaurantHours.Migrations
{
    public partial class Added_String_to_RestaurantHasTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DayOfWeek",
                table: "RestaurantHasTime",
                newName: "DayOfWeekInt");

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeekString",
                table: "RestaurantHasTime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeekString",
                table: "RestaurantHasTime");

            migrationBuilder.RenameColumn(
                name: "DayOfWeekInt",
                table: "RestaurantHasTime",
                newName: "DayOfWeek");
        }
    }
}

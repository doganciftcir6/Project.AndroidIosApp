using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.AndroidIosApp.DataAccess.Migrations
{
    public partial class AddingDeviceTableWithCreateDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Devices",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Devices");
        }
    }
}

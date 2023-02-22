using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.AndroidIosApp.DataAccess.Migrations
{
    public partial class AddOSAndDeviceTypeTableHasData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "Definition", "Status" },
                values: new object[,]
                {
                    { 1, "Phone", true },
                    { 2, "Tablet", true }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Definition", "Status" },
                values: new object[,]
                {
                    { 1, "Erkek", true },
                    { 2, "Kadın", true },
                    { 3, "Belirtmek İstemiyorum", true }
                });

            migrationBuilder.InsertData(
                table: "GetOS",
                columns: new[] { "Id", "Definition", "Status" },
                values: new object[,]
                {
                    { 1, "Ios", true },
                    { 2, "Android", true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeviceTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GetOS",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GetOS",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.AndroidIosApp.DataAccess.Migrations
{
    public partial class EditSupportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Supports",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Supports",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "Supports",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Supports",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Supports",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Supports");


            migrationBuilder.RenameIndex(
                name: "IX_Supports_ProjectUserId",
                table: "Supports",
                newName: "IX_Supports_ProjectUserSenderId");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Supports",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);
        }
    }
}

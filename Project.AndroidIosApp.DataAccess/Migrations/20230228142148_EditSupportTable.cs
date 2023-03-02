using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.AndroidIosApp.DataAccess.Migrations
{
    public partial class EditSupportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supports_ProjectUsers_ProjectUserSenderId",
                table: "Supports");

            migrationBuilder.DropColumn(
                name: "ProjectUserReceiverId",
                table: "Supports");

            migrationBuilder.RenameColumn(
                name: "ProjectUserSenderId",
                table: "Supports",
                newName: "ProjectUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Supports_ProjectUserSenderId",
                table: "Supports",
                newName: "IX_Supports_ProjectUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Supports_ProjectUsers_ProjectUserId",
                table: "Supports",
                column: "ProjectUserId",
                principalTable: "ProjectUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supports_ProjectUsers_ProjectUserId",
                table: "Supports");

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

            migrationBuilder.RenameColumn(
                name: "ProjectUserId",
                table: "Supports",
                newName: "ProjectUserSenderId");

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

            migrationBuilder.AddColumn<int>(
                name: "ProjectUserReceiverId",
                table: "Supports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Supports_ProjectUsers_ProjectUserSenderId",
                table: "Supports",
                column: "ProjectUserSenderId",
                principalTable: "ProjectUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

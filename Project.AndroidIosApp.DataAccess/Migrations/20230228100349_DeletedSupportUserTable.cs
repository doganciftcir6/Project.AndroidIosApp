using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.AndroidIosApp.DataAccess.Migrations
{
    public partial class DeletedSupportUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupportUserSupports");

            migrationBuilder.DropTable(
                name: "SupportUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupportUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    SupportEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SupportImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportLastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupportName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SupportPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportUserSupports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    SupportId = table.Column<int>(type: "int", nullable: false),
                    SupportUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportUserSupports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportUserSupports_Supports_SupportId",
                        column: x => x.SupportId,
                        principalTable: "Supports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportUserSupports_SupportUsers_SupportUserId",
                        column: x => x.SupportUserId,
                        principalTable: "SupportUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupportUserSupports_SupportId_SupportUserId",
                table: "SupportUserSupports",
                columns: new[] { "SupportId", "SupportUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportUserSupports_SupportUserId",
                table: "SupportUserSupports",
                column: "SupportUserId");
        }
    }
}

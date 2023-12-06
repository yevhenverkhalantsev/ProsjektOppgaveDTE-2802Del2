using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProsjektOppgaveWebAPI.Migrations
{
    public partial class backToPrevious : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Blog_BlogId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_BlogId",
                table: "Tag");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d84d438-e8b9-4dca-8767-7d2d1ef842ad");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Tag");

            migrationBuilder.CreateTable(
                name: "BlogTagRelations",
                columns: table => new
                {
                    BlogId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTagRelations", x => new { x.BlogId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BlogTagRelations_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "BlogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTagRelations_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "faf5ef61-4e84-4353-807b-9dea9d5570b5", 0, "9e9133dc-3a76-4abc-9708-2695481200de", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEBLFI9//R3wYkJgnxOQIr6QlGeBmOjoHtWFSxou/e+BDQxvXtLqe44UhOeKegxxpEg==", null, false, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagRelations_TagId",
                table: "BlogTagRelations",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTagRelations");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "faf5ef61-4e84-4353-807b-9dea9d5570b5");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Tag",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4d84d438-e8b9-4dca-8767-7d2d1ef842ad", 0, "dd589d43-9d2c-4dd6-9e1e-fb6cd6221021", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEHlpG00SBqc/ZB6nFG3FeQCI7TnmQVFYWoDysw0jGWM8u0BsfKtOh0IeLXN4SeBk8w==", null, false, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_BlogId",
                table: "Tag",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Blog_BlogId",
                table: "Tag",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "BlogId");
        }
    }
}

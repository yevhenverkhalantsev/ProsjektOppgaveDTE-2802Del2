using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProsjektOppgaveWebAPI.EntityFramework.Migrations
{
    public partial class changedPostTagsRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTagRelations");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d0d1cfa-b06d-443f-8ed5-1548c1a34e83");

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    PostFk = table.Column<int>(type: "INTEGER", nullable: false),
                    TagFk = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => new { x.PostFk, x.TagFk });
                    table.ForeignKey(
                        name: "FK_PostTags_Post_PostFk",
                        column: x => x.PostFk,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tag_TagFk",
                        column: x => x.TagFk,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b3115660-d27c-4f8b-ab7d-8186b1ec3cb1", 0, "4f14936a-ce87-4bdb-9248-f8076a909370", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEO+Ku6G+WKmGMfwRLXVWFXtoXwa8AjdCWIpSoWsUHmdA5M8beWSMNSY8Y1utz+bkOw==", null, false, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagFk",
                table: "PostTags",
                column: "TagFk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3115660-d27c-4f8b-ab7d-8186b1ec3cb1");

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
                values: new object[] { "3d0d1cfa-b06d-443f-8ed5-1548c1a34e83", 0, "3f6fd1c0-cf19-4e06-a316-ed8be24474a2", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEBgcVjtcHZiaNwohMu7lmPFWtO2h+GLb9psPVM+bKq38DBO3hNmTsW8vhl5y9opdRQ==", null, false, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagRelations_TagId",
                table: "BlogTagRelations",
                column: "TagId");
        }
    }
}

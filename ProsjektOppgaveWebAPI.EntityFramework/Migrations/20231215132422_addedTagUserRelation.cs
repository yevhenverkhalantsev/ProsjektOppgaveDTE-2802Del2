using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProsjektOppgaveWebAPI.EntityFramework.Migrations
{
    public partial class addedTagUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b3115660-d27c-4f8b-ab7d-8186b1ec3cb1");

            migrationBuilder.AddColumn<Guid>(
                name: "UserFk",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tag",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e22406fb-2d08-4abd-8af9-38d2bb71e43c", 0, "e425276d-c491-4695-9879-40f630d748ac", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEM1YnUK9/zGDPoo5oT6PE0oL/RLWy88ThSMD+n4KhMsbQmbM4PuBz/HD+5Wh0L5/dw==", null, false, "", false, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UserId",
                table: "Tag",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_AspNetUsers_UserId",
                table: "Tag",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_AspNetUsers_UserId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Tag_UserId",
                table: "Tag");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e22406fb-2d08-4abd-8af9-38d2bb71e43c");

            migrationBuilder.DropColumn(
                name: "UserFk",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tag");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b3115660-d27c-4f8b-ab7d-8186b1ec3cb1", 0, "4f14936a-ce87-4bdb-9248-f8076a909370", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEO+Ku6G+WKmGMfwRLXVWFXtoXwa8AjdCWIpSoWsUHmdA5M8beWSMNSY8Y1utz+bkOw==", null, false, "", false, "admin" });
        }
    }
}

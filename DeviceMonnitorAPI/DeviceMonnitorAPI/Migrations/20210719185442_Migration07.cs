using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceMonnitorAPI.Migrations
{
    public partial class Migration07 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("a3f3d2d0-5fe6-4c64-8084-50dc34888741"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("cb36cbe9-5ac3-46a6-9327-09bfbe741f95"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ParamName",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("ec3349a8-0bc7-4ed1-bb36-ba6f6597cc0c"), new DateTime(2021, 7, 19, 23, 54, 41, 909, DateTimeKind.Local).AddTicks(2650), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("07d307c4-93b3-47d2-90a5-78603ade05d6"), new DateTime(2021, 7, 19, 23, 54, 41, 909, DateTimeKind.Local).AddTicks(3034), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("07d307c4-93b3-47d2-90a5-78603ade05d6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("ec3349a8-0bc7-4ed1-bb36-ba6f6597cc0c"));

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ParamName");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("cb36cbe9-5ac3-46a6-9327-09bfbe741f95"), new DateTime(2021, 7, 17, 22, 35, 13, 623, DateTimeKind.Local).AddTicks(2117), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("a3f3d2d0-5fe6-4c64-8084-50dc34888741"), new DateTime(2021, 7, 17, 22, 35, 13, 623, DateTimeKind.Local).AddTicks(2519), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });
        }
    }
}

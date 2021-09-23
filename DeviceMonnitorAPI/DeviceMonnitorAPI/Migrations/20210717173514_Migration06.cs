using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceMonnitorAPI.Migrations
{
    public partial class Migration06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceParamName_ParamNames_ParamNamesId",
                table: "DeviceParamName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamNames",
                table: "ParamNames");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("0ffd55c9-271b-46fe-99fc-46a3ea8ecec9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("3a570eb7-0918-4712-9e5a-6d21e70759f0"));

            migrationBuilder.RenameTable(
                name: "ParamNames",
                newName: "ParamName");

            migrationBuilder.RenameIndex(
                name: "IX_ParamNames_Id",
                table: "ParamName",
                newName: "IX_ParamName_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamName",
                table: "ParamName",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("cb36cbe9-5ac3-46a6-9327-09bfbe741f95"), new DateTime(2021, 7, 17, 22, 35, 13, 623, DateTimeKind.Local).AddTicks(2117), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("a3f3d2d0-5fe6-4c64-8084-50dc34888741"), new DateTime(2021, 7, 17, 22, 35, 13, 623, DateTimeKind.Local).AddTicks(2519), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceParamName_ParamName_ParamNamesId",
                table: "DeviceParamName",
                column: "ParamNamesId",
                principalTable: "ParamName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceParamName_ParamName_ParamNamesId",
                table: "DeviceParamName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParamName",
                table: "ParamName");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("a3f3d2d0-5fe6-4c64-8084-50dc34888741"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("cb36cbe9-5ac3-46a6-9327-09bfbe741f95"));

            migrationBuilder.RenameTable(
                name: "ParamName",
                newName: "ParamNames");

            migrationBuilder.RenameIndex(
                name: "IX_ParamName_Id",
                table: "ParamNames",
                newName: "IX_ParamNames_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParamNames",
                table: "ParamNames",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("3a570eb7-0918-4712-9e5a-6d21e70759f0"), new DateTime(2021, 7, 17, 22, 33, 20, 798, DateTimeKind.Local).AddTicks(1969), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("0ffd55c9-271b-46fe-99fc-46a3ea8ecec9"), new DateTime(2021, 7, 17, 22, 33, 20, 798, DateTimeKind.Local).AddTicks(2354), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceParamName_ParamNames_ParamNamesId",
                table: "DeviceParamName",
                column: "ParamNamesId",
                principalTable: "ParamNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

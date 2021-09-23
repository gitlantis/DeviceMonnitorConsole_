using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceMonnitorAPI.Migrations
{
    public partial class Migration05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceParamNames");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("8554ee2b-e60d-40a5-9751-80dc26a9a5dd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("8b89bb24-0648-4c51-9ae7-76a59ad5e39b"));

            migrationBuilder.RenameColumn(
                name: "Names",
                table: "ParamNames",
                newName: "NameDomain");

            migrationBuilder.RenameColumn(
                name: "NameIndexes",
                table: "ParamNames",
                newName: "NameIndex");

            migrationBuilder.RenameColumn(
                name: "NameDomains",
                table: "ParamNames",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "DeviceParamName",
                columns: table => new
                {
                    DevicesDeviceGuid = table.Column<Guid>(type: "char(36)", nullable: false),
                    ParamNamesId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceParamName", x => new { x.DevicesDeviceGuid, x.ParamNamesId });
                    table.ForeignKey(
                        name: "FK_DeviceParamName_Device_DevicesDeviceGuid",
                        column: x => x.DevicesDeviceGuid,
                        principalTable: "Device",
                        principalColumn: "DeviceGuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceParamName_ParamNames_ParamNamesId",
                        column: x => x.ParamNamesId,
                        principalTable: "ParamNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("3a570eb7-0918-4712-9e5a-6d21e70759f0"), new DateTime(2021, 7, 17, 22, 33, 20, 798, DateTimeKind.Local).AddTicks(1969), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("0ffd55c9-271b-46fe-99fc-46a3ea8ecec9"), new DateTime(2021, 7, 17, 22, 33, 20, 798, DateTimeKind.Local).AddTicks(2354), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceParamName_ParamNamesId",
                table: "DeviceParamName",
                column: "ParamNamesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceParamName");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("0ffd55c9-271b-46fe-99fc-46a3ea8ecec9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("3a570eb7-0918-4712-9e5a-6d21e70759f0"));

            migrationBuilder.RenameColumn(
                name: "NameIndex",
                table: "ParamNames",
                newName: "NameIndexes");

            migrationBuilder.RenameColumn(
                name: "NameDomain",
                table: "ParamNames",
                newName: "Names");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ParamNames",
                newName: "NameDomains");

            migrationBuilder.CreateTable(
                name: "DeviceParamNames",
                columns: table => new
                {
                    DevicesDeviceGuid = table.Column<Guid>(type: "char(36)", nullable: false),
                    ParamNamesId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceParamNames", x => new { x.DevicesDeviceGuid, x.ParamNamesId });
                    table.ForeignKey(
                        name: "FK_DeviceParamNames_Device_DevicesDeviceGuid",
                        column: x => x.DevicesDeviceGuid,
                        principalTable: "Device",
                        principalColumn: "DeviceGuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceParamNames_ParamNames_ParamNamesId",
                        column: x => x.ParamNamesId,
                        principalTable: "ParamNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("8b89bb24-0648-4c51-9ae7-76a59ad5e39b"), new DateTime(2021, 7, 17, 22, 18, 43, 35, DateTimeKind.Local).AddTicks(4957), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("8554ee2b-e60d-40a5-9751-80dc26a9a5dd"), new DateTime(2021, 7, 17, 22, 18, 43, 35, DateTimeKind.Local).AddTicks(5363), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceParamNames_ParamNamesId",
                table: "DeviceParamNames",
                column: "ParamNamesId");
        }
    }
}

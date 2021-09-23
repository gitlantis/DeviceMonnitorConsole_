using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceMonnitorAPI.Migrations
{
    public partial class Migration04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("467e2276-1f12-44d5-8ce2-a43dba846802"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("7ba64c1d-823e-4841-8287-ed440f8ab217"));

            migrationBuilder.CreateTable(
                name: "ParamNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DeviceGuid = table.Column<Guid>(type: "char(36)", nullable: false),
                    Names = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    NameDomains = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    NameIndexes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamNames", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ParamNames_Id",
                table: "ParamNames",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceParamNames");

            migrationBuilder.DropTable(
                name: "ParamNames");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("8554ee2b-e60d-40a5-9751-80dc26a9a5dd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("8b89bb24-0648-4c51-9ae7-76a59ad5e39b"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("7ba64c1d-823e-4841-8287-ed440f8ab217"), new DateTime(2021, 7, 10, 11, 55, 24, 824, DateTimeKind.Local).AddTicks(3099), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("467e2276-1f12-44d5-8ce2-a43dba846802"), new DateTime(2021, 7, 10, 11, 55, 24, 824, DateTimeKind.Local).AddTicks(3495), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });
        }
    }
}

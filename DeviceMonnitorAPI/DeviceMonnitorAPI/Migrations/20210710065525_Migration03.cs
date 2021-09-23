using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceMonnitorAPI.Migrations
{
    public partial class Migration03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("1afd9f95-fe90-4c07-a1e5-0b866ad7be6e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("4c3e4d5a-87e3-4e1b-8c2b-ca10dc7ae5c4"));

            migrationBuilder.AddColumn<bool>(
                name: "DO0",
                table: "DeviceConfig",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DO1",
                table: "DeviceConfig",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DO2",
                table: "DeviceConfig",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DO3",
                table: "DeviceConfig",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("7ba64c1d-823e-4841-8287-ed440f8ab217"), new DateTime(2021, 7, 10, 11, 55, 24, 824, DateTimeKind.Local).AddTicks(3099), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("467e2276-1f12-44d5-8ce2-a43dba846802"), new DateTime(2021, 7, 10, 11, 55, 24, 824, DateTimeKind.Local).AddTicks(3495), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("467e2276-1f12-44d5-8ce2-a43dba846802"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserGuid",
                keyValue: new Guid("7ba64c1d-823e-4841-8287-ed440f8ab217"));

            migrationBuilder.DropColumn(
                name: "DO0",
                table: "DeviceConfig");

            migrationBuilder.DropColumn(
                name: "DO1",
                table: "DeviceConfig");

            migrationBuilder.DropColumn(
                name: "DO2",
                table: "DeviceConfig");

            migrationBuilder.DropColumn(
                name: "DO3",
                table: "DeviceConfig");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("1afd9f95-fe90-4c07-a1e5-0b866ad7be6e"), new DateTime(2021, 7, 7, 19, 18, 26, 753, DateTimeKind.Local).AddTicks(4005), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Api", true, "Admin", "@p!Adm!n21U$er00222", "ApiAdmin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "apiadmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserGuid", "CreatedDate", "Description", "EditedDate", "FirstName", "IsActive", "LastName", "Password", "Role", "Token", "TokenExpire", "Username" },
                values: new object[] { new Guid("4c3e4d5a-87e3-4e1b-8c2b-ca10dc7ae5c4"), new DateTime(2021, 7, 7, 19, 18, 26, 753, DateTimeKind.Local).AddTicks(4461), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", true, "User", "@@dm!nU$er", "Admin", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });
        }
    }
}

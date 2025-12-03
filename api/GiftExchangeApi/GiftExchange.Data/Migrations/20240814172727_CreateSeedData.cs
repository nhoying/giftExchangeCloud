using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GiftExchange.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "GiftExchange",
                table: "TurnActionTypes",
                columns: new[] { "TurnActionTypeId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 10, "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "New" },
                    { 20, "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trade" }
                });

            migrationBuilder.InsertData(
                schema: "GiftExchange",
                table: "TurnStatuses",
                columns: new[] { "TurnStatusId", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "NotPlayed" },
                    { 2, "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress" },
                    { 3, "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "SeedData", new DateTime(2024, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "GiftExchange",
                table: "TurnActionTypes",
                keyColumn: "TurnActionTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "GiftExchange",
                table: "TurnActionTypes",
                keyColumn: "TurnActionTypeId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "GiftExchange",
                table: "TurnStatuses",
                keyColumn: "TurnStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "GiftExchange",
                table: "TurnStatuses",
                keyColumn: "TurnStatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "GiftExchange",
                table: "TurnStatuses",
                keyColumn: "TurnStatusId",
                keyValue: 3);
        }
    }
}

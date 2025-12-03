using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftExchange.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTurnStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurnStatusId",
                schema: "GiftExchange",
                table: "Turns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TurnStatuses",
                schema: "GiftExchange",
                columns: table => new
                {
                    TurnStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnStatuses", x => x.TurnStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turns_TurnStatusId",
                schema: "GiftExchange",
                table: "Turns",
                column: "TurnStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turns_TurnStatuses_TurnStatusId",
                schema: "GiftExchange",
                table: "Turns",
                column: "TurnStatusId",
                principalSchema: "GiftExchange",
                principalTable: "TurnStatuses",
                principalColumn: "TurnStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turns_TurnStatuses_TurnStatusId",
                schema: "GiftExchange",
                table: "Turns");

            migrationBuilder.DropTable(
                name: "TurnStatuses",
                schema: "GiftExchange");

            migrationBuilder.DropIndex(
                name: "IX_Turns_TurnStatusId",
                schema: "GiftExchange",
                table: "Turns");

            migrationBuilder.DropColumn(
                name: "TurnStatusId",
                schema: "GiftExchange",
                table: "Turns");
        }
    }
}

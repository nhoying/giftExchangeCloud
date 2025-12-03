using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftExchange.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "GiftExchange");

            migrationBuilder.CreateTable(
                name: "Exchanges",
                schema: "GiftExchange",
                columns: table => new
                {
                    ExchangeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false),
                    MaxSwipes = table.Column<int>(type: "int", nullable: false),
                    MaxTurnActions = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.ExchangeId);
                });

            migrationBuilder.CreateTable(
                name: "TurnActionTypes",
                schema: "GiftExchange",
                columns: table => new
                {
                    TurnActionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnActionTypes", x => x.TurnActionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Gifts",
                schema: "GiftExchange",
                columns: table => new
                {
                    GiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    HasLotteryTickets = table.Column<bool>(type: "bit", nullable: false),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.GiftId);
                    table.ForeignKey(
                        name: "FK_Gifts_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalSchema: "GiftExchange",
                        principalTable: "Exchanges",
                        principalColumn: "ExchangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                schema: "GiftExchange",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    PlayerIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalSchema: "GiftExchange",
                        principalTable: "Exchanges",
                        principalColumn: "ExchangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turns",
                schema: "GiftExchange",
                columns: table => new
                {
                    TurnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    TurnNumber = table.Column<int>(type: "int", nullable: false),
                    TurnIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    StartingPlayerId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turns", x => x.TurnId);
                    table.ForeignKey(
                        name: "FK_Turns_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalSchema: "GiftExchange",
                        principalTable: "Exchanges",
                        principalColumn: "ExchangeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turns_Players_StartingPlayerId",
                        column: x => x.StartingPlayerId,
                        principalSchema: "GiftExchange",
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateTable(
                name: "TurnActions",
                schema: "GiftExchange",
                columns: table => new
                {
                    TurnActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurnId = table.Column<int>(type: "int", nullable: false),
                    TurnActionIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ParentTurnActionId = table.Column<int>(type: "int", nullable: true),
                    GiftId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    TurnActionTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurnActions", x => x.TurnActionId);
                    table.ForeignKey(
                        name: "FK_TurnActions_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalSchema: "GiftExchange",
                        principalTable: "Gifts",
                        principalColumn: "GiftId");
                    table.ForeignKey(
                        name: "FK_TurnActions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalSchema: "GiftExchange",
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                    table.ForeignKey(
                        name: "FK_TurnActions_TurnActionTypes_TurnActionTypeId",
                        column: x => x.TurnActionTypeId,
                        principalSchema: "GiftExchange",
                        principalTable: "TurnActionTypes",
                        principalColumn: "TurnActionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurnActions_TurnActions_ParentTurnActionId",
                        column: x => x.ParentTurnActionId,
                        principalSchema: "GiftExchange",
                        principalTable: "TurnActions",
                        principalColumn: "TurnActionId");
                    table.ForeignKey(
                        name: "FK_TurnActions_Turns_TurnId",
                        column: x => x.TurnId,
                        principalSchema: "GiftExchange",
                        principalTable: "Turns",
                        principalColumn: "TurnId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_ExchangeId",
                schema: "GiftExchange",
                table: "Gifts",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ExchangeId",
                schema: "GiftExchange",
                table: "Players",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnActions_GiftId",
                schema: "GiftExchange",
                table: "TurnActions",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnActions_ParentTurnActionId",
                schema: "GiftExchange",
                table: "TurnActions",
                column: "ParentTurnActionId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnActions_PlayerId",
                schema: "GiftExchange",
                table: "TurnActions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnActions_TurnActionTypeId",
                schema: "GiftExchange",
                table: "TurnActions",
                column: "TurnActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TurnActions_TurnId",
                schema: "GiftExchange",
                table: "TurnActions",
                column: "TurnId");

            migrationBuilder.CreateIndex(
                name: "IX_Turns_ExchangeId",
                schema: "GiftExchange",
                table: "Turns",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Turns_StartingPlayerId",
                schema: "GiftExchange",
                table: "Turns",
                column: "StartingPlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurnActions",
                schema: "GiftExchange");

            migrationBuilder.DropTable(
                name: "Gifts",
                schema: "GiftExchange");

            migrationBuilder.DropTable(
                name: "TurnActionTypes",
                schema: "GiftExchange");

            migrationBuilder.DropTable(
                name: "Turns",
                schema: "GiftExchange");

            migrationBuilder.DropTable(
                name: "Players",
                schema: "GiftExchange");

            migrationBuilder.DropTable(
                name: "Exchanges",
                schema: "GiftExchange");
        }
    }
}

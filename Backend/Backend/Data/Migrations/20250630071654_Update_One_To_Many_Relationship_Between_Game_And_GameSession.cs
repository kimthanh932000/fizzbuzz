using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_One_To_Many_Relationship_Between_Game_And_GameSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameSessions_GameId",
                table: "GameSessions");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_GameId",
                table: "GameSessions",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GameSessions_GameId",
                table: "GameSessions");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_GameId",
                table: "GameSessions",
                column: "GameId",
                unique: true);
        }
    }
}

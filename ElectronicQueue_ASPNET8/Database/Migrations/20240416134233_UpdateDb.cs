using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectronicQueue.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueItems_Statuses_StatusNumber",
                table: "QueueItems");

            migrationBuilder.DropIndex(
                name: "IX_QueueItems_StatusNumber",
                table: "QueueItems");

            migrationBuilder.DropColumn(
                name: "StatusNumber",
                table: "QueueItems");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "QueueItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QueueItems_StatusId",
                table: "QueueItems",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueItems_Statuses_StatusId",
                table: "QueueItems",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Number",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueItems_Statuses_StatusId",
                table: "QueueItems");

            migrationBuilder.DropIndex(
                name: "IX_QueueItems_StatusId",
                table: "QueueItems");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "QueueItems");

            migrationBuilder.AddColumn<short>(
                name: "StatusNumber",
                table: "QueueItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QueueItems_StatusNumber",
                table: "QueueItems",
                column: "StatusNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueItems_Statuses_StatusNumber",
                table: "QueueItems",
                column: "StatusNumber",
                principalTable: "Statuses",
                principalColumn: "Number");
        }
    }
}

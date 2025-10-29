using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifyBankAccountOwner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "bankAccounts");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "bankAccounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_bankAccounts_OwnerId",
                table: "bankAccounts",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_bankAccounts_Users_OwnerId",
                table: "bankAccounts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bankAccounts_Users_OwnerId",
                table: "bankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_bankAccounts_OwnerId",
                table: "bankAccounts");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "bankAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "bankAccounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}

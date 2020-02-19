using Microsoft.EntityFrameworkCore.Migrations;

namespace memiarzeEu.Migrations
{
    public partial class MemeCommentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPage",
                table: "Memes");

            migrationBuilder.AddColumn<int>(
                name: "MemeId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MemeId",
                table: "Comments",
                column: "MemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Memes_MemeId",
                table: "Comments",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Memes_MemeId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MemeId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MemeId",
                table: "Comments");

            migrationBuilder.AddColumn<bool>(
                name: "MainPage",
                table: "Memes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

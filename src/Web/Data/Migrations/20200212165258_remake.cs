using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace memiarzeEu.Migrations
{
    public partial class remake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memes_AspNetUsers_ApplicationUserId",
                table: "Memes");

            migrationBuilder.DropForeignKey(
                name: "FK_XdPoints_AspNetUsers_ApplicationUserId",
                table: "XdPoints");

            migrationBuilder.DropIndex(
                name: "IX_XdPoints_ApplicationUserId",
                table: "XdPoints");

            migrationBuilder.DropIndex(
                name: "IX_Memes_ApplicationUserId",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "XdPoints");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "MemeId",
                table: "XdPoints",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "XdPoints",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "XdPoints",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "XdPoints",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "XdPoints",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Memes",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "MainPage",
                table: "Memes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Memes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Memes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "AspNetUsers",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_XdPoints_CommentId",
                table: "XdPoints",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_XdPoints_UserId",
                table: "XdPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Memes_UserId",
                table: "Memes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_AspNetUsers_UserId",
                table: "Memes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_XdPoints_Comments_CommentId",
                table: "XdPoints",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_XdPoints_AspNetUsers_UserId",
                table: "XdPoints",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memes_AspNetUsers_UserId",
                table: "Memes");

            migrationBuilder.DropForeignKey(
                name: "FK_XdPoints_Comments_CommentId",
                table: "XdPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_XdPoints_AspNetUsers_UserId",
                table: "XdPoints");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_XdPoints_CommentId",
                table: "XdPoints");

            migrationBuilder.DropIndex(
                name: "IX_XdPoints_UserId",
                table: "XdPoints");

            migrationBuilder.DropIndex(
                name: "IX_Memes_UserId",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "XdPoints");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "XdPoints");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "XdPoints");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "XdPoints");

            migrationBuilder.DropColumn(
                name: "MainPage",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Memes");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "MemeId",
                table: "XdPoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "XdPoints",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Memes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Memes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "About",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 160,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_XdPoints_ApplicationUserId",
                table: "XdPoints",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Memes_ApplicationUserId",
                table: "Memes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_AspNetUsers_ApplicationUserId",
                table: "Memes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_XdPoints_AspNetUsers_ApplicationUserId",
                table: "XdPoints",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

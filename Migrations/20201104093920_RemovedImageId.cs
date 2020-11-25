using Microsoft.EntityFrameworkCore.Migrations;

namespace Task2.Migrations
{
    public partial class RemovedImageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ImgId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ImgId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImgId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImgId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ImgId",
                table: "Users",
                column: "ImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ImgId",
                table: "Users",
                column: "ImgId",
                principalTable: "Images",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

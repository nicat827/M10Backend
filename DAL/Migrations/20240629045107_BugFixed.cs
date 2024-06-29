using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace M10Backend.DAL.Migrations
{
    public partial class BugFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTPCode_AspNetUsers_UserId",
                table: "OTPCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OTPCode",
                table: "OTPCode");

            migrationBuilder.RenameTable(
                name: "OTPCode",
                newName: "OTPCodes");

            migrationBuilder.RenameIndex(
                name: "IX_OTPCode_UserId",
                table: "OTPCodes",
                newName: "IX_OTPCodes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OTPCodes",
                table: "OTPCodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OTPCodes_AspNetUsers_UserId",
                table: "OTPCodes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTPCodes_AspNetUsers_UserId",
                table: "OTPCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OTPCodes",
                table: "OTPCodes");

            migrationBuilder.RenameTable(
                name: "OTPCodes",
                newName: "OTPCode");

            migrationBuilder.RenameIndex(
                name: "IX_OTPCodes_UserId",
                table: "OTPCode",
                newName: "IX_OTPCode_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OTPCode",
                table: "OTPCode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OTPCode_AspNetUsers_UserId",
                table: "OTPCode",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

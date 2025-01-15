using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakaleWebProje.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MakaleComment",
                keyColumn: "MakaleCommentId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 10, 17, 22, 3, 715, DateTimeKind.Local).AddTicks(8086));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MakaleComment",
                keyColumn: "MakaleCommentId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 10, 17, 18, 53, 132, DateTimeKind.Local).AddTicks(6865));
        }
    }
}

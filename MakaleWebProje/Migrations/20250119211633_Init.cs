using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MakaleWebProje.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategori",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.UserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Makale",
                columns: table => new
                {
                    MakaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakaleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleSummary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MakaleIsShow = table.Column<bool>(type: "bit", nullable: false),
                    MakaleImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KategoriId = table.Column<int>(type: "int", nullable: false),
                    MakaleCarousel = table.Column<bool>(type: "bit", nullable: false),
                    MakaleIsShowHome = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makale", x => x.MakaleId);
                    table.ForeignKey(
                        name: "FK_Makale_Kategori_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Kategori",
                        principalColumn: "KategoriId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "Role",
                        principalColumn: "UserRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MakaleData",
                columns: table => new
                {
                    MakaleDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MakaleId = table.Column<int>(type: "int", nullable: false),
                    MakaleLike = table.Column<int>(type: "int", nullable: false),
                    MakaleDislike = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakaleData", x => x.MakaleDataId);
                    table.ForeignKey(
                        name: "FK_MakaleData_Makale_MakaleId",
                        column: x => x.MakaleId,
                        principalTable: "Makale",
                        principalColumn: "MakaleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MakaleData_User_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MakaleComment",
                columns: table => new
                {
                    MakaleCommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakaleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    MakaleDataId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakaleComment", x => x.MakaleCommentId);
                    table.ForeignKey(
                        name: "FK_MakaleComment_MakaleData_MakaleDataId",
                        column: x => x.MakaleDataId,
                        principalTable: "MakaleData",
                        principalColumn: "MakaleDataId");
                    table.ForeignKey(
                        name: "FK_MakaleComment_Makale_MakaleId",
                        column: x => x.MakaleId,
                        principalTable: "Makale",
                        principalColumn: "MakaleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MakaleComment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Kategori",
                columns: new[] { "KategoriId", "KategoriName" },
                values: new object[,]
                {
                    { 1, "Bilim Kurgu" },
                    { 2, "Yazılım" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "UserRoleId", "UserRoleName" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Makale",
                columns: new[] { "MakaleId", "KategoriId", "MakaleCarousel", "MakaleContent", "MakaleDate", "MakaleImagesUrl", "MakaleIsShow", "MakaleIsShowHome", "MakaleName", "MakaleSummary" },
                values: new object[,]
                {
                    { 1, 1, true, "asd", "31.12.2013", "/images/1.jpg", true, true, "Denem1", "Makale1Özet" },
                    { 2, 2, true, "asd", "26.08.2008", "/images/2.jpg", true, true, "Denem2", "Makale2Özet" },
                    { 3, 2, true, "asd", "14.03.2021", "/images/3.jpg", true, true, "Denem3", "Makale3Özet" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Name", "Password", "SurName", "UserName", "UserRoleId" },
                values: new object[,]
                {
                    { 1, "ozdemirbaranugur@example.com", "Baran Uğur", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Özdemir", "ozdemir", 2 },
                    { 2, "tuncyusuf@example.com", "Yusuf", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Tunç", "tunc", 2 },
                    { 3, "soydincomer@example.com", "Ömer", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Soydinç", "soydınc", 2 },
                    { 4, "aliari@example.com", "Ali", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Arı", "ari", 1 }
                });

            migrationBuilder.InsertData(
                table: "MakaleComment",
                columns: new[] { "MakaleCommentId", "Comment", "CreatedDate", "IsActive", "MakaleDataId", "MakaleId", "UserId" },
                values: new object[] { 1, "baran emmi", new DateTime(2025, 1, 20, 0, 16, 32, 363, DateTimeKind.Local).AddTicks(1444), true, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "MakaleData",
                columns: new[] { "MakaleDataId", "MakaleDislike", "MakaleId", "MakaleLike", "UserId", "UsersUserId" },
                values: new object[] { 1, 1, 1, 1, 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_Makale_KategoriId",
                table: "Makale",
                column: "KategoriId");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleComment_MakaleDataId",
                table: "MakaleComment",
                column: "MakaleDataId");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleComment_MakaleId",
                table: "MakaleComment",
                column: "MakaleId");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleComment_UserId",
                table: "MakaleComment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleData_MakaleId",
                table: "MakaleData",
                column: "MakaleId");

            migrationBuilder.CreateIndex(
                name: "IX_MakaleData_UsersUserId",
                table: "MakaleData",
                column: "UsersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                table: "User",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MakaleComment");

            migrationBuilder.DropTable(
                name: "MakaleData");

            migrationBuilder.DropTable(
                name: "Makale");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Kategori");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
